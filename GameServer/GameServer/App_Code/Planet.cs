using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Planet {

        /// <summary>
        /// Anzahl der Gebäude aka Planeten
        /// </summary>
        public int gebäude{get;set;}

        /// <summary>
        /// Spieler id
        /// </summary>
        public int würfelzahl { get; set; }
        public static int counter = 1;
        public Spieler spieler { get; set; }

        public Planet(Spieler spieler)
        {
            this.spieler = spieler;
            würfelzahl = counter++;
            gebäude = 1;
        }
    }
}