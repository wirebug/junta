using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;

namespace GameServer.App_Code {
    public class Konto {
        Spieler spieler { get; set; }
        public int guthaben { get; set; }


        /// <summary>
        /// erhöht Guthaben mit der Summe von Geldkarte x
        /// </summary>
        /// <param name="karte">Muss eine Karte vom Typ Geldkarte sein</param>
        public void increaseGuthaben(CreditsKarte karte)
        {
            guthaben += karte. ; //guthaben attribut fehlt noch
        }

        /// <summary>
        /// veringert das Guthaben in Höhe des Parameters
        /// </summary>
        /// <param name="Summe">Credits die abgezogen werden sollen</param>
        public void decreaseGuthaben(int Summe)
        {
            if (guthaben - Summe > 0)
            {
                guthaben -= Summe;
            } else
            {
                throw ArgumentOutOfRangeException();
            }
        }
    }
}