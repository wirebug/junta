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

        public void VersprechenHinzu(int ident, int id, string titel, string text) {
            Clients.All.AddVerspprechung(ident, id, titel, text);
        }

        public void VersprechenEntfernen(Spieler spieler) {
            Clients.All.RemoveVersprechen(spieler.ID);
        }
        public void AlleVersprechenEnfernen() {
            Clients.All.RemoveAllVersprechen();
        }
        public void FlottenAuswahl(Spieler spieler,int flottenAnzahl)
        {
            Clients.All.KampfWählen(spieler.ID,flottenAnzahl);
        }
        public void FlottenVerarbeiten(int idSpieler,int[] wrfl)
        {
            sv.FlottenBefehligen(idSpieler, wrfl);
        }
        public void KarteIDEntfernen(Spieler spieler, Karte karte)
        {

        }
        public void ZeigeNachricht(Spieler spieler, string nachricht)
        {

        }
        public void SetzeImperator(Spieler spieler, Spieler alt)
        {
            Clients.All.SetImperator(spieler.ID, alt.ID);
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
            sv.verarbeiteEinbrecherAntwort(b,0,0);
        }
        public void EinbrecherAntwort(bool b,int idSpielerWeg, int idSpielerHinzu)
        {
            sv.verarbeiteEinbrecherAntwort(b,idSpielerWeg, idSpielerHinzu);
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
            Clients.All.Kaufen(spieler.ID, spieler.Credits);
        }
        public void KaufenAntwort(Spieler spieler,int i)
        {

        }
    }
}
