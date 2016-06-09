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
        int Value { get; set; }
        CreditsKarte(int value, int credits) : base(value)
        {
            Value = credits;
        }
        public override void Action()
        {
            Credits(Value);
        }
        public void Credits(int value)
        {
            
        }
    }
}