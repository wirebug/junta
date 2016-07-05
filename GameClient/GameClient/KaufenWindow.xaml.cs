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

namespace GameClient {
    /// <summary>
    /// Interaktionslogik für KaufenWindow.xaml
    /// </summary>
    public partial class KaufenWindow : Window {
        public int option = -1;
        public KaufenWindow(int money, bool second) {
            InitializeComponent();
            moneyLabel.Content = money;
            if (money < 1000) {
                rb3.IsHitTestVisible = false;
                rb2.IsHitTestVisible = false;
                rb1.IsHitTestVisible = false;
            } else if (money < 2000) {
                rb3.IsHitTestVisible = false;
                rb2.IsHitTestVisible = false;
            } else if (money < 4000) {
                rb3.IsHitTestVisible = false;
            }
            if (second) {
                rb1.IsHitTestVisible = false;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            if (option > -1) {
                this.Hide();
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e) {
            option = Int32.Parse((string)(sender as RadioButton).Content);
        }

        private void button_Click2(object sender, RoutedEventArgs e) {
            if (hilfeLabel.Visibility == Visibility.Hidden) {
                hilfeLabel.Visibility = Visibility.Visible;
            } else {
                hilfeLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
