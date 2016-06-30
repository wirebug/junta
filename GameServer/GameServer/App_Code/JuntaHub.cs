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
            Clients.All.AddKarte(spieler.planet.würfelzahl, karte.id, karte.titel, karte.kartenphase + " " + karte.text);
        }

        /// <summary>
        /// Imperator wählt seine Versprechen aus
        /// </summary>
        public void VersprechenAuswählen(int idSpieler,string json)
        {
            Clients.All.VersprechenWählen(idSpieler,json);
        }
        public void VersprechenVerarbeiten(Dictionary<int,int> versprechung)
        {
            sv.VersprechungMachen(versprechung);
        }
        /// <summary>
        /// KampfWählen ist gleich Flotte festlegen wer wen angreift
        /// </summary>
        /// <param name="spieler">Spieler ist der Spieler welcher an der reihe ist</param>
        /// <param name="flottenAnzahl">Anzahl der Milizen die der Spieler besitzt</param>
        public void FlottenAuswahl(Spieler spieler,int flottenAnzahl)
        {
            Clients.All.KampfWählen(spieler.ID,flottenAnzahl);
        }
        public void FlottenVerarbeiten(int idSpieler,int[] wrfl)
        {
            sv.FlottenBefehligen(idSpieler, wrfl);
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
