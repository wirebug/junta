using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Referenzen {
    public class FakeSpieler {

        public FakeSpieler(int ident) {
            würfelzahl = ident;
            punktzahl = 0;
        }

        public FakeSpieler(int ident, int punktzahl) {
            würfelzahl = ident;
            this.punktzahl = punktzahl;
        }

        public int würfelzahl { get; set; }
        public int punktzahl { get; set; }
    }
}
