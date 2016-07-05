using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.Code.Karten;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;

namespace GameServer.Code
{
    public class Spielverwaltung {
        public List<Spieler> spieler;
        public Spieler imperator;
        public JuntaHub _hub { get; set; }
        public Deck deck { get; set; }
        public int rundenCount { get; set; } = 0;
        public int ImperatorID {
            get { return imperator.ID; }
        }
        private int schedule = 0;
        //Phasen
        /// <summary>
        /// Spielphase I: KartenZiehen
        /// </summary>
        public void KartenZiehen()
        {
            Karte k;
            foreach (Spieler s in spieler)
            {
                if (!s.imperator)
                {
                    if (rundenCount < 1)
                    {
                        //in er ersten Runde 2 Karten ziehen
                        k = deck.Ziehen();
                        s.hand.AddHandkarte(k);
                        _hub.KarteIdHinzu(s, k);
                        k = deck.Ziehen();
                        s.hand.AddHandkarte(k);
                        _hub.KarteIdHinzu(s, k);
                    }
                    else
                    {
                        //1 Karte ziehen
                        k = deck.Ziehen();
                        s.hand.AddHandkarte(k);
                        _hub.KarteIdHinzu(s, k);
                    }
                }
                else
                {
                    //Imperator, Spielerzahl + 2 Karten ziehen
                    for (int i = 0; i < spieler.Count(); i++)
                    {
                        k = deck.Ziehen();
                        s.hand.AddHandkarte(k);
                        _hub.KarteIdHinzu(s, k);
                    }
                    k = deck.Ziehen();
                    s.hand.AddHandkarte(k);
                    _hub.KarteIdHinzu(s, k);

                    k = deck.Ziehen();
                    s.hand.AddHandkarte(k);
                    _hub.KarteIdHinzu(s, k);
                }
            }
        }
        /// <summary>
        /// Spielphase II 
        /// 
        /// Imperator wird aufgerufen und über JuntaHub und ClientHub
        /// zur Auswahl der Versprechungen aufgerufen
        /// Imperator wählt Karten und Spieler
        /// </summary>
        /// <param name="spieler">Der Imperator muss übergeben werden</param>
        public void VersprechungStart(Spieler spieler)
        {
            int idSpieler = spieler.ID;
            string json = JsonConvert.SerializeObject(spieler.hand.handKarten);

            _hub.VersprechenAuswählen(idSpieler,json);
        }
        /// <summary>
        /// Spielphase II 
        /// Versprechungen Machen
        /// Imperator hat Karten und Spieler gewählt
        /// Infos werden vom ClientProxyHub aufgenommen und von JuntaHubMehtode verarbeitet(Versprechen Verarbeiten) nimmt spielerID und KartenID an
        /// Spielverwaltung wandelt SpielerID in Spieler und KartenID in Karte um
        /// VersprechungMachen wird aufgerufen und dadurch dem Spieler das Karten[] übergeben + dem Imperator die Karten abgezogen.
        /// Nach VersprechungenMachen wird EinbrecherKarteSpielen abgefragt
        /// Versprechungen werden direkt verteilt wenn kein ImperatorKampf vorhanden ist.(Wenn niemand den Imperator angreift)
        /// Versprechungen werden nach der Kampfphase verteilt, wenn der Imperator überlebt.
        /// </summary>
        /// <param name="idSpieler"></param>
        /// <param name="idKarten"></param>
        public void VersprechungMachen(Dictionary<int,int> versprechung) //1.key = karte, 2.value = spieler
        {

            foreach(KeyValuePair<int,int> pair in versprechung)
            {
                Karte temp = imperator.hand.getKarteById(pair.Key);
                if (pair.Value != ImperatorID) {
                    imperator.hand.RemoveHandkarte(temp);
                    _hub.KarteIDEntfernen(imperator, temp);
                    GetSpieleraById(pair.Value).versprechungen.Add(temp);
                    _hub.VersprechenHinzu(pair.Value, pair.Key, temp.titel, temp.text);
                }
            }
            FlottenStart();
           /*
            foreach(Spieler s in spieler)
            {
                if (s.hand.hatEinbrecher)
                {
                    EinbrecherKarteSpielen(s);
                }
            
             }*/
        }

