using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class Ueberraschungsangriff : Karte
    {
        public Ueberraschungsangriff(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            kartenname = "Überraschungsangriff";
            kartenphase = "Spiele als Angreifer vor einem Kampf.";
            kartentext = "Ignoriere in diesem Kampf alle Gebäude des Verteidigers.";
        }
        override public void Action()
        {
            //vor kampf als angreifer
            //ignoriere die Gebäude des verteidigers
            //2x
        }
    }
}