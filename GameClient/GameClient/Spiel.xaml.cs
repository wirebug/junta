using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameClient.Referenzen;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GameClient {
    /// <summary>
    /// Interaktionslogik für Spiel.xaml
    /// </summary>
    public partial class Spiel : Page {
        public HubProxy proxy;
        public ObservableCollection<FakeKarte> karten = new ObservableCollection<FakeKarte>();
        public ObservableCollection<FakeKarte> versprechen = new ObservableCollection<FakeKarte>();
        private bool istPräsident = false;
        public bool IstPräsident {
            get { return istPräsident; }
            set {
                istPräsident = value;
                istPräsidentChanged(); }
        }

        private int milizen;
        public int Milizen {
            get { return milizen; }
            set { milizen = value;
                milizenLabel.Content = milizen;
            }
        }

        public FakeSpieler selbst;
        public List<FakeSpieler> mitspieler;

        public Spiel() {          
            InitializeComponent();
            handGrid.DataContext = karten;
            versprechenGrid.DataContext = versprechen;
            //this.Loaded += (s,e) => { proxy = new HubProxy(this); };      
        }

        private void istPräsidentChanged() {
            if (istPräsident) {
                    präsidentLabel.Content = "Du bist Präsident";
                } else {
                    präsidentLabel.Content = "Du bist nicht Präsident";
                }
            
        }

        private void debugButton_Click(object sender, RoutedEventArgs e) {
            DebugWindow debug = new DebugWindow(this);
            debug.Show();
        }
    }
}
