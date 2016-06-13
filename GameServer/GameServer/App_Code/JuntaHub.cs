using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GameServer.App_Code {
    public class JuntaHub : Hub {

        Spielverwaltung sv;

        public void KarteIdHinzu(Spieler spieler, int id) { 
        }
        public void VersprechenVerarbeiten(int[] ids, Spieler sp)
        {
            sv.VersprechungMachen(ids, sp);
        }
    }
}
