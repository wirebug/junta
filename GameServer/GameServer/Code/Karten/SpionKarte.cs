﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class SpionKarte : Karte
    {
        public SpionKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "Spion";
            kartenphase = "Spiele unmittelbar, nachdem alle Spieler Ihre Milizen befehligt haben.";
            text = "Schau dir die Milizen aller\nMitspieler an, bevor du deine\neigenen befehligst. Die Mitspieler\ndürfenihre Milizen nicht mehr ändern";
        }
        override public void Action()
        {
            //spiele nachdem Spieler ihre Milizen festgelegt haben
            //gucke milizen der spieler nach
            //1x
        }
    }
}