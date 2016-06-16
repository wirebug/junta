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
            
            hand.spieler.kampf.angriffswürfel[hand.spieler] += 3;
            //ACHTUNG KARTE WIRD VOR DEM KAMPF GESPIELT ABER ERST BEIM WÜRFELN VERRECHNET !

            //spiele vor einem Kampf
            //der Kampfwert erhöht sich um 3
            //2x
            
        }

    }
}