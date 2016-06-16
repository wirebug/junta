using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class BestechungKarte : Karte
    {
        public BestechungKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {
            kartenname = "Bestechung";
            kartenphase = "Spiele vor einem Kampf.";
            kartentext = "Ignoriere in diesem Kampf die Flotten des Imperators.";
        }
        override public void Action()
        {
            //vor kampf
            // ignoriere Milizen des IMperators
            //1x
        }
    }
}