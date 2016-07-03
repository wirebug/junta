using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Referenzen {
    public class FakeKarte {
        public string titel { get; set; }
        public string text { get; set; }
        public int id { get; set; }

        public FakeKarte() { }
        public FakeKarte(string titel, string text, int id) {
            this.titel = titel;
            this.text = text;
            this.id = id;
        }
    }
}
