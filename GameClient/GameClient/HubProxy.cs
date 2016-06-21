using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameClient.Referenzen;
using System.Windows;

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

        public void UpdatePräsident(int ident, bool präs) {
            if (IsPlayer(ident)) {
                spiel.selbst.präsident = präs;
            } else {
                foreach(FakeSpieler s in spiel.mitspieler) {
                    if(s.würfelzahl == ident) {
                        s.präsident = präs;
                        return;
                    }
                }
            }
        }

        public void VersprechenWählen() {
            /*neues Fenster mit Liste von allen Karten die per JSON
             * Objekt übertragen wurden. Per Radio Button für alle den
             * entsprechenden Spieler auswählen und Ergebnis an Server senden.
             * TODO Custom List Objekte hearusfinden*/
        }

        public void AddKarte(int ident,int id, string titel, string text) {
            if (IsPlayer(ident)){
                FakeKarte temp = new FakeKarte();
                temp.id = id;
                temp.text = text;
                temp.titel = titel;
                spiel.karten.Add(temp);
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

        public void KampfWählen(int ident) {
            if (IsPlayer(ident)) {
                /*Fenster öffnen und entsprechend Anzahl Milizen in
                 * Liste einfügen und mit RadioButtons entsprechend
                 * Abfragen welches Ziel gewählt wurde*/

                /*Invoke testen ob Methode weiterläuft oder auf Abarbeitung wartet*/
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
                    proxy.Invoke("EinbrecherAntwort", true);
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



    }
}
