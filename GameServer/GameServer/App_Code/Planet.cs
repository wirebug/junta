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
        /// Spieler ID
        /// </summary>
        public int würfelzahl { get; set; }
        public Spieler spieler { get; set; }
    }
}