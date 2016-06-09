﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    /*
     * SPIELE VOR EINEM KAMPF 
     * DEIN KAMPFWERT ERHÖHT SICH UM +3
     */
    public class InterplanetareGefechtstationKarte : Karte
    {
        InterplanetareGefechtstationKarte(int value) : base(value)
        {

        }
        public void Action(Kampf k)
        {
            InterplanetareGefechtsstation();
        }
        public void InterplanetareGefechtsstation()
        {
            
            //spiele vor einem Kampf
            //der Kampfwert erhöht sich um 3
        }

    }
}