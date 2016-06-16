using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Konto {
        Spieler spieler { get; set; }
        public int guthaben { get; set; }

        public void addGuthaben(Karte karte)
        {
            guthaben += karte.creditskarte;
        }
    }
}