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
using System.Windows.Shapes;
using GameClient.Referenzen;

namespace GameClient {
    /// <summary>
    /// Interaktionslogik für DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window {
        Spiel spiel;
        public DebugWindow(Spiel s) {
            InitializeComponent();
            spiel = s;
        }

        private void PräsidentÄndern_Click(object sender, RoutedEventArgs e) {
            if (spiel.IstPräsident) {
                spiel.IstPräsident = false;
            } else {
                spiel.IstPräsident = true;
            }
        }

        private void KarteHinzu_Click(object sender, RoutedEventArgs e) {
            FakeKarte a = new FakeKarte();
            Random b = new Random();
            a.id = b.Next(99);
            a.text = b.Next(342632543).ToString();
            a.titel = b.Next(34423).ToString();
            spiel.karten.Add(a);
        }

        private void KarteWeg_Click(object sender, RoutedEventArgs e) {
            spiel.karten.RemoveAt(0);
        }

        private void spielerHinzu_Click(object sender, RoutedEventArgs e) {
            spiel.mitspieler.Add(new FakeSpieler(3));
        }
    }
}
