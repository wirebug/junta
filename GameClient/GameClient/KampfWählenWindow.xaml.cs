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

namespace GameClient {
    /// <summary>
    /// Interaktionslogik für KampfWählenWindow.xaml
    /// </summary>
    public partial class KampfWählenWindow : Window {

        private bool sp1 =true;
        private bool sp2 = true;
        private bool sp3 = false;
        private bool sp4 = true;
        private bool sp5 = true;

        public int[] würfel;
        public KampfWählenWindow(int milizen) {
            InitializeComponent();
            List<ValueItem> valueList = new List<ValueItem>();
            ValueItem temp;
            würfel = new int[milizen];
            for(int i = 0; i < milizen; i++) {
                temp = new ValueItem(sp1, sp2, sp3, sp4, sp5, i);
                valueList.Add(temp);
            }
            kampfListBox.ItemsSource = valueList;

        }

        private class ValueItem {
            public ValueItem(bool sp1, bool sp2, bool sp3, bool sp4, bool sp5, int group) {
                this.sp1 = sp1;
                this.sp2 = sp2;
                this.sp3 = sp3;
                this.sp4 = sp4;
                this.sp5 = sp5;
                this.group = group;
            }
            public bool sp1 { get; set; }
            public bool sp2 { get; set; }
            public bool sp3 { get; set; }
            public bool sp4 { get; set; }
            public bool sp5 { get; set; }
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
    }
}
