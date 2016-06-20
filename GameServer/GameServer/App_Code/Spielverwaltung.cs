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
        JuntaHub _hub;
        public Deck deck { get; }
        public int rundenCount { get; set; }
        public void KartenZiehen()
        {
            foreach (Spieler s in spieler)
            {
                if (!s.imperator)
                {
                    if (rundenCount < 1)
                    {
                        //in der ersten Runde 2 Karten ziehen
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
                    for(int i=0;i<reihenfolge.Count;i++)
                    {
                        s.hand.AddHandkarte(deck.Ziehen());
                    }
                    s.hand.AddHandkarte(deck.Ziehen());
                    s.hand.AddHandkarte(deck.Ziehen());
                }

            }
        }
        public void KarteKlauen(Spieler taeter, Spieler opfer)
        {
            Karte rndKarte = opfer.hand.RandomHandkarte();
            opfer.hand.RemoveHandkarte(rndKarte);
            taeter.hand.AddHandkarte(rndKarte);
        }
        public void VersprechungMachen(int[] ids, Spieler sp)
        {
            // mind 1 pro Spieler vom Imp.
            sp.versprechungen.AddRange(ids);
        }
        public void FlottenBefehligen(Spieler sp, int[] würfel)
        {
            //Für jeden einzelnen VERTEIDIGENDEN Spieler wird ein Kampf erstellt
            if (sp.kampf == null)
            {
                foreach (Spieler s in spieler)
                {
                    if (s.imperator)
                    {
                        s.kampf = new ImperatorKampf();
                        s.kampf.addVerteidigung(s.flotten);//alle imp flotten verteidigen
                    }
                    else
                    {
                        s.kampf = new Kampf();
                    }
                }
            }
            if (!sp.imperator)
            {

            }
        }
        public void KaempfeAustragen()
        {
            //Spieler hat kampf opjekt was hier abgehandelt wird
            //MUSS NACH ABHANDLUNG AUF NULL GESETZT WERDEN
        }
        public void GeldAusgeben(Spieler sp, int kauf)
        {
            switch (kauf)
            {
                case 1://Handkarte hinzufügen
                    if (sp.Credits >= 1)
                    {
                        sp.hand.AddHandkarte(deck.Ziehen());
                        sp.Credits -= 1;
                    }
                    break;
                case 2://Flotte hinzufügen
                    if (sp.Credits >= 2)
                    {
                        if (sp.flotten < 4)
                        {
                            sp.flotten++;
                            sp.Credits -= 2;
                        }
                    }
                    break;
                case 3://Gebäude hinzufügen
                    if (sp.Credits >= 4)
                    {
                        if(sp.planet.gebäude<5)
                            sp.planet.gebäude++;
                        sp.Credits -= 4;
                    }
                    break;
                default:
                    break;
            }
        }
        public void HandkartenlimitPrüfen()
        {
            foreach (Spieler s in reihenfolge) //iterate through int or change reihenfolge to Spieler array
            {
                while (s.hand.GetHandKartenZahl() > 4)
                {
                    s.hand.RemoveHandkarte(s.hand.RandomHandkarte());
                }
            }
            rundenCount++;
        }
    }

}