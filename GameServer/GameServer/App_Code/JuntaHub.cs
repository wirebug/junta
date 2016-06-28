using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GameServer.App_Code.Karten;

namespace GameServer.App_Code {
    public class JuntaHub : Hub {

        public Spielverwaltung sv { get; set; }

        public JuntaHub(Spielverwaltung sv)
        {
            this.sv = sv;
        }

        //SIGNALR Hubs api guide server
        //in server code , you define methods that can be called by clients, and you call methods that run on the client.

        public void KarteIdHinzu(Spieler spieler, Karte karte) {
            Clients.All.AddKarte(spieler.planet.würfelzahl, karte.ID, karte.kartenname, karte.kartenphase + " " + karte.kartentext);
        }

        /// <summary>
        /// Imperator wählt seine Versprechen aus
        /// </summary>
        public void VersprechenAuswählen()
        {
            Clients.All.VersprechenWählen();
        }
        public void VersprechenVerarbeiten(int idSpieler, int[] idKarten)
        {
            sv.VersprechungMachen(idSpieler,idKarten);
        }
        public void FlottenAuswahl(Spieler spieler)
        {
            //TODO im hubproxy
            //Clients.All.FlotteWählen(Spieler spieler, int[] wrfl);
        }
        public void FlottenVerarbeiten(Spieler spieler,int[] wrfl)
        {
            sv.FlottenBefehligen(spieler, wrfl);
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
            Clients.All.SpieleSpion(spieler.planet.würfelzahl);
        }
        public void SpionAntwort(bool b)
        {
            sv.verarbeiteSpionAntwort(b);
        }
        public void SpieleEinbrecher(Spieler spieler)
        {
            Clients.All.SpieleEinbrecher(spieler.planet.würfelzahl);
        }
        public void EinbrecherAntwort(bool b)
        {
            sv.verarbeiteEinbrecherAntwort(b,-1);
        }
        public void EinbrecherAntwort(bool b,int idSpieler)
        {
            sv.verarbeiteEinbrecherAntwort(b,idSpieler);
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
