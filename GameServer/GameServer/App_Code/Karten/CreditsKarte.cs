using GameServer.App_Code;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class CreditsKarte : Karte
    {
        public int Credits { get; set; }
        
        public CreditsKarte(int value, int credits,Hand hand,Deck deck, Konto konto) : base(value,hand,deck,konto)
        {
            this.Credits = credits;
            kartenname = "Credits";
            kartenphase = "Spiele in der Geld Ausgeben Phase.";
            kartentext = "Diese Karte gibt dir " + Credits + " Credits.";
        }
        public override void Action()
        {
            konto.
            //12x 1000 
            //11x 2000
            //3x 3000
        }
        
    }
}