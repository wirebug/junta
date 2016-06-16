using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class Spielverwaltung
    { 
        public List<Spieler> reihenfolge { get; }
        JuntaHub _hub;
        public Deck deck { get; }
        public int rundenCount { get; set; }
        public void KartenZiehen()
        {
            foreach (Spieler s in reihenfolge)
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
                foreach (Spieler s in reihenfolge)
                {
                    s.kampf = new Kampf();
                }
            }
            if (sp.imperator)   //Imp.-Flotten verteidigen
            {
                sp.kampf.addVerteidigung(sp.flotten);
            }else
            {
                
            }
        }
        public void KaempfeAustragen()
        {
            //Spieler hat kampf opjekt was hier abgehandelt wird?
        }
        public void GeldAusgeben(Spieler sp, int kauf)
        {
            switch (kauf)
            {
                case 1://Handkarte hinzufügen
                    if (sp.konto.guthaben >= 1)
                    {
                        sp.hand.AddHandkarte(deck.Ziehen());
                        sp.konto.guthaben -= 1;
                    }
                    break;
                case 2://Flotte hinzufügen
                    if (sp.konto.guthaben >= 2)
                    {
                        if (sp.flotten < 4)
                        {
                            sp.flotten++;
                            sp.konto.guthaben -= 2;
                        }
                    }
                    break;
                case 3://Gebäude hinzufügen
                    if (sp.konto.guthaben >= 4)
                    {
                        if(sp.planet.gebäude<5)
                            sp.planet.gebäude++;
                        sp.konto.guthaben -= 4;
                    }
                    break;
                default:
                    break;
            }
        }
        public void HandkartenlimitPrüfen()
        {
            foreach (Spieler s in reihenfolge)
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