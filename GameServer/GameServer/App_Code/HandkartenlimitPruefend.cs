using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class HandkartenlimitPruefend : Spielphase
    {
        public HandkartenlimitPruefend(Spielverwaltung sv) : base(sv){ }
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
            //nicht gültig
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
            //max 4 Karten in der Hand am Ende der Runde
            sv.rundenCount++;
        }
    }
}