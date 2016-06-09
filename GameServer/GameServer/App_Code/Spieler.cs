using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Spieler {
        int flotten { get; set; }
        int punkte { get; set; }
        bool imperator { get; set; }
        Planet planet { get; set; }
        Hand hand { get; set; }
    }
}