        /// <summary>
        /// Falls der Spieler eine EinbrecherKarte hat, wird die Methode aufgerufen.
        /// Die Methode übergibt den Spieler mit der Karte an den Hub
        /// Der Spieler kann sich aussuchen ob er den Einbrecher spielen möchte
        /// </summary>
        public void EinbrecherKarteSpielen(Spieler spieler)
        {
            _hub.SpieleEinbrecher(spieler);
        }
        public void verarbeiteEinbrecherAntwort(bool b,int idSpielerWeg, int idSpielerHinzu)
        {
            if (b)
            {
                KarteKlauen(GetSpieleraById(idSpielerHinzu), GetSpieleraById(idSpielerWeg));
                Karte k = GetSpieleraById(idSpielerHinzu).hand.getKarteById(1);
                GetSpieleraById(idSpielerHinzu).hand.RemoveHandkarte(k);
                _hub.KarteIDEntfernen(GetSpieleraById(idSpielerHinzu), k);
                deck.Ablegen(k);
            }
        }
        /// <summary>
        /// Spielphase III Flotten befehligen
        /// Jeder Spieler wählt jetzt seine Flotten
        /// </summary>
        public void FlottenStart()
        {
            foreach(Spieler s in spieler)
            {
                if (!s.imperator)
                {
                    s.kampf = new Kampf(s);
                } else {
                    s.kampf = new ImperatorKampf(s);
                }
            }
            FlottenScheduler();
        }

        public void FlottenScheduler() {
            if(schedule < spieler.Count) {
                if (!spieler[schedule].imperator) {
                    _hub.FlottenAuswahl(spieler[schedule], spieler[schedule].flotten);
                } else {
                    schedule++;
                    FlottenScheduler();
                }
            } else {
                KaempfeAustragen();
            }
        }
        

        
        /// <summary>
        /// Spielphase III: Flotten befehligen
        /// </summary>
        /// <param name="sp">Spieler der sich verteidigt</param>
        /// <param name="würfel">einzelne Würfelwerte des Spielers</param>
        public void FlottenBefehligen(int idSpieler, int[] würfel)
        {
            Spieler temp = GetSpieleraById(idSpieler);
            foreach(int i in würfel) {
                if(i == 6) {
                    (imperator.kampf as ImperatorKampf).addVerteidigung(temp);
                } else if(i == idSpieler) {
                    temp.kampf.addVerteidigung();
                } else {
                    GetSpieleraById(i).kampf.addAngriff(temp);
                }
            }
            schedule++;
            FlottenScheduler();
        }
        
        /// <summary>
        /// Spielphase IV: Kampf austragen 
        /// </summary>
        public void KaempfeAustragen()
        {
            schedule = 0;
            List<Spieler> gewinner;
            Spieler nImperator = imperator;
            foreach(Spieler i in spieler) {
                if (i.imperator) {
                    gewinner = (i.kampf as ImperatorKampf).ErgebnisBerechnen();
                    if(gewinner.Count > 0) {
                        _hub.AlleVersprechenEnfernen();
                        AlleVersprechenEntfernen();
                        nImperator = gewinner.First();
                        foreach(Spieler p in gewinner) {
                            KarteKlauen(p, imperator);
                        }
                    } else {
                        foreach(KeyValuePair<Spieler,int> s in i.kampf.angriffswürfel) {
                            AlleVersprechenEntfernen(s.Key);
                            _hub.VersprechenEntfernen(s.Key);
                        }
                    }
                } else {
                    gewinner = i.kampf.ErgebnisBerechnen();
                    if(gewinner.Count > 0) {
                        foreach(Spieler p in gewinner) {
                            KarteKlauen(p, i);
                        }
                    }
                }
            }
            
            foreach(Spieler s in spieler) {
                if(s.versprechungen.Count > 0) {
                    foreach (Karte k in s.versprechungen) {
                        s.hand.AddHandkarte(k);
                    }
                    s.versprechungen.Clear();
                    _hub.VersprechenEntfernen(s);
                }
            }
            neuerImperator(nImperator);
            Kaufen();
        }

