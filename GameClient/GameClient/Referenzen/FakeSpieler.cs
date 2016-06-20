using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Referenzen {
    public class FakeSpieler {

        public FakeSpieler(int ident, bool präs) {
            würfelzahl = ident;
            punktzahl = 0;
            präsident = präs;
        }

        public FakeSpieler(int ident, int punktzahl, bool präs) {
            würfelzahl = ident;
            this.punktzahl = punktzahl;
            präsident = präs;
        }

        public int würfelzahl { get; set; }
        public int punktzahl { get; set; }
        public bool präsident { get; set; }
    }
}
