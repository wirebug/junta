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
        public int imperatorID;
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
            taeter.hand.AddHandkarte(opfer.hand.RandomHandkarte());
        }
        public void VersprechungMachen(int[] ids, Spieler sp)
        {
            // mind 1 pro Spieler vom Imp.
            sp.versprechungen.AddRange(ids);
        }
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
                        s.kampf = new ImperatorKampf(s);
                        a = (ImperatorKampf)s.kampf;
                        a.addVerteidigung(s, s.flotten);//alle Imp. flotten verteidigen
                    }
                    else
                    {
                        s.kampf = new Kampf(s);
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
    }
}