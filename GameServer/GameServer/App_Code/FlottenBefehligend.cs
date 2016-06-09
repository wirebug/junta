using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class FlottenBefehligend : Spielphase
    {
        public FlottenBefehligend(Spielverwaltung sv) : base(sv){ }
        public override void KarteZiehen(Spieler spieler)
        {
            //nicht gültig
        }
        public override void VersprechungenMachen(Spieler spieler)
        {
            //nicht gültig
        }
        public override void FlottenBefehligen(Spieler spieler)
        {
            //Imperator Flotten alle 1 (defending)
            //Alle Spielerflotten befehligen
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