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
        private And<object, object, object> and;

        int ID { get; set; }
        Hand hand { get; set; }
        Deck deck { get; set; }
        Konto konto { get; set; }
        public Karte(int id, Hand hand, Deck deck, Konto konto)
        {
            this.ID = ID;
            this.hand = hand;
            this.deck = deck;
            this.konto = konto;
        }

        public Karte(int value, And<object, object, object> and, Deck deck, Konto konto)
        {
            this.value = value;
            this.and = and;
            this.deck = deck;
            this.konto = konto;
        }

        public abstract void Action();
        
        public void WaehleAktion()
        {
            //
            Action();
        }

    }
}