using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class AblenkungsmanoeverKarte : Karte
    {
        
        public AblenkungsmanoeverKarte(int value,Hand hand,Deck deck) : base(value,hand,deck) 
        {
            titel = "Finte";
            kartenphase = "Spiele unmittelbar nach einem Würfelwurf.";
            text = "Entweder ein einzelener oder alle\nWürfel dieses Wurfs werden neue\ngeworfen";
        }

        public override void Action()
        {
            //spiele nach Würfelwurf
            //Würfel neu würfeln
            //2x
        }
    }
}