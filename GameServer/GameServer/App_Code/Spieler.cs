using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;

namespace GameServer.App_Code
{
    public class Spieler
    {
        public static int spielerCount = 0;
        public int ID { get; set; }
        public int Kampfmodifikator { get; set; }//Kampkarte InterplanetareGefechtsstations - bonus
        public List<Karte> versprechungen;//Versprechungen sind noch nicht in der Hand und werden als Karte(?) übergeben
        public int flotten { get; set; }
        public int punkte { get; set; } = 1;
        public bool imperator { get; set; }
        public Planet planet { get; set; }
        public Hand hand { get; set; }
        public Kampf kampf { get; set; }
        public int Credits { get; set; }
        public Spielverwaltung sv { get; set; }
        public int GeldZuSchreiben { get; set; } =0;

        //Konstruktor NICHT FERTIG!!!
        public Spieler(int flotten, int punkte, bool imperator, Planet planet, Hand hand)
        {
            Kampfmodifikator = 0;
        }
        public Spieler()
        {
            spielerCount++;
            ID = spielerCount;
        }

        /// <summary>
        /// erhöht Guthaben mit der Summe aus Action
        /// </summary>
        /// <param name="Credit">Muss eine Karte vom Typ Geldkarte sein</param>
        public void increaseGuthaben(int Credit)
        {
            this.Credits += Credit;
        }

        public void decreaseGuthaben(int credit) {
            Credits -= credit;
        }

        public void schreibeAusgaben() {
            if(GeldZuSchreiben > 0) {
                List<CreditsKarte> geldkarten = new List<CreditsKarte>();
                foreach (Karte u in hand.handKarten) {
                    if(u.id >= 19) {
                        geldkarten.Add(u as CreditsKarte);
                    }
                }
                int betr = 0;
                List<Karte> remove = new List<Karte>();
                while (betr < GeldZuSchreiben) {
                    foreach(CreditsKarte k in geldkarten) {
                        if(k.Credits == 1000) {
                            betr += 1000;
                        } else if(k.Credits == 2000) {
                            betr += 2000;
                        } else {
                            betr += 3000;
                        }
                        remove.Add(k);
                        break;
                    }
                }
                foreach(Karte v in remove) {
                    hand.RemoveHandkarte(v);
                    sv._hub.KarteIDEntfernen(this, v);
                }
                GeldZuSchreiben = 0;
            }
        }
    }
}