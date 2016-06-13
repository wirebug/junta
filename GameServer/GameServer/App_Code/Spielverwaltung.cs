using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class Spielverwaltung
    { 
        public List<Spieler> reihenfolge { get; }
        JuntaHub _hub;
        public Deck deck { get; }
        public int rundenCount { get; set; }
    }

}