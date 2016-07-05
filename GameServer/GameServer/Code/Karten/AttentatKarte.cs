using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code.Karten
{
    /*SPIELE VOR EINEM KAMPF
     * ZERSTÖRE EINE GEGNERISCHE MILIZ DEINER WAHL
     2X 
     */
        public class AttentatKarte : Karte
        {
        public AttentatKarte(int value, Hand hand, Deck deck) : base(value, hand, deck)
        {
            titel = "Attentat";
            kartenphase = "Spiele vor einem Kampf.";
            text = "Zerstöre eine gegnerische Flotte\neines Gegners deiner Wahl";
        }
        override public void Action()
        {
            /*SPIELE VOR EINEM KAMPF
                 * ZERSTÖRE EINE GEGNERISCHE MILIZ DEINER WAHL
                 2X 
                 */
        }
    }
}