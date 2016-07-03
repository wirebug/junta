using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;
using Newtonsoft.Json;

namespace GameServer.App_Code
{
    public class Spielverwaltung
    {
        public int[] reihenfolge { get; }
        public Spieler[] spieler;
        public Spieler imperator;
        public JuntaHub _hub { get; set; }
        public Deck deck { get; }
        public int rundenCount { get; set; }
        public int ImperatorID{
            get{return imperator.ID;}
        }
        
        //Phasen
        /// <summary>
        /// Spielphase I: KartenZiehen
        /// </summary>
        public void KartenZiehen()
        {
            foreach (Spieler s in spieler)
            {
                if (!s.imperator)
                {
                    if (rundenCount < 1)
                    {
                        //in er ersten Runde 2 Karten ziehen
                        s.hand.AddHandkarte(deck.Ziehen());                  
                        s.hand.AddHandkarte(deck.Ziehen());
                    }
                    else
                    {
                        //1 Karte ziehen
                        s.hand.AddHandkarte(deck.Ziehen());
                    }
                }
                else
                {
                    //Imperator, Spielerzahl + 2 Karten ziehen
                    for (int i = 0; i < reihenfolge.Count(); i++)
                    {
                        s.hand.AddHandkarte(deck.Ziehen());
                    }
                    s.hand.AddHandkarte(deck.Ziehen());
                    
                    s.hand.AddHandkarte(deck.Ziehen());
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
                    GetSpieleraById(pair.Value).hand.AddHandkarte(temp);
                    _hub.VersprechenHinzu(pair.Value, pair.Key, temp.titel, temp.text);
                }
            }
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
                    s.kampf = new Kampf();
                    _hub.FlottenAuswahl(s,s.flotten);
                } else {
                    s.kampf = new ImperatorKampf();
                }
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
        }
        
        /// <summary>
        /// Spielphase IV: Kampf austragen 
        /// </summary>
        public void KaempfeAustragen()
        {
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
                            s.Key.versprechungen.Clear();
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
        }


        public void GeldAusgeben(Spieler sp, int kauf)
        {
            switch (kauf)
            {
                case 1://Handkarte hinzufügen
                    if (sp.Credits >= 1000)
                    {
                        sp.hand.AddHandkarte(deck.Ziehen());
                        sp.Credits -= 1000;
                    }
                    break;
                case 2://Flotte hinzufügen
                    if (sp.Credits >= 2000)
                    {
                        if (sp.flotten < 4000)
                        {
                            sp.flotten++;
                            sp.Credits -= 2000;
                        }
                    }
                    break;
                case 3://Gebäude hinzufügen
                    if (sp.Credits >= 4000)
                    {
                        if (sp.planet.gebäude < 5000)
                            sp.planet.gebäude++;
                        sp.Credits -= 4000;
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Spielphase VI: Handkartenlimit prüfen
        /// </summary>
        public void HandkartenlimitPrüfen()
        {
            Karte tmp;
            foreach (Spieler s in spieler)
            {
                while (s.hand.GetHandkartenAnzahl() > 4)
                {
                    tmp = s.hand.RandomHandkarte();
                    deck.Gespielt(tmp);
                }
            }
            rundenCount++;
        }

        public void verarbeiteSpionAntwort(bool b) {

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
                c.versprechungen.Clear();
            }
        }

    }
}