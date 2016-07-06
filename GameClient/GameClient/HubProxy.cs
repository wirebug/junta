using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameClient.Referenzen;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Threading;

namespace GameClient {
    public class HubProxy {
        IHubProxy proxy;
        Spiel spiel;
        public HubProxy(Spiel spiel) {
            this.spiel = spiel;
            string url = @"http://localhost:3204/signalr/hubs";
            var connection = new HubConnection(url);
            proxy = connection.CreateHubProxy("JuntaHub");

            proxy.On<int, int, string, string>("AddKarte", (ident, id, titel, text) => AddKarte(ident, id, titel, text));
            proxy.On<int, string>("VersprechenWählen", (idSpieler, json) => VersprechenWählen(idSpieler, json));
            proxy.On<int, int, string, string>("AddVersprechung", (ident, id, titel, text) => AddVersprechung(ident, id, titel, text));
            proxy.On<int>("RemoveVersprechen", (idSpieler) => RemoveVersprechen(idSpieler));
            proxy.On<int>("AlleVersprechenEnfernen", (id) => RemoveAllVersprechen());
            proxy.On<int, int>("KampfWählen", (idSpieler, flotten) => KampfWählen(idSpieler, flotten));
            proxy.On<int, int>("RemoveKarte", (idSpieler, id) => RemoveKarte(idSpieler, id));
            proxy.On<int, int>("SetImperator", (neu, alt) => SetImperator(neu, alt));
            proxy.On<int>("SpieleSpion", (idSpieler) => SpieleSpion(idSpieler));
            proxy.On<int>("SpieleEinbrecher", (idSpieler) => SpieleEinbrecher(idSpieler));
            proxy.On<int, int, bool>("Kaufen", (idSpieler, credits, second) => Kaufen(idSpieler, credits, second));
            proxy.On<int>("AddGebäude", (idSpieler) => AddGebäude(idSpieler));
            proxy.On<int>("RemoveGebäude", (idSpieler) => RemoveGebäude(idSpieler));
            proxy.On<int>("addMiliz", (idSpieler) => addMiliz(idSpieler));
            proxy.On<int>("removeMiliz", (idSpieler) => removeMiliz(idSpieler));
            proxy.On<int, int, bool, int, int>("setSpieler", (idSpieler, punkte, imp, flotten, anzK) => setSpieler(idSpieler, punkte, imp, flotten, anzK));
            proxy.On<int, bool>("AddOtherPlayer", (ident, imp) => AddOtherPlayers(ident, imp));
            proxy.On<string>("message", (text) => message(text));
            proxy.On<int>("won", (ident) => won(ident));
            proxy.On<int, string>("ZeigeNachricht", (ident, mesage) => ZeigeNachricht(ident, mesage));   
            connection.Start().Wait();
        }
        public void message(string ab) {
            MessageBox.Show(ab, "Message", MessageBoxButton.OK);
        }
        public bool IsPlayer(int ident) {
            if(spiel.selbst.würfelzahl == ident) {
                return true;
            } else {
                return false;
            }
        }

        public void AddOtherPlayers(int ident, bool präs) {
            if (!IsPlayer(ident)) {
                foreach (FakeSpieler s in spiel.mitspieler) {
                    if (ident == s.würfelzahl) {
                        return;
                    }
                }
                
                spiel.Dispatcher.BeginInvoke(new Action(() => spiel.mitspieler.Add(new FakeSpieler(ident, präs)))).Wait();
            }
        }

        public void UpdatePunkteMitspieler(int ident, int pktzl) {
            if (!IsPlayer(ident)) {
                foreach (FakeSpieler s in spiel.mitspieler) {
                    if (ident == s.würfelzahl) {
                        s.punktzahl = pktzl;
                    }
                }
            }
        }

