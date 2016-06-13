using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class KarteZiehend : Spielphase
    {
        public KarteZiehend(Spielverwaltung sv):base(sv){}
        public override void KarteZiehen(Spieler spieler)
        {
            foreach(Spieler s in sv.reihenfolge)
            {
                if (!s.imperator)
                {
                    if (sv.rundenCount == 1)
                    {
                        //2 Karten ziehen
                        s.hand.AddHandkarte(sv.deck.Ziehen());
                        s.hand.AddHandkarte(sv.deck.Ziehen());
                    }
                    else
                    {
                        //1 Karte ziehen
                        s.hand.AddHandkarte(sv.deck.Ziehen());
                    }
                }
                else
                {
                    //Spielerzahl + 2 Karten ziehen
                    foreach(Spieler si in sv.reihenfolge)//Spieleranzahl einfacher auslesen?
                    {
                        s.hand.AddHandkarte(sv.deck.Ziehen());
                    }
                    s.hand.AddHandkarte(sv.deck.Ziehen());
                    s.hand.AddHandkarte(sv.deck.Ziehen());
                }

            }
        }
        public override void VersprechungenMachen(Spieler spieler)
        {
            //nicht gültig
            //throw (new InvalidOperationException("VersprechungMethode"));
        }
        public override void FlottenBefehligen(Spieler spieler)
        {
            //nicht Gültig
        }
        public override void KaempfeAustragen(Spieler spieler)
        {
            //nicht Gültig
        }
        public override void GeldAusgeben(Spieler spieler)
        {
            //nicht Gültig
        }
        public override void HandkartenlimitPruefen(Spieler spieler)
        {
            //nicht Gültig
        }
    }
}