        public void Kaufen() {
           /* foreach(Spieler v in spieler) {
                _hub.Kaufen(v);
            }*/

            if (schedule < spieler.Count) {
                _hub.Kaufen(spieler[schedule]);
            } else {
                HandkartenlimitPrüfen();
            }
        }

        private void KaufenSpieler(Spieler spieler, bool second) {
            _hub.Kaufen2(spieler, second);
        }
        public void GeldAusgeben(int idSpieler, int kauf)
        {
            Spieler temp = GetSpieleraById(idSpieler);
            switch (kauf) {
                case 1: temp.GeldZuSchreiben += 1000;
                        Karte k = deck.Ziehen();
                        temp.hand.AddHandkarte(k);
                        _hub.KarteIdHinzu(temp, k);
                        break;
                case 2: temp.GeldZuSchreiben += 2000;
                        temp.flotten++;
                        _hub.AddMiliz(temp);
                        break;
                case 3: temp.GeldZuSchreiben += 4000;
                        temp.planet.addGebäude();
                        _hub.AddGebäude(temp);
                        break;
            }
            if(kauf > 1 && temp.Credits - temp.GeldZuSchreiben >= 1000) {
                KaufenSpieler(temp, false);
            } else if(kauf > 0 && temp.Credits - temp.GeldZuSchreiben >= 2000) {
                KaufenSpieler(temp, true);
            } else {
                temp.schreibeAusgaben();
                schedule++;
                Kaufen();
            }
        }
        /// <summary>
        /// Spielphase VI: Handkartenlimit prüfen
        /// </summary>
        public void HandkartenlimitPrüfen()
        {
            schedule = 0;
            Karte tmp;
            foreach (Spieler s in spieler)
            {
                while (s.hand.GetHandkartenAnzahl() > 4)
                {
                    tmp = s.hand.RandomHandkarte();
                    deck.Gespielt(tmp);
                    _hub.KarteIDEntfernen(s, tmp);
                }
            }
            rundenCount++;
            Spieler temp = won();
            if(temp != null) {
                _hub.won(temp);
            } else { GameLoop.Next(); }
            
        }
        //Methoden
        public void neuerImperator(Spieler s)
        {
            _hub.SetzeImperator(s, imperator);
            imperator.imperator = false;
            imperator = s;
            s.imperator = true;
        }
        public void KarteKlauen(Spieler taeter, Spieler opfer)
        {
            if (!opfer.hand.isEmpty)
            {
                Karte temp = opfer.hand.RandomHandkarte();
                taeter.hand.AddHandkarte(temp);
                _hub.KarteIDEntfernen(opfer, temp);
                _hub.KarteIdHinzu(taeter, temp);
            }
        }

        private Spieler GetSpieleraById(int id) {
            foreach(Spieler i in spieler) {
                if(i.ID == id) {
                    return i;
                }
            }
            return null;
        }

        private void AlleVersprechenEntfernen() {
            foreach(Spieler c in spieler) {
                foreach(Karte k in c.versprechungen) {
                    deck.Ablegen(k);
                }
                c.versprechungen.Clear();
            }
        }
        private void AlleVersprechenEntfernen(Spieler c) {
            foreach (Karte k in c.versprechungen) {
                deck.Ablegen(k);
            }
            c.versprechungen.Clear();
        }
        
        private Spieler won() {
            foreach(Spieler i in spieler) {
                if(i.punkte >= 6) {
                    return i;
                } 
            }
            return null;
        }

    }
}