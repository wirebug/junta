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
        List<Karte> hand = new List<Karte>();
        void checkStates()
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

        public Karte RandomHandkarte()//Removed sogar eine handkarte, umbenennen evt in RemoveRandomHandkarte?
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
            hand.Remove(item);
        }

        public void AddHandkarte(Karte item)
        {
            checkStates();
            hand.Add(item);
            //Karte.hand.set
        }

        public int GetHandKartenZahl()
        {
            return hand.Count();
        }

    }
}