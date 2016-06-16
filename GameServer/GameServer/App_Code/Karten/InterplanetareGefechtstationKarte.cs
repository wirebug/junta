using System;
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
        public InterplanetareGefechtstationKarte(int value,Hand hand,Deck deck,Konto konto) : base(value,hand,deck,konto)
        {
            
        }
        override public void Action()
        {
            //TODO
            /*
            hand.spieler.kampf.angriffswürfel.getSpieler
            //spiele vor einem Kampf
            //der Kampfwert erhöht sich um 3
            //2x
            */
        }

    }
}