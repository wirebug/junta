﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public abstract class Karte
    {
        private int value;
        public int id { get; set; }
        public Hand hand { get; set; }

        public Deck deck { get; set; }
        public string titel;
        public string text;
        public string kartenphase;
        public Karte(int id, Hand hand, Deck deck)
        {
            this.id = id;
            this.hand = hand;
            this.deck = deck;
        }

        public abstract void Action();
        
        public void WaehleAktion()
        {
            //code der in allen karten gleich ist
            Action();
        }

    }
}