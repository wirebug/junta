using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class Hand
    {
<<<<<<< HEAD
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
=======
        bool hatSpion { get; set; }
        bool hatEinbrecher { get; set; }
        bool hatKampfkarte { get; set; }
        public Spieler spieler { get; set; }
        List<Karte> hand = new List<Karte>();
        void checkStates()
>>>>>>> Phasen
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

        /// <summary>
        /// Zieht zufälige Handkarte des Spielers
        /// </summary>
        /// <returns>Handkarte vom Typ Karte</returns>
        public Karte RandomHandkarte()
        {
            Random rng = new Random();
            int anzahl = GetHandkartenAnzahl();
            int index = rng.Next(anzahl);
            Karte ret = hand[index];
            RemoveHandkarte(hand[index]);
            return ret;
        }

        /// <summary>
        /// Nimmt Karte von Hand
        /// </summary>
        /// <param name="item">Handkarte die gelöscht werden soll</param>
        void RemoveHandkarte(Karte item)
        {
            checkFlags();
            hand.Remove(item);
        }
<<<<<<< HEAD
        /// <summary>
        /// Legt Karte auf Hand
        /// </summary>
        /// <param name="item">Karte die auf die Hand kommt</param>
=======

>>>>>>> Phasen
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

    }
}