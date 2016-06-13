using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class SpendeKarte : Karte
    {
        SpendeKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            //beim geldausgeben
            //du bekommst ein Gebäude umsonst dazu
            //1x
        }
    }
}