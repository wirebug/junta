﻿using System;
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
                    for (int i = 0; i < reihenfolge.Count(); i++)
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
        /// <summary>
        /// Spielphase II: Versprechungen machen
        /// </summary>
        /// <param name="ids">ID der Karten</param>
        /// <param name="sp">Spieler der Karte erhalten soll</param>
        public void VersprechungMachen(int[] ids, Spieler sp)
        {
            // mind 1 pro Spieler vom Imp.
            sp.versprechungen.AddRange(ids);
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

        /// <summary>
        /// Spielpahse IV: Kampf austragen
        /// </summary>
        public void KaempfeAustragen()
        {
            //Spieler hat kampf opjekt was hier abgehandelt wird
            //MUSS NACH ABHANDLUNG AUF NULL GESETZT WERDEN
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
        /// Spielpahse VI: Handkartenlimit prüfen
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
    }
}