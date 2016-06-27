using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GameClient.Referenzen;

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
            if (spiel.selbst.präsident)
            {
                proxy.VersprechenVerarbeiten(/*idKarte, idSpieler*/);
            }
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

        public void KampfWählen(int ident) {
            if (IsPlayer(ident)) {
                /*Fenster öffnen und entsprechend Anzahl Milizen in
                 * Liste einfügen und mit RadioButtons entsprechend
                 * Abfragen welches Ziel gewählt wurde*/

                /*Invoke testen ob Methode weiterläuft oder auf Abarbeitung wartet*/
            }
        }
        
        

    }
}
