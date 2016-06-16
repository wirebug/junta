using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class ImperatorKampf : Kampf {

        public new Dictionary<Spieler, int> verteidigungswürfel { get; set; }

        public void addVerteidigung(Spieler spieler, int verteidigungswürfel) {
            this.verteidigungswürfel.Add(spieler, verteidigungswürfel);
        }

        private bool Würfeln() {
            int vert = 0;
            int angr = 0;
            Random rng = new Random();
            foreach (KeyValuePair<Spieler, int> pair in verteidigungswürfel) {
                if (!pair.Key.imperator) {
                    int spVert = 0;
                    for (int i = 0; i < pair.Value; i++) {
                        spVert += rng.Next(1, 6);
                    }
                    verteidigungswürfel[pair.Key] = spVert;
                    vert += spVert;
                }
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

    }
}