using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaktionslogik für KampfWählenWindow.xaml
    /// </summary>
    public partial class KampfWählenWindow : Window {

        private int sp1 = 0;
        private int sp2 = 0;
        private int sp3 = 0;
        private int sp4 = 0;
        private int sp5 = 0;

        public int[] würfel;
        private static readonly int COLUMN_WIDTH = 40;
        public KampfWählenWindow(int milizen, Spiel spiel) {
            InitializeComponent();
            List<ValueItem> valueList = new List<ValueItem>();
            ValueItem temp;
            würfel = new int[milizen];
            for(int i = 0; i <= spiel.mitspieler.Count; i++) {
                FakeSpieler check;
                if(i == 0) {
                    check = spiel.selbst;
                } else {
                    check = spiel.mitspieler[i-1];
                }
                switch (check.würfelzahl) {
                    case 1: sp1 = COLUMN_WIDTH; break;
                    case 2: sp2 = COLUMN_WIDTH; break;
                    case 3: sp3 = COLUMN_WIDTH; break;
                    case 4: sp4 = COLUMN_WIDTH; break;
                    case 5: sp5 = COLUMN_WIDTH; break;
                }
            }
            for(int i = 0; i < milizen; i++) {
                temp = new ValueItem(sp1, sp2, sp3, sp4, sp5, i);
                valueList.Add(temp);
            }
            kampfListBox.ItemsSource = valueList;

        }

        private class ValueItem {
            public ValueItem(int sp1, int sp2, int sp3, int sp4, int sp5, int group) {
                this.sp1 = sp1;
                this.sp2 = sp2;
                this.sp3 = sp3;
                this.sp4 = sp4;
                this.sp5 = sp5;
                this.group = group;
            }
            public int sp1 { get; set; }
            public int sp2 { get; set; }
            public int sp3 { get; set; }
            public int sp4 { get; set; }
            public int sp5 { get; set; }
            public int group { get; set; }
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            for(int i = 0; i < würfel.Length; i++) {
                if(würfel[i] == 0) {
                    return;
                }
            }
            this.Hide();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs a) {
            DependencyObject dep = (DependencyObject)a.OriginalSource;
            while((dep != null) && !(dep is ListBoxItem)) {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if(dep == null) {
                return;
            }
            int index = kampfListBox.ItemContainerGenerator.IndexFromContainer(dep);
            würfel[index] = Int32.Parse((string)(sender as RadioButton).Content);
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            if (hilfeLabel.Visibility == Visibility.Hidden) {
                hilfeLabel.Visibility = Visibility.Visible;
            } else {
                hilfeLabel.Visibility = Visibility.Hidden;
            }
        }
    }
}
