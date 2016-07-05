using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code {
    public class ImperatorKampf : Kampf {

        public ImperatorKampf(Spieler sp) : base(sp) { }
        public new Dictionary<Spieler, int> verteidigungswürfel { get; set; } = new Dictionary<Spieler, int>();
        private Dictionary<Spieler, int> ergV = new Dictionary<Spieler, int>();
        public void addVerteidigung(Spieler spieler) {
            try {
                this.verteidigungswürfel.Add(spieler, 1);
            } catch(ArgumentException) {
                verteidigungswürfel[spieler]++;
            }
        }

        public void addVerteidigung(Spieler spieler, int verteidigungswürfel) {
            try {
                this.verteidigungswürfel.Add(spieler, verteidigungswürfel);
            } catch(ArgumentException) {
                this.verteidigungswürfel[spieler] += verteidigungswürfel;
            }
        }

        private bool Würfeln() {
            int vert = zuordnung.planet.gebäude +zuordnung.flotten;
            int angr = 0;
            Random rng = new Random();
            foreach (KeyValuePair<Spieler, int> pair in verteidigungswürfel) {
                if (!pair.Key.imperator) {
                    int spVert = 0;
                    for (int i = 0; i < pair.Value; i++) {
                        spVert += rng.Next(1, 7);
                    }
                    ergV.Add(pair.Key, spVert);
                    vert += spVert;
                }
            }

            foreach (KeyValuePair<Spieler, int> pair in angriffswürfel) {
                int spAngr = 0;
                for (int i = 0; i < pair.Value; i++) {
                    spAngr += rng.Next(1, 7);
                }
                ergA.Add(pair.Key, spAngr);
                angr += spAngr;
            }
            if (vert >= angr) {
                return true;
            } else {
                return false;
            }

        }

        public new List<Spieler> ErgebnisBerechnen() {
            List<Spieler> ergebnis = new List<Spieler>();
            if (Würfeln()) {
                return ergebnis;
            } else {
                int s = angriffswürfel.Count;
                for (int i = 0; i < s; i++) {
                    Spieler temp = ergA.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                    ergebnis.Add(temp);
                    angriffswürfel.Remove(temp);
                }
                return ergebnis;
            }
        }

    }
}