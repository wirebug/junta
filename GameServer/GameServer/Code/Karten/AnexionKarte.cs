using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class AnexionKarte : Karte
    {
        
        public AnexionKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "Annexion";
            kartenphase = "Spiele als Angreifer vor einem Kampf, wenn der Verteidiger mehr Gebäude hat als du.";
            text = "Ist dein Angriff erfolgreich,\nübernimm 1 Gebäude des Verteidigers\nund verzichte auf seine\nHandkarte";
        }
        override public void Action()
        {
            //spiele vor einem kampf wenn der vert mehr gebäude hat als angreifer
            //ist angriff erfolgreich, nehme gebäude statt karte
            //1x
        }
    }
}