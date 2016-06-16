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
            AttentatKarte(int value, Hand hand, Deck deck, Konto konto) : base(value, hand, deck, konto)
        {

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