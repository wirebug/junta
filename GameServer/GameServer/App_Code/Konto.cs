using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;

namespace GameServer.App_Code {
    public class Konto {
        Spieler spieler { get; set; }
<<<<<<< HEAD
        public int Credits { get; set; }


        /// <summary>
        /// erhöht Guthaben mit der Summe von Geldkarte x
        /// </summary>
        /// <param name="Credit">Credit der</param>
        public void increaseGuthaben(int Credit)
        {
            this.Credits += Credit;
        }

        /// <summary>
        /// veringert das Guthaben in Höhe des Parameters. Achtung Methode wirft ArgumentOutOfRangeException!
        /// </summary>
        /// <param name="Credit">Credits die abgezogen werden sollen</param>
        public void decreaseGuthaben(int Credit)
=======
        public int guthaben { get; set; }

        public void addGuthaben(Karte karte)
>>>>>>> master
        {
            if (Credits - Credit > 0)
            {
                this.Credits -= Credit;
            } else
            {
                throw new ArgumentOutOfRangeException("Nicht genug Guthaben!");
            }
        }
    }
}