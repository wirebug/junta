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
    /// Interaction logic for VersprechenWählenWindow.xaml
    /// </summary>
    public partial class VersprechenWählenWindow : Window {

        private bool[] sp  = new bool[5];
        public Dictionary<int,int> versprechen = new Dictionary<int,int>();
        private List<ValueItem> valueList = new List<ValueItem>();
        private int[] spieler;

        public VersprechenWählenWindow(List<FakeKarte> karten,Spiel spiel) {
            InitializeComponent();
            spieler = new int[karten.Count];
            
            ValueItem temp;
            for(int sd = 0; sd < sp.Length; sd++) {
                sp[sd] = true;
            }
            sp[2] = false;
        /*
            for (int i = 0; i <= spiel.mitspieler.Count; i++) {
                FakeSpieler check;
                if (i == 0) {
                    check = spiel.selbst;
                } else {
                    check = spiel.mitspieler[i];
                }
                switch (check.würfelzahl) {
                    case 1: sp[0] = true; break;
                    case 2: sp[1] = true; break;
                    case 3: sp[2] = true; break;
                    case 4: sp[3] = true; break;
                    case 5: sp[4] = true; break;
                }
            }*/
            int r = 0;
            foreach(FakeKarte n in karten) {
                temp = new ValueItem(sp[0], sp[1], sp[2], sp[3], sp[4], r++, n.titel, n.text, n.id);
                valueList.Add(temp);
            }
            versprechenListBox.ItemsSource = valueList;
        }

        private class ValueItem {
            public ValueItem(bool sp1, bool sp2, bool sp3, bool sp4, bool sp5, int group, string titel, string text, int id) {
                this.sp1 = sp1;
                this.sp2 = sp2;
                this.sp3 = sp3;
                this.sp4 = sp4;
                this.sp5 = sp5;
                this.group = group;
                this.titel = titel;
                this.text = text;
                this.id = id;
            }
            public bool sp1 { get; set; }
            public bool sp2 { get; set; }
            public bool sp3 { get; set; }
            public bool sp4 { get; set; }
            public bool sp5 { get; set; }
            public int group { get; set; }
            public string titel { get; set; }
            public string text { get; set; }
            public int id { get; set; }
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            int i = 0;
            foreach(ValueItem d in valueList) {
                versprechen.Add(d.id, spieler[i++]);
            }

            i = 1;
            while(i <= 5) {
                if (sp[i]) {
                    bool set = false;
                    foreach (KeyValuePair<int, int> q in versprechen) {
                        if (q.Value == i) {
                            set = true;
                            break;
                        }
                        
                    }
                    if (!set) {
                        versprechen.Clear();
                        return; }
                }
                i++;
            }
            this.Hide();

        }

        private void radioButton_Checked(object sender, RoutedEventArgs a) {
            DependencyObject dep = (DependencyObject)a.OriginalSource;
            while ((dep != null) && !(dep is ListBoxItem)) {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null) {
                return;
            }
            int index = versprechenListBox.ItemContainerGenerator.IndexFromContainer(dep);
            spieler[index] = Int32.Parse((string)(sender as RadioButton).Content);
        }
    }
}
