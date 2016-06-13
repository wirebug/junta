using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    public class EinbrecherKarte : Karte
    {
        public EinbrecherKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

        }
        override public void Action()
        {
            //spiele nach versprechungen
            //schau auf handkarten eines spielers und nehme dann eine
            //1x
        }
    }
}