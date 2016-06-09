using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class VersprechungenMachend : Spielphase
    {
        //Konstruktor
        public VersprechungenMachend(Spielverwaltung sv) : base(sv){}
        public override void KarteZiehen(Spieler spieler)
        {
            //nicht gültig
        }
        public override void VersprechungenMachen(Spieler spieler)
        {
            //mindestens 1 Versprechungen pro Spieler aus Imperator-Hand vom Imperator

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