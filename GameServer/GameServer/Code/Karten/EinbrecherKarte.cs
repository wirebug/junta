using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    public class EinbrecherKarte : Karte
    {
        public EinbrecherKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "Einbrecher";
            kartenphase = "Spiele unmittelbar, nachdem der Imperator seine Versprechungen gemacht hat.";
            text = "Schau dir die Handkarten eines &#A;Spielers deiner Wahl an und nimm eine &#A;davon auf die Hand.";
        }
        override public void Action()
        {
            //spiele nach versprechungen
            //schau auf handkarten eines spielers und nehme dann eine
            //1x
        }
    }
}