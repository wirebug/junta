﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class SpendeKarte : Karte
    {
        public SpendeKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "Spende";
            kartenphase = "Spiele beim Geld ausgeben.";
            text = "Du erhälst ein Gebäude gratis";
        }
        override public void Action()
        {
            hand.spieler.planet.gebäude += 1;
            //beim geldausgeben
            //du bekommst ein Gebäude umsonst dazu
            //1x
        }
    }
}