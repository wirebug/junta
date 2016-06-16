using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class SpionKarte : Karte
    {
        public SpionKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            //spiele nachdem Spieler ihre Milizen festgelegt haben
            //gucke milizen der spieler nach
            //1x
        }
    }
}