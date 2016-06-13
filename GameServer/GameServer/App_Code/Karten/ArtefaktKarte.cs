using System;
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
        ArtefaktKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            
        }
    }
}