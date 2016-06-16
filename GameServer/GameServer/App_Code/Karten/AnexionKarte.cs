using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class AnexionKarte : Karte
    {
        public AnexionKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            //spiele vor einem kampf wenn der vert mehr gebäude hat als angreifer
            //ist angriff erfolgreich, nehme gebäude statt karte
            //1x
        }
    }
}