        /// <summary>
        /// Nimmt Antwort des Imperators entgegen und schickt die Infos an JuntaHub zu VersprechenVerarbeiten
        /// </summary>
        /// <param name="idSpieler">ident Spieler</param>
        /// <param name="idKarte">id Karte</param>
        public void VersprechenWählen(int idSpieler, string json) {
            if (spiel.selbst.präsident) {
                List<FakeKarte> FKarteListe = new List<FakeKarte>();
                FKarteListe = JsonConvert.DeserializeObject<List<FakeKarte>>(json);

                VersprechenWählenWindow vww = null;
                 spiel.Dispatcher.Invoke(() => {
                    vww = new VersprechenWählenWindow(FKarteListe, spiel);


                    if (vww.ShowDialog() == false) {
                        string j = JsonConvert.SerializeObject(vww.versprechen);
                        proxy.Invoke("VersprechenVerarbeiten", j);
                    }
                });        
            }
            
        }
        /// <summary>
        /// fügt dem Spiel eine neue FakeKarte hinzu
        /// </summary>
        /// <param name="ident">Würfelzahl bzw ID des Spielers</param>
        /// <param name="id">ID der Karte</param>
        /// <param name="titel">Titel der Karte</param>
        /// <param name="text">Text der Karte</param>
        public void AddKarte(int ident,int id, string titel, string text) {
            if (IsPlayer(ident)){
                FakeKarte temp = new FakeKarte();
                temp.id = id;
                temp.text = text;
                temp.titel = titel;
                spiel.Dispatcher.BeginInvoke(new Action(() => spiel.karten.Add(temp))).Wait();
            } else {
                foreach (FakeSpieler f in spiel.mitspieler) {
                    if (f.würfelzahl == ident) {
                        f.anzKarten++;
                    }
                }
            }
        }
        
        public void AddVersprechung(int ident, int id, string titel, string text)
        {
            if (IsPlayer(ident))
            {
                FakeKarte temp = new FakeKarte();
                temp.id = id;
                temp.text = text;
                temp.titel = titel;
                spiel.Dispatcher.BeginInvoke(new Action(() => spiel.versprechen.Add(temp))).Wait();
                
            }
        }

        public void updateMoney(int money) {

        }

        public void RemoveKarte(int ident, int id) {
            if (IsPlayer(ident)) {
                foreach(FakeKarte s in spiel.karten) {
                    if(s.id == id) {
                        spiel.Dispatcher.BeginInvoke(new Action(() => spiel.karten.Remove(s)));
                        return;
                    }
                }
            } else {
                foreach (FakeSpieler f in spiel.mitspieler) {
                    if (f.würfelzahl == ident) {
                        f.anzKarten--;
                    }
                }
            }
        }

        public void VersprechenBekommen(int ident) {
            if (IsPlayer(ident)){
                foreach(FakeKarte s in spiel.versprechen) {
                    spiel.Dispatcher.BeginInvoke(new Action(() => spiel.versprechen.Remove(s))).Wait();
                    spiel.Dispatcher.BeginInvoke(new Action(() => spiel.karten.Add(s))).Wait();                   
                }
            }
        }
        
        public void RemoveVersprechen(int ident) {
            if (IsPlayer(ident)) {
                spiel.Dispatcher.BeginInvoke(new Action(() => spiel.versprechen.Clear())).Wait();
            }
        }

        public void RemoveAllVersprechen() {
            spiel.Dispatcher.BeginInvoke(new Action(() => spiel.versprechen.Clear())).Wait();
        }

        public void KampfWählen(int ident,int flottenAnzahl) {
            if (IsPlayer(ident)) {
                /*    spiel.Dispatcher.InvokeAsync(() => {
                        KampfWählenWindow kww = new KampfWählenWindow(flottenAnzahl, spiel);
                        if (kww.ShowDialog() == false) {
                            proxy.Invoke("FlottenVerarbeiten", ident, kww.würfel);
                        }
                    });
                 */
                KampfWählenWindow kww = null;
                spiel.Dispatcher.BeginInvoke(new Action(() =>  kww = new KampfWählenWindow(flottenAnzahl, spiel))).Wait();
                spiel.Dispatcher.BeginInvoke(new Action(() => kww.ShowDialog())).Wait();
                proxy.Invoke("FlottenVerarbeiten", ident, kww.würfel);
            }
        }
        
