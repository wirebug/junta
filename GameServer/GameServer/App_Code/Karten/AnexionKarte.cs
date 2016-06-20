using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class AnexionKarte : Karte
    {
        
        public AnexionKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            kartenname = "Annexion";
            kartenphase = "Spiele als Angreifer vor einem Kampf, wenn der Verteidiger mehr Gebäude hat als du.";
            kartentext = "Ist dein Angriff erfolgreich, übernimm 1 Gebäude des Verteidigers und verzichte auf seine Handkarte.";
        }
        override public void Action()
        {
            //spiele vor einem kampf wenn der vert mehr gebäude hat als angreifer
            //ist angriff erfolgreich, nehme gebäude statt karte
            //1x
        }
    }
}