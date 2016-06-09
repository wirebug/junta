using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Konto {
        Spieler spieler { get; set; }
        int guthaben { get; set; }

        void addGuthaben(Karte karte)
        {
            guthaben += karte.creditskarte;
        }
    }
}