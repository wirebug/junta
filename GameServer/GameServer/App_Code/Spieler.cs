﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Spieler {
        public static int spielerCount = 0;
        public int ID;
        public int Kampfmodifikator { get; set; }//Kampkarte InterplanetareGefechtsstations - bonus
        public List<int> versprechungen;//Versprechungen sind noch nicht in der Hand und werden als ids übergeben
        public int flotten { get; set; }
        public int punkte { get; set; }
        public bool imperator { get; set; }
        public Planet planet { get; set; }
        public Hand hand { get; set; }
        public Konto konto { get; set; }
        public Kampf kampf { get; set; }

        //Konstruktor NICHT FERTIG!!!
        public Spieler(int flotten, int punkte, bool imperator, Planet planet, Hand hand)
        {
            Kampfmodifikator = 0;
        }
        public Spieler()
        {
            spielerCount++;
            ID = spielerCount;
        }
    }
}