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




                //json ist die fakekarte
                //PArse den json
                //fakekarte in liste einfügen
                //liste als parameter an das fenster übergeben

                //TODO
                // Methode liefert 
                // dict
                // 
                //TODO 
                /*neues Fenster mit Liste von allen Karten die per JSON
                 * Objekt übertragen wurden. Per Radio Button für alle den
                 * entsprechenden Spieler auswählen und Ergebnis an Server senden.
                 * TODO Custom List Objekte hearusfinden*/
                //TODO
                Dictionary<int, int> versprechung = new Dictionary<int, int>();
                proxy.Invoke("VersprechenVerarbeiten",versprechung);
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
            /// <summary>
            /// FlottenAuswahl
            /// </summary>
            /// <param name="ident">idSpieler</param>
        public void KampfWählen(int ident,int flottenAnzahl) {
            if (IsPlayer(ident)) {
                int[] würfel = new int[flottenAnzahl];

                /*Fenster öffnen und entsprechend Anzahl Milizen in
                 * Liste einfügen und mit RadioButtons entsprechend
                 * Abfragen welches Ziel gewählt wurde*/

                /*Invoke testen ob Methode weiterläuft oder auf Abarbeitung wartet*/

                proxy.Invoke("FlottenVerarbeiten", ident, würfel);
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
                    EinbrecherWindow ew = new EinbrecherWindow();
                    if (ew.ShowDialog() == false)
                    {
                        proxy.Invoke("EinbrecherAntwort", true, ew.zahl);
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



    }
}
