﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    /* 1 SIEGPUNKT
     * 2X
     * 
     */
    public class ArtefaktKarte : Karte
    {
        public ArtefaktKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            kartenname = "Artefakt";
            kartenphase = "Solange du diese Karte auf der Hand hast gilt ihr Bonus.";
            kartentext = "Du erhälst einen Bonuspunkt.";
        }
        override public void Action()
        {
            
            //Spieler bekommt einen Siegpunkt
            //2x
        }
    }
}