using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class NeuwahlenKarte : Karte
    {
        public NeuwahlenKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "Neuwahlen";
            kartenphase = "Diese Karte ist nicht Ausspielbar.";
            text = "Wahlen...was für ein Altmodischer &#A;Quatsch.";
        }
        override public void Action()
        {
            //nix passiert
            //1x
        }
    }
}