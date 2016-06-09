using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public abstract class Spielphase
    {
        protected Spielverwaltung sv;
        public Spielphase(Spielverwaltung sv)
        {
            this.sv = sv;
            sv.rundenCount = 1;
        }

        public abstract void KarteZiehen(Spieler spieler);
        public abstract void VersprechungenMachen(Spieler spieler);
        public abstract void FlottenBefehligen(Spieler spieler);
        public abstract void KaempfeAustragen(Spieler spieler);
        public abstract void GeldAusgeben(Spieler spieler);
        public abstract void HandkartenlimitPruefen(Spieler spieler);
    }
    /* Beim gestalten der GUI wird entschieden ob die einzelnen phasen dafür verantwortlich sind
     * ob die Funktionalitäten blockiert werden
     * 
     * Exceptionklasse für jede Phase zum throwen
     */
}