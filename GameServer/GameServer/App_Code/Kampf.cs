using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Kampf {
        private Dictionary<Spieler, int> angriffswürfel { get; set; } //Würfel pro Spieler
        private int verteidigungswürfel { get; set; } //Würfel des Verteidigungsspieler
        private Spieler zuordnung { get; set; }

        public Kampf() {
            angriffswürfel = new Dictionary<Spieler, int>();
        }
        public void addVerteidigung(int verteidigungswürfel) {
            this.verteidigungswürfel = verteidigungswürfel;
        }

        public void addAngriff(Spieler spieler, int angriffswürfel) {
            this.angriffswürfel.Add(spieler, angriffswürfel);
        }

        private bool Würfeln() { 
            int vert = 0; // spieler.planet.gebäude
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
        

    }
}