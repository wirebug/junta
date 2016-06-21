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
        public bool isEmpty { get { return hand.Count == 0; } }

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
                if (k.ID == 0)
                {
                    hatSpion = true;
                }
                else
                    hatSpion = false;
                if (k.ID == 1)
                {
                    hatEinbrecher = true;
                }
                else
                    hatEinbrecher = false;
                if (k.ID > 1 && k.ID < 14)
                {
                    hatKampfkarte = true;
                }
            }
        }

        /// <summary>
        /// Zieht zufällige Karte aus Spielerhand
        /// </summary>
        /// <returns>Zufällige Handkarte des Spielers</returns>
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
        }
        /// <summary>
        /// Gibt Anzahl der Handkarten zurück
        /// </summary>
        /// <returns></returns>
        public int GetHandkartenAnzahl()
        {
            return hand.Count();
        }

        /// <summary>
        /// Gibt die Anzahl der auf der Hand befindlichen Karten zurück
        /// </summary>
        /// <returns>Anzahl der Handkarten</returns>
        public int GetHandKartenZahl()
        {
            return hand.Count();
        }
    }
}