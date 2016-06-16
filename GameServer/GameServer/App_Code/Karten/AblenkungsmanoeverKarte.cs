using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class AblenkungsmanoeverKarte : Karte
    {
        
        public AblenkungsmanoeverKarte(int value,Hand hand,Deck deck) : base(value,hand,deck) 
        {
            kartenname = "Ablenkungsmanöver";
            kartenphase = "Spiele unmittelbar nach einem Würfelwurf.";
            kartentext = "Entweder ein einzlener oder alle Würfel dieses Wurfs werden neue geworfen.";
        }

        public override void Action()
        {
            //spiele nach Würfelwurf
            //Würfel neu würfeln
            //2x
        }
    }
}