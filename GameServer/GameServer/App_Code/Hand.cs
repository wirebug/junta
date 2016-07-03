using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.App_Code.Karten;

namespace GameServer.App_Code
{
    public class Hand
    {
        public bool hatSpion { get; set; }
        public bool hatEinbrecher { get; set; }
        public bool hatKampfkarte { get; set; }
        public Spieler spieler { get; set; }
        public bool isEmpty { get { return handKarten.Count == 0; } }

        /// <summary>
        /// Handkarten
        /// </summary>
        public List<Karte> handKarten { get; set; }
        
        public Hand(Spieler spieler)
        {
            this.spieler = spieler;
            handKarten = new List<Karte>();
            hatEinbrecher = false;
            hatKampfkarte = false;
            hatSpion = false;
        }
        /// <summary>
        /// überprüft die flags hatSpion, hat Einbrecher, hatKampfkarte auf korrektheit
        /// </summary>
        void checkFlags()
        {
            hatKampfkarte = false;
            foreach (Karte k in handKarten)
            {
                if (k.id == 0)
                {
                    hatSpion = true;
                }
                else
                    hatSpion = false;
                if (k.id == 1)
                {
                    hatEinbrecher = true;
                }
                else
                    hatEinbrecher = false;
                if (k.id > 1 && k.id < 14)
                {
                    hatKampfkarte = true;
                }
            }
        }

        /// <summary>
        /// Zieht zufällige Karte aus Spielerhand
        /// </summary>
        /// <returns>Zufällige Handkarte des Spielers</returns>
        public Karte RandomHandkarte()
        {
                Random rng = new Random();
                int anzahl = GetHandkartenAnzahl();
                int index = rng.Next(anzahl);
                Karte ret = handKarten[index];
                RemoveHandkarte(handKarten[index]);
                return ret;
        }
        public Karte getKarteById(int idKarte)
        {
            foreach(Karte k in handKarten)
            {
                if(k.id == idKarte)
                {
                    return k;
                }else
                {
                    throw new Exception("Karte nicht gefunden");
                }
            }
            throw new Exception("Karte nicht gefunden");
        }

        public void RemoveHandkarte(Karte item)
        {
            if (item.id >= 19) {
                spieler.decreaseGuthaben((item as CreditsKarte).Credits);
            }
            checkFlags();
            handKarten.Remove(item);
        }
        /// <summary>
        /// Legt Karte auf Hand
        /// </summary>
        /// <param name="item">Karte die auf die Hand kommt</param>
        public void AddHandkarte(Karte item)
        {
            if(item.id >= 19) {
                spieler.increaseGuthaben((item as CreditsKarte).Credits);
            }
            checkFlags();
            handKarten.Add(item);
        }
        /// <summary>
        /// Gibt Anzahl der Handkarten zurück
        /// </summary>
        /// <returns></returns>
        public int GetHandkartenAnzahl()
        {
            return handKarten.Count();
        }
    }
}