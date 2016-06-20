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
        public bool hatSpion { get; set; }
        public bool hatEinbrecher { get; set; }
        public bool hatKampfkarte { get; set; }
        public Spieler spieler { get; set; }

        /// Handkarten
        List<Karte> hand = new List<Karte>();
        void checkStates()
        /// überprüft die flags hatSpion, hat Einbrecher, hatKampfkarte auf korrektheit
        void checkFlags()
        {
            hatKampfkarte = false;
            foreach (Karte k in hand)
            {
                if (k.id == 0)
                if (k.ID == 0)
                {
                    hatSpion = true;
                }
                else
                    hatSpion = false;
                if (k.id == 1)
                if (k.ID == 1)
                {
                    hatEinbrecher = true;
                }
                else
                    hatEinbrecher = false;
                if (k.id > 1)
                if (k.ID > 1 && k.ID < 14)
                {
                    hatKampfkarte = true;
                }
            }
        }

        public Karte RandomHandkarte()//Removed sogar eine handkarte, umbenennen evt in RemoveRandomHandkarte?
        /// Zieht zufällige Karte aus Spielerhand
        ///Zufällige Handkarte des Spielers
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
            checkStates();
            checkFlags();
            hand.Remove(item);
        }

        /// Legt Karte auf Hand
        ///Karte die auf die Hand kommt
        public void AddHandkarte(Karte item)
        {
            checkStates();
            checkFlags();
            hand.Add(item);
        }

        /// Gibt Anzahl der Handkarten zurück
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