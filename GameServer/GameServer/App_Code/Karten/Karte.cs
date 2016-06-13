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
        int ID { get; set; }
        private Hand phand;
        public Hand Phand
        {
           get
            {
                return Phand;
            }
            set
            {
                phand = Phand;
            }

        }

        Deck deck { get; set; }
        Konto konto { get; set; }
        public Karte(int id, Hand hand, Deck deck, Konto konto)
        {
            this.ID = id;
            this.Phand = hand;
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