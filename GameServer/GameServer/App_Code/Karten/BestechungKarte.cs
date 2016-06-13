using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class BestechungKarte : Karte
    {
        BestechungKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            //vor kampf
            // ignoriere Milizen des IMperators
            //1x
        }
    }
}