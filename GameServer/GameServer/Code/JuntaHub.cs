using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using GameServer.Code.Karten;
using Microsoft.AspNet.SignalR.Hubs;

namespace GameServer.Code {
    public class JuntaHub : Hub {
        private IHubContext context;
        public Spielverwaltung sv { get; set; }

        public JuntaHub(Spielverwaltung sv)
        {
            this.sv = sv;
            context = GlobalHost.ConnectionManager.GetHubContext<JuntaHub>();
        }
        public JuntaHub() {
            context = GlobalHost.ConnectionManager.GetHubContext<JuntaHub>();
        }

        public void KarteIdHinzu(Spieler spieler, Karte karte) {
            context.Clients.All.AddKarte(spieler.planet.würfelzahl, karte.id, karte.titel, karte.text);
        }

        public void VersprechenAuswählen(int idSpieler,string json)
        {
            context.Clients.All.VersprechenWählen(idSpieler,json);
        }
        public void VersprechenVerarbeiten(Dictionary<int,int> versprechung)
        {
            sv.VersprechungMachen(versprechung);
        }

        public void VersprechenHinzu(int ident, int id, string titel, string text) {
            context.Clients.All.AddVersprechung(ident, id, titel, text);
        }

        public void VersprechenEntfernen(Spieler spieler) {
            context.Clients.All.RemoveVersprechen(spieler.ID);
        }
        public void AlleVersprechenEnfernen() {
            context.Clients.All.RemoveAllVersprechen(1);
        }
        public void FlottenAuswahl(Spieler spieler,int flottenAnzahl)
        {
            context.Clients.All.KampfWählen(spieler.ID,flottenAnzahl);
        }
        public void FlottenVerarbeiten(int idSpieler,int[] wrfl)
        {
            sv.FlottenBefehligen(idSpieler, wrfl);
        }
        public void KarteIDEntfernen(Spieler spieler, Karte karte)
        {
            context.Clients.All.RemoveKarte(spieler.ID, karte.id);
        }
        public void ZeigeNachricht(string nachricht)
        {
            context.Clients.All.message(nachricht);
        }
        public void SetzeImperator(Spieler spieler, Spieler alt)
        {
            context.Clients.All.SetImperator(spieler.ID, alt.ID);
        }
        public void SpieleSpion(Spieler spieler)
        {
            context.Clients.All.SpieleSpion(spieler.planet.würfelzahl);
        }
        public void SpionAntwort(bool b)
        {
            sv.verarbeiteSpionAntwort(b);
        }
        public void SpieleEinbrecher(Spieler spieler)
        {
            context.Clients.All.SpieleEinbrecher(spieler.planet.würfelzahl);
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
        public void AusführenSpion(Spieler spieler)
        {

        }
        public void Kaufen(Spieler spieler)
        {
            context.Clients.All.Kaufen(spieler.ID, spieler.Credits, false);
        }
        public void Kaufen2(Spieler spieler, bool second) {
            context.Clients.All.Kaufen(spieler.ID, (spieler.Credits - spieler.GeldZuSchreiben), second);
        }
        public void KaufenAntwort(int id,int i)
        {
            sv.GeldAusgeben(id, i);
        }
        public void AddGebäude(Spieler spieler) {
            context.Clients.All.AddGebäude(spieler.ID);
        }

        public void RemoveGebäude(Spieler spieler) {
            context.Clients.All.RemoveGebäude(spieler.ID);
        }

        public void AddMiliz(Spieler spieler) {
            context.Clients.All.addMiliz(spieler.ID);
        }

        public void RemoveMiliz(Spieler spieler) {
            context.Clients.All.removeMiliz(spieler.ID);
        }

        public void StartUp() {
            GameLoop.Main();
        }

        public void Start() {
            GameLoop.Start();
        }

        public void addSpieler() {
            GameLoop.initSpieler();
        }

        public void setSpieler(Spieler spieler) {
            context.Clients.All.setSpieler(spieler.ID, spieler.punkte, spieler.imperator, spieler.flotten, spieler.hand.GetHandkartenAnzahl());
        }

        public void AddOtherPlayer(Spieler spieler) {
            context.Clients.All.AddOtherPlayer(spieler.ID, spieler.imperator);
        }
    }
}
