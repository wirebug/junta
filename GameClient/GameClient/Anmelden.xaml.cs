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

namespace GameClient {
    /// <summary>
    /// Interaktionslogik für Anmelden.xaml
    /// </summary>
    public partial class Anmelden : Page {
        public Anmelden() {
            InitializeComponent();
        }

        private void anmeldenButton_Click(object sender, RoutedEventArgs e) {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new Uri("Spiel.xaml", UriKind.RelativeOrAbsolute));
        }

    }
}
