using GameServer.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class CreditsKarte : Karte
    {
        public int Credits { get; set; }
        
        public CreditsKarte(int value, int credits,Hand hand,Deck deck) : base(value,hand,deck)
        {
            this.Credits = credits;
            titel = "Credits";
            kartenphase = "Spiele in der Geld Ausgeben Phase.";
            text = "Diese Karte gibt dir " + Credits + " Credits.";
        }
        public override void Action()
        {
            //12x 1000 
            //11x 2000
            //3x 3000
        }
        
    }
}