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
        private int milizen;
        public int Milizen {
            get { return milizen; }
            set { milizen = value;
                Dispatcher.BeginInvoke(new Action(() => milizenLabel.Content = milizen)).Wait();           
            }
        }

        public FakeSpieler selbst;
        public ObservableCollection<FakeSpieler> mitspieler = new ObservableCollection<FakeSpieler>();
        private List<Rectangle> gebäude = new List<Rectangle>();
        private int gebäudeCount = 1;

        public Spiel() {          
            InitializeComponent();
            gebäude.Add(gebäude1);
            gebäude.Add(gebäude2);
            gebäude.Add(gebäude3);
            gebäude.Add(gebäude4);
            gebäude.Add(gebäude5);
            handGrid.DataContext = karten;
            versprechenGrid.DataContext = versprechen;
            mitspielerGrid.DataContext = mitspieler;
            this.Loaded += (s,e) => { proxy = new HubProxy(this); proxy.addSpieler(); };
            
        }

        public void IstPräsident() {
            if (selbst.präsident) {               
                    Dispatcher.BeginInvoke(new Action(() => präsidentLabel.Content = "Du bist Imperator")).Wait();
            } else {
                Dispatcher.BeginInvoke(new Action(() => präsidentLabel.Content = "Du bist nicht Imperator")).Wait();
            }
            
        }

        private void debugButton_Click(object sender, RoutedEventArgs e) {
            DebugWindow debug = new DebugWindow(this);
            debug.Show();
        }

        public void addGebäude() {
            Dispatcher.BeginInvoke(new Action(() => gebäude[gebäudeCount++ -1].Visibility = Visibility.Visible)).Wait();           
        }

        public void removeGebäude() {
            Dispatcher.BeginInvoke(new Action(() => gebäude[gebäudeCount-- -2].Visibility = Visibility.Hidden)).Wait();           
        }

        public void initGUI() {
            
            Dispatcher.BeginInvoke(new Action(() => würfelzahlLabel.Content = selbst.würfelzahl)).Wait();
            IstPräsident();
            Milizen = selbst.milizen;
        }
    }
}
