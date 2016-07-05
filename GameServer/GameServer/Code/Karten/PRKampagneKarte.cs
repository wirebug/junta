using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class PRKampagneKarte : Karte
    {
        public PRKampagneKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "PRKampagne";
            kartenphase = "Spiele beim Geld ausgeben.";
            text = "Du erhälst 1 Miliz gratis";
        }
        override public void Action()
        {
            //beim geldausgeben
            //bekomme eine miliz
            //1x
        }
    }
}