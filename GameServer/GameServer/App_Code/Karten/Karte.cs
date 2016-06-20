using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public abstract class Karte
    {
        private int value;
        public int ID { get; set; }
        public Hand hand { get; set; }

        public Deck deck { get; set; }
        public string kartenname;
        public string kartentext;
        public string kartenphase;
        public Karte(int id, Hand hand, Deck deck)
        {
            this.ID = id;
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