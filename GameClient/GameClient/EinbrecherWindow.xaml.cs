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
    /// Interaktionslogik für EinbrecherWindow.xaml
    /// </summary>
    public partial class EinbrecherWindow : Window {
        public int erg { get; set; } = -1;
        public EinbrecherWindow(Spiel spiel) {
            InitializeComponent();
            foreach(FakeSpieler i in spiel.mitspieler) {
                switch (i.würfelzahl) {
                    case 1: rb1.IsHitTestVisible = true;break;
                    case 2: rb2.IsHitTestVisible = true; break;
                    case 3: rb3.IsHitTestVisible = true; break;
                    case 4: rb4.IsHitTestVisible = true; break;
                    case 5: rb5.IsHitTestVisible = true; break;
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            if(erg > -1) {
                this.Hide();
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e) {
            erg = Int32.Parse((string)(sender as RadioButton).Content);
        }
    }
}