        public void SpieleSpion(int ident) {
            if (IsPlayer(ident)) {
                string message = "Möchtest du den Spion spielen?";
                string caption = "Junta";
                MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes) {
                    proxy.Invoke("SpionAntwort", true);
                } else {
                    proxy.Invoke("SpionAntwort", false);
                }
            }
        }

        public void SpieleEinbrecher(int ident) {
            if (IsPlayer(ident)) {
                string message = "Möchtest du den Einbrecher spielen?";
                string caption = "Junta";
                spiel.Dispatcher.Invoke(() => {
                    MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes) {
                    //Fenster mit spieleranzahl an radiobuttons
                    //EinbrecherWindow
                    
                        EinbrecherWindow ew = new EinbrecherWindow(spiel);
                        if (ew.ShowDialog() == false) {
                            proxy.Invoke("EinbrecherAntwort", true, ew.erg, spiel.selbst.würfelzahl);
                        }
                        //Methode im EW auswahl eines spielers der beklaut werden soll.


                    } else {
                        proxy.Invoke("EinbrecherAntwort", false);
                    }
                });
            }
        }

        public  void ZeigeNachricht(int ident, string message) {
            if (IsPlayer(ident)) {
                spiel.Dispatcher.BeginInvoke(new Action(() => spiel.messageBox.Items.Add(message)));
            }
        }

        public void SetImperator(int ident, int alt) {
            if (IsPlayer(ident)) {
                spiel.selbst.präsident = true;
            } else if (IsPlayer(alt)) {
                spiel.selbst.präsident = false;
            } else {
                foreach(FakeSpieler y in spiel.mitspieler) {
                    if(y.würfelzahl == ident) {
                        y.präsident = true;
                    } else if(y.würfelzahl == alt) {
                        y.präsident = false;
                    }
                }
            }
            spiel.IstPräsident();
        }

        public void Kaufen(int ident, int money, bool second) {
            if (IsPlayer(ident)) {
                spiel.Dispatcher.Invoke(() => {
                    KaufenWindow kw = new KaufenWindow(money, second);
                    if (kw.ShowDialog() == false) {
                        proxy.Invoke("KaufenAntwort", ident, kw.option);
                    }
                });
            }
        }

        public void AddGebäude(int ident) {
            if (IsPlayer(ident)) {
                spiel.addGebäude();
            } else {
                foreach(FakeSpieler f in spiel.mitspieler) {
                    if(f.würfelzahl == ident) {
                        f.punktzahl++;
                    }
                }
            }
        }

        public void RemoveGebäude(int ident) {
            if (IsPlayer(ident)){
                spiel.removeGebäude();
            } else {
                foreach (FakeSpieler f in spiel.mitspieler) {
                    if (f.würfelzahl == ident) {
                        f.punktzahl--;
                    }
                }
            }
        }

        public void addMiliz(int ident) {
            if (IsPlayer(ident)) {
                spiel.Milizen++;
            } else {
                foreach (FakeSpieler f in spiel.mitspieler) {
                    if (f.würfelzahl == ident) {
                        f.milizen++;
                    }
                }
            }
        }

        public void removeMiliz(int ident) {
            if (IsPlayer(ident)) {
                spiel.Milizen--;
            } else {
                foreach (FakeSpieler f in spiel.mitspieler) {
                    if (f.würfelzahl == ident) {
                        f.milizen--;
                    }
                }
            }
        }

        public void addSpieler() {
            proxy.Invoke("addSpieler");
        }

        public void setSpieler(int ident, int punkte, bool imperator, int flotten, int anzK) {
            if (spiel.selbst == null) {
                spiel.selbst = new FakeSpieler(ident, punkte, imperator, flotten, anzK);
                spiel.initGUI();
            }
        }

        public void Start() {
            proxy.Invoke("Start");
        }

        public void won(int ident) {
            spiel.Dispatcher.BeginInvoke(new Action(() => MessageBox.Show("Spieler " + ident + "hat gewonnen"))).Wait();
        }
    }
}
