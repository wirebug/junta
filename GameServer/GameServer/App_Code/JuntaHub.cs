using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GameServer.App_Code {
    public class JuntaHub : Hub {

        public Spielverwaltung sv { get; set; }

        public JuntaHub(Spielverwaltung sv)
        {
            this.sv = sv;
        }

        //SIGNALR Hubs api guide server
        //in server code , you define methods that can be called by clients, and you call methods that run on the client.

        public void KarteIdHinzu(Spieler spieler, int id) { 
            
        }
        public void VersprechenVerarbeiten(int[] ids, Spieler sp)
        {
            sv.VersprechungMachen(ids, sp);
        }
        public void FlottenAuswahl(Spieler spieler)
        {

        }
        public void FlottenVerarbeiten(Spieler spieler)
        {

        }
        public void KarteIDEntfernen(Spieler spieler)
        {

        }
        public void ZeigeNachticht(Spieler spieler, string nachricht)
        {

        }
        public void SetzeImperator(Spieler spieler)
        {

        }
        public void SpieleSpion(Spieler spieler)
        {

        }
        public void SpieleEinbrecher(Spieler spieler)
        {

        }
        public void SpieleVorkampfkarte(Spieler spieler)
        {

        }
        public void SpieleGeldkarte(Spieler spieler)
        {

        }
        public void AusführenSpion(Spieler spieler)
        {

        }
        public void AusführenEinbrecher(Spieler spieler)
        {

        }
        public void Kaufen(Spieler spieler)
        {

        }
        public void KaufBearbeiten(Spieler spieler,int i)
        {

        }
    }
}
