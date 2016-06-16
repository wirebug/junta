using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;

namespace GameServer.App_Code
{
    public class Hand
    {
        public bool hatSpion { get; set; }
        public bool hatEinbrecher { get; set; }
        public bool hatKampfkarte { get; set; }
        public Spieler spieler { get; set; }

        /// <summary>
        /// Handkarten
        /// </summary>
        List<Karte> hand = new List<Karte>();
        
        /// <summary>
        /// überprüft die flags hatSpion, hat Einbrecher, hatKampfkarte auf korrektheit
        /// </summary>
        void checkFlags()
        {
            hatKampfkarte = false;
            foreach (Karte k in hand)
            {
                if (k.id == 0)
                {
                    hatSpion = true;
                }
                else
                    hatSpion = false;
                if (k.id == 1)
                {
                    hatEinbrecher = true;
                }
                else
                    hatEinbrecher = false;
                if (k.id > 1)
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
        /// <summary>
        /// Legt Karte auf Hand
        /// </summary>
        /// <param name="item">Karte die auf die Hand kommt</param>
        public void AddHandkarte(Karte item)
        {
            checkFlags();
            hand.Add(item);
            //Karte.hand.set
        }
        /// <summary>
        /// Gibt Anzahl der Handkarten zurück
        /// </summary>
        /// <returns></returns>
        public int GetHandkartenAnzahl()
        {
            return hand.Count();
        }

        public int GetHandKartenZahl()
        {
            return hand.Count();
        }

    }
}