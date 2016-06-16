﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class EinbrecherKarte : Karte
    {
        public EinbrecherKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            kartenname = "Einbrecher";
            kartenphase = "Spiele unmittelbar, nachdem der Imperator seine Versprechungen gemacht hat.";
            kartentext = "Schau dir die Handkarten eines Spielers deineer Wahl an und nimm eine davon auf die Hand.";
        }
        override public void Action()
        {
            //spiele nach versprechungen
            //schau auf handkarten eines spielers und nehme dann eine
            //1x
        }
    }
}