using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;

namespace GameServer.App_Code
{
    public class Hand
    {
        bool hatSpion { get; set; }
        bool hatEinbrecher { get; set; }
        bool hatKampfkarte { get; set; }
        public Spieler spieler { get; set; }

        /// <summary>
        /// Handkarten
        /// </summary>
        List<Karte> hand = new List<Karte>();
        void checkFlags()
        {
            hatKampfkarte = false;
            foreach (Karte k in hand)
            {
                if (k.ID == 0)
                {
                    hatSpion = true;
                }
                else
                    hatSpion = false;
                if (k.ID== 1)
                {
                    hatEinbrecher = true;
                }
                else
                    hatEinbrecher = false;
                if (k.ID > 1 || k.ID < 14)
                {
                    hatKampfkarte = true;
                }
            }
        }

        public Karte RandomHandkarte()
        {
            Random rng = new Random();
            int anzahl = GetHandKartenZahl();
            int index = rng.Next(anzahl);
            Karte ret = hand[index];
            RemoveHandkarte(hand[index]);
            return ret;
        }

        public void RemoveHandkarte(Karte item)
        {
            checkFlags();
            hand.Remove(item);
        }

        public void AddHandkarte(Karte item)
        {
            checkFlags();
            hand.Add(item);
            //Karte.hand.set
        }

        public int GetHandKartenZahl()
        {
            return hand.Count();
        }
        /// <summary>
        /// Gibt Anzahl der Handkarten zurück
        /// </summary>
        /// <returns></returns>
        public int GetHandkartenAnzahl()
        {
            return hand.Count();
        }

    }
}