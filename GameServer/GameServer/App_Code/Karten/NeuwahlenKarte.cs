using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class NeuwahlenKarte : Karte
    {
        public NeuwahlenKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            kartenname = "Neuwahlen";
            kartenphase = "Diese Karte ist nicht Ausspielbar.";
            kartentext = "Wahlen...was für ein Altmodischer Quatsch.";
        }
        override public void Action()
        {
            //nix passiert
            //1x
        }
    }
}