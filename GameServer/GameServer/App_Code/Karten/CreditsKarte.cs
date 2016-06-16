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
        int Credits { get; set; }
                
        CreditsKarte(int value, int credits, Deck deck, Hand hand, Konto konto) : base(value,hand,deck,konto)
        { 
            this.Credits = credits;
        }
        public override void Action()
        {
            konto.increaseGuthaben(credits);

        }
        
    }
}