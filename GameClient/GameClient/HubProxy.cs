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

namespace GameClient {
    public class HubProxy {
        IHubProxy proxy;
        Spiel spiel;

        public HubProxy(Spiel v) {
            spiel = v;
            string url = @"http://localhost:58523/";
            var connection = new HubConnection(url);
            proxy = connection.CreateHubProxy("JuntaHub");

            proxy.On<int, bool>("SetSelf", (ident, präs) => SetSelf(ident, präs));

            proxy.On<int, bool>("AddOtherPlayer", (ident, präs) => AddOtherPlayers(ident, präs));

            proxy.On<int, int>("UpdatePunkteMitspieler", (ident, pktzl) => UpdatePunkteMitspieler(ident, pktzl));

            proxy.On<int, bool>("UpdatePräsident", (ident, präs) => UpdatePräsident(ident, präs));

            connection.Start().Wait();
        }

        public bool IsPlayer(int ident) {
            if(spiel.selbst.würfelzahl == ident) {
                return true;
            } else {
                return false;
            }
        }

        public void SetSelf(int ident, bool präs) {
            if(spiel.selbst == null) {
                spiel.selbst = new FakeSpieler(ident, präs);
            }
        }

        public void AddOtherPlayers(int ident, bool präs) {
            foreach(FakeSpieler s in spiel.mitspieler) {
                if(ident == s.würfelzahl) {
                    return;
                }
            }
            spiel.mitspieler.Add(new FakeSpieler(ident, präs));
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
            if (spiel.selbst.präsident)
            {
                List<FakeKarte> FKarteListe = new List<FakeKarte>();
                FKarteListe = JsonConvert.DeserializeObject<List<FakeKarte>>(json);

                VersprechenWählenWindow vww = new VersprechenWählenWindow(FKarteListe, spiel);
                if (vww.ShowDialog() == false) {
                    proxy.Invoke("VersprechenVerarbeiten", vww.versprechen);
                }               
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
                spiel.karten.Add(temp);
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
                spiel.versprechen.Add(temp);
            }
        }

        public void RemoveKarte(int ident, int id) {
            if (IsPlayer(ident)) {
                foreach(FakeKarte s in spiel.karten) {
                    if(s.id == id) {
                        spiel.karten.Remove(s);
                        return;
                    }
                }
            }
        }

        public void VersprechenBekommen(int ident) {
            if (IsPlayer(ident)){
                foreach(FakeKarte s in spiel.versprechen) {
                    spiel.versprechen.Remove(s);
                    spiel.karten.Add(s);
                }
            }
        }
        
        public void RemoveVersprechen(int ident) {
            if (IsPlayer(ident)) {
                spiel.versprechen.Clear();
            }
        }

        public void RemoveAllVersprechen() {
            spiel.versprechen.Clear();
        }

        public void KampfWählen(int ident,int flottenAnzahl) {
            if (IsPlayer(ident)) {
                KampfWählenWindow kww = new KampfWählenWindow(flottenAnzahl, spiel);
                if(kww.ShowDialog() == false) {
                    proxy.Invoke("FlottenVerarbeiten", ident, kww.würfel);
                }
                
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
                MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes) {
                    //Fenster mit spieleranzahl an radiobuttons
                    //EinbrecherWindow
                    EinbrecherWindow ew = new EinbrecherWindow(spiel);
                    if (ew.ShowDialog() == false)
                    {
                        proxy.Invoke("EinbrecherAntwort", true, ew.erg);
                    }
                    //Methode im EW auswahl eines spielers der beklaut werden soll.

                    
                } else {
                    proxy.Invoke("EinbrecherAntwort", false);
                }
            }
        }

        public  void ZeigeNachricht(int ident, string message) {
            if (IsPlayer(ident)) {
                string caption = "Junta";
                MessageBox.Show(message, caption, MessageBoxButton.OK);
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
        }

        public void Kaufen(int ident, int money) {
            if (IsPlayer(ident)) {
                KaufenFenster kw = new KaufenFenster(money);
                if(kw.ShowDialog() == false) {
                    proxy.Invoke("KaufenAntwort", ident, kw.option);
                }
            }
        }



    }
}
