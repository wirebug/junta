using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class AblenkungsmanoeverKarte : Karte
    {
        AblenkungsmanoeverKarte(int value,Hand hand,Deck deck,Konto konto) : base(value,hand,deck,konto) 
        {
            
        }

        public override void Action()
        {
            throw new NotImplementedException();
        }
    }
}