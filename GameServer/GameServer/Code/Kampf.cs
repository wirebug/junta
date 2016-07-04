﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.Code.Karten;

namespace GameServer.Code {
    public class Kampf {
        public Dictionary<Spieler, int> angriffswürfel { get; set; } //Würfel pro Spieler
        public int verteidigungswürfel { get; set; } //Würfel des Verteidigungsspieler
        public Spieler zuordnung { get; set; }

        public Kampf() {
            angriffswürfel = new Dictionary<Spieler, int>();
            verteidigungswürfel += zuordnung.planet.gebäude;
        }

        public void addVerteidigung() {
            this.verteidigungswürfel++;
        }
        public void addVerteidigung(int verteidigungswürfel) {
            this.verteidigungswürfel = verteidigungswürfel;
        }

        public void addAngriff(Spieler spieler) {
            try {
                this.angriffswürfel.Add(spieler, 1);
            } catch (ArgumentException) {
                addAngriffWert(spieler, 1);
            }
        }

        public void addAngriffWert(Spieler spieler, int value) {
            try {
                this.angriffswürfel.Add(spieler, value);
            } catch(ArgumentException) {
                this.angriffswürfel[spieler] += value;
            }
        }

        private bool Würfeln() { 
            int vert = 0;
            int angr = 0;
            Random rng = new Random();
            for (int i = 0; i < verteidigungswürfel; i++) {
                vert += rng.Next(1, 6);
            }
            foreach (KeyValuePair<Spieler, int> pair in angriffswürfel) {
                int spAngr = 0;
                for (int i = 0; i < pair.Value; i++) {
                    spAngr += rng.Next(1, 6);
                }
                angriffswürfel[pair.Key] = spAngr;
                angr += spAngr;
            }
            if (vert >= angr) {
                return true;
            } else {
                return false;
            }

        }

        public List<Spieler> ErgebnisBerechnen() {
            List<Spieler> ergebnis = new List<Spieler>();
            if (Würfeln()) {
                return ergebnis;
            } else {
                int s = angriffswürfel.Count;
                for (int i = 0; i < s; i++) {
                    Spieler temp = angriffswürfel.Max().Key;
                    ergebnis.Add(temp);
                    angriffswürfel.Remove(temp);
                }
                return ergebnis;
            }
        }

        public void KarteVorKampf(Karte k) {
            k.Action();
        }

        public void KarteInKampf(Karte k) {
            k.Action();
        }
        

    }
}