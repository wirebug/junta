using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class SpionKarte : Karte
    {
        public SpionKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            kartenname = "Spion";
            kartenphase = "Spiele unmittelbar, nachdem alle Spieler Ihre Milizen befehligt haben.";
            kartentext = "Schau dir die Milizen aller Mitspieler an, bevor du deine eigenen befehligst. Die Mitspieler dürfen ihre Milizen nicht emhr ändern.";
        }
        override public void Action()
        {
            //spiele nachdem Spieler ihre Milizen festgelegt haben
            //gucke milizen der spieler nach
            //1x
        }
    }
}