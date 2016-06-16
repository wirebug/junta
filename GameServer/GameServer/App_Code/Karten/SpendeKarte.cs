using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class SpendeKarte : Karte
    {
        public SpendeKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            kartenname = "Spende";
            kartenphase = "Spiele beim Geld ausgeben.";
            kartentext = "Du erhälst ein Gebäude gratis.";
        }
        override public void Action()
        {
            //beim geldausgeben
            //du bekommst ein Gebäude umsonst dazu
            //1x
        }
    }
}