﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class Ueberraschungsangriff : Karte
    {
        Ueberraschungsangriff(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            //vor kampf als angreifer
            //ignoriere die Gebäude des verteidigers
            //2x
        }
    }
}