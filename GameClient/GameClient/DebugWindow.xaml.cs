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
            spiel.proxy.addSpieler();
        }

        private void KarteHinzu_Click(object sender, RoutedEventArgs e) {
           
        }

        private void KarteWeg_Click(object sender, RoutedEventArgs e) {
            spiel.karten.RemoveAt(0);
        }

        private void spielerHinzu_Click(object sender, RoutedEventArgs e) {
            spiel.mitspieler.Add(new FakeSpieler(3, 1, true, 3,5));
        }

        private void button_Click(object sender, RoutedEventArgs e) {

        }

        private void KampfWählen_Click(object sender, RoutedEventArgs e) {
            spiel.mitspieler[0].punktzahl = 2;
           spiel.mitspielerGrid.GetBindingExpression(DataGrid.ItemsSourceProperty).UpdateSource();
        }

        private void versprechenWählen_Click(object sender, RoutedEventArgs e) {
            FakeKarte a = new FakeKarte();
            a.id =3;
            a.text = "ds";
            a.titel = "sfs";
            FakeKarte b = new FakeKarte("dsd", "sdsd", 32);
            FakeKarte c = new FakeKarte("sdsd", "sds", 34343);
            FakeKarte d = new FakeKarte("sd", "sd", 2);
            FakeKarte f = new FakeKarte("sdaa", "sds", 5);
            FakeKarte g = new FakeKarte("sds", "qq", 54);
            FakeKarte h = new FakeKarte("fd", "sds", 3423);
            List<FakeKarte> z = new List<FakeKarte>();
            z.Add(a);
            z.Add(b);
            z.Add(c);
            z.Add(d);
            z.Add(f);
            z.Add(g);
            z.Add(h);
            VersprechenWählenWindow ac = new VersprechenWählenWindow(z, spiel);
            ac.Show();
        }

        private void startButton_Click(object sender, RoutedEventArgs e) {
            spiel.proxy.Start();
        }
    }
}
