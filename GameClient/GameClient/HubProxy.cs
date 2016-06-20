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

            connection.Start().Wait();
        }

        public bool IsPlayer(int ident) {
            if(spiel.selbst.würfelzahl == ident) {
                return true;
            } else {
                return false;
            }
        }

        public void SetSelf(int ident) {
            if(spiel.selbst == null) {
                spiel.selbst = new FakeSpieler(ident);
            }
        }

        public void SetOtherPlayers() {

        }
    }
}
