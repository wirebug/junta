using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code.Karten
{
    /*SPIELE VOR EINEM KAMPF
     * ZERSTÖRE EINE GEGNERISCHE MILIZ DEINER WAHL
     2X 
     */
        public class AttentatKarte : Karte
        {
        public AttentatKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {
            kartenname = "Attentat";
            kartenphase = "Spiele vor einem Kampf.";
            kartentext = "Zerstöre eine gegnerische Flotte eines Gegners deiner Wahl.";
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