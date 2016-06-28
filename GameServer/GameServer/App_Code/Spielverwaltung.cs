using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;

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
        public void VersprechungStart()
        {
            _hub.VersprechenAuswählen();
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
        public void VersprechungMachen(int idSpieler, int[] idKarten)
        {
            Spieler tmp = spieler[idSpieler];
            Karte[] ktmp = new Karte[idKarten.Length];
            for (int i = 0; i < idKarten.Length; i++)
            {
                ktmp[i] = imperator.hand.getKarteById(i);
                imperator.hand.RemoveHandkarte(ktmp[i]);

                
            }
            tmp.versprechungen.AddRange(ktmp);
            
            foreach(Spieler s in spieler)
            {
                if (s.hand.hatEinbrecher)
                {
                    EinbrecherKarteSpielen(s);
                }
            }
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
        public void verarbeiteEinbrecherAntwort(bool b,int idSpieler)
        {
            if (b && idSpieler>=0)
            {
                foreach(Spieler s in spieler)
                {
                    if (s.hand.hatEinbrecher)
                    {
                        foreach (Spieler sp in spieler)
                        {
                            Spieler tmp = null;
                            if (idSpieler == sp.ID)
                            {
                                tmp = sp;
                            }
                            KarteKlauen(s, tmp);
                        }
                        //TODO EinbrecherAntwortAntwort
                        //Welchem spieler soll eine karte geklaut werden ?
                    }
                }
            }
        }
        
        /// <summary>
        /// Spielphase III: Flotten befehligen
        /// </summary>
        /// <param name="sp">Spieler der sich verteidigt</param>
        /// <param name="würfel">einzelne Würfelwerte des Spielers</param>
        public void FlottenBefehligen(Spieler sp, int[] würfel)
        {
            //Für jeden einzelnen VERTEIDIGENDEN Spieler wird ein Kampf erstellt
            ImperatorKampf a;
            int tmpw;
            if (sp.kampf == null)
            {
                foreach (Spieler s in spieler)
                {
                    if (s.imperator)
                    {
                        s.kampf = new ImperatorKampf();
                        a = (ImperatorKampf)s.kampf;
                        a.addVerteidigung(s, s.flotten);//alle Imp. flotten verteidigen
                    }
                    else
                    {
                        s.kampf = new Kampf();
                    }
                }
            }
            if (!sp.imperator)
            {
                for(int i=0;i<4;i++)
                {
                    tmpw = würfel[i];
                    if (tmpw == sp.ID)//wenn der würfel die eigene ID zeigt, verteidigt man sich
                    {
                        sp.kampf.addVerteidigung(1);
                    }
                    else
                    {
                        if (tmpw == 6)//6 ist immer das verteidigen des Imperators
                        {
                            a = (ImperatorKampf)spieler[imperatorID].kampf;
                            a.addVerteidigung(sp, 1);
                        }
                        else//angreifen des Spielers mit index seiner ID-1(da liste)
                        {
                            spieler[tmpw-1].kampf.addAngriff(sp, 1);
                        }
                    }
                }
            }
        }
        public void verarbeiteSpionAntwort(bool b)
        {

        }
        /// <summary>
        /// Spielphase IV: Kampf austragen 
        /// </summary>
        public void KaempfeAustragen()
        {
            //Spieler hat kampf opjekt was hier abgehandelt 
            //ImperatorKampf zuerst, dann reihenfolge
            List<Spieler> gewinner;
            Spieler nImperator = imperator;

            //Imperatorkampf
            //NACH allen Kämpfen neuer Imperator (gewinner)
            ImperatorKampf a;
            a = (ImperatorKampf)imperator.kampf;
            gewinner = a.ErgebnisBerechnen();
            if (gewinner.Count != 0)//Imperator verliert, KEINE VERPRECHEN bei allen
            {
                foreach (Spieler s in spieler)
                {
                    if(gewinner.Contains(s))
                    {
                        KarteKlauen(s, imperator);
                    }
                    foreach (Karte k in s.versprechungen)
                    {
                        deck.Gespielt(k);
                    }
                    s.versprechungen.Clear();
                }
                gewinner[0] = nImperator;
            }
            else//Imperator gewinnt
            {
                foreach(Spieler s in spieler)
                {
                    if (a.angriffswürfel.ContainsKey(s))//Angreifer kriegen keine Versprechungen
                    {
                        foreach (Karte k in s.versprechungen)
                        {
                            deck.Gespielt(k);
                        }
                    }
                    else//Alle anderen kriegen Versprechen instant auf die Hand
                    {
                        for (int i = s.versprechungen.Count; i > 0; i--)
                        {
                            s.hand.AddHandkarte(s.versprechungen[i - 1]);
                        }
                    }
                    s.versprechungen.Clear();
                }
            }                       //das geht bestimmt effizienter...
            
            //normale Spielerkämpfe
            foreach (Spieler s in spieler)
            {
                if (!s.imperator)
                {
                    gewinner = s.kampf.ErgebnisBerechnen();//Liste mit gewinnern als return(leer wenn vert. gewinnt)
                    s.kampf = null;//da mithilfe != null bei flottenbefehligen neuer Kampf erstellt wird
                    foreach (Spieler g in gewinner)
                    {
                        KarteKlauen(g, s);
                    }
                }
            }
            //der neue Imperator ist erst am Ende von allen Kämpfen gesetzt
            neuerImperator(nImperator);
        }
        /// <summary>
        /// Spielphase V: Geld ausgeben
        /// </summary>
        /// <param name="sp">Spieler der einkauft</param>
        /// <param name="kauf">Einkaufswunsch, 1 Handkarte, 2 Flotte, 3 Gebäude</param>
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
                while (s.hand.GetHandKartenZahl() > 4)
                {
                    tmp = s.hand.RandomHandkarte();
                    deck.Gespielt(tmp);
                }
            }
            rundenCount++;
        }


        //Methoden
        public void neuerImperator(Spieler s)
        {
            imperator.imperator = false;
            imperator = s;
            s.imperator = true;
        }
        public void KarteKlauen(Spieler taeter, Spieler opfer)
        {
            if (!opfer.hand.isEmpty)
            {
                Spieler henryk = opfer;
                taeter.hand.AddHandkarte(henryk.hand.RandomHandkarte());
            }
        }

    }
}