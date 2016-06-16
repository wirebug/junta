using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class PropagandaKampagneKarte : Karte
    {
        public PropagandaKampagneKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {
            kartenname = "Propaganda";
            kartenphase = "Spiele vor einem Kampf.";
            kartentext = "Alle verteidigenden Milizen haben in diesem Kampf den Wert 2.";
        }
        override public void Action()
        {
            //Wie bauernaufstand
            //vor kampf
            //Alle verteidigenden Milizen haben den wert 2(nicht würfeln)
            //2x
        }
    }
}