using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class BestechungKarte : Karte
    {
        public BestechungKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "Bestechung";
            kartenphase = "Spiele vor einem Kampf.";
            text = "Ignoriere in diesem Kampf die\nFlotten des Imperators";
        }
        override public void Action()
        {
            //vor kampf
            // ignoriere Milizen des IMperators
            //1x
        }
    }
}