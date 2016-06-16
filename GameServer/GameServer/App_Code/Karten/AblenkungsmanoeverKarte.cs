using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class AblenkungsmanoeverKarte : Karte
    {
        public AblenkungsmanoeverKarte(int value,Hand hand,Deck deck,Konto konto) : base(value,hand,deck,konto) 
        {
            
        }

        public override void Action()
        {
            //spiele nach Würfelwurf
            //Würfel neu würfeln
            //2x
        }
    }
}