using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class PRKampagneKarte : Karte
    {
        public PRKampagneKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            //beim geldausgeben
            //bekomme eine miliz
            //1x
        }
    }
}