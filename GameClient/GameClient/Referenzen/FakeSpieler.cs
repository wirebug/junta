using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Referenzen {
    public class FakeSpieler : INotifyPropertyChanged {

        public FakeSpieler(int ident, bool präs) {
            würfelzahl = ident;
            punktzahl = 1;
            präsident = präs;
            milizen = 1;
            anzKarten = 0;
        }

        public FakeSpieler(int ident, int punktzahl, bool präs) {
            würfelzahl = ident;
            this.punktzahl = punktzahl;
            präsident = präs;
        }

        public FakeSpieler(int ident, int punktzahl, bool präs, int milizen, int anzKarten) {
            würfelzahl = ident;
            this.punktzahl = punktzahl;
            präsident = präs;
            this.milizen = milizen;
            this.anzKarten = anzKarten;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            if(PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int würfelzahl { get; set; }
        private int Punktzahl;
        public int punktzahl {
            get { return Punktzahl; }
            set { Punktzahl = value;
                OnPropertyChanged("punktzahl");
            }
        }
        private bool Präsident;
        public String präs { get; set; }
        public bool präsident {
            get { return Präsident; }
            set { Präsident = value; if (value) { präs = "Ja"; } else { präs = "Nein"; }
                OnPropertyChanged("präs"); }
        }

        private int Milizen;
        public int milizen {
            get { return Milizen; }
            set {
                Milizen = value;
                OnPropertyChanged("milizen");
            }
        }

        private int AnzKarten;
        public int anzKarten {
            get { return AnzKarten; }
            set {
                AnzKarten = value;
                OnPropertyChanged("anzKarten");
            }
        }

        

    }
}
