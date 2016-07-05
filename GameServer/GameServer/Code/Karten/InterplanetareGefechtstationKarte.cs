using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    /*
     * SPIELE VOR EINEM KAMPF 
     * DEIN KAMPFWERT ERHÖHT SICH UM +3
     */
    public class InterplanetareGefechtstationKarte : Karte
    {
        public InterplanetareGefechtstationKarte(int value,Hand hand,Deck deck) : base(value,hand,deck)
        {
            titel = "Raumstation";
            kartenphase = "Spiele vor einem Kampf.";
            text = "Dein Kampfwert erhöht sich beim\nnächsten Kampf um +3";
        }
        override public void Action()
        {
            hand.spieler.kampf.addAngriffWert(hand.spieler,3);
            //ACHTUNG KARTE WIRD VOR DEM KAMPF GESPIELT ABER ERST BEIM WÜRFELN VERRECHNET !

            //spiele vor einem Kampf
            //der Kampfwert erhöht sich um 3
            //2x
            
        }

    }
}