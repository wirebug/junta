using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameServer.Code.Karten;

namespace GameServer.Code {
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
        
        public Hand()
        {
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
            hatEinbrecher = false;
            hatSpion = false;
            foreach (Karte k in handKarten)
            {
                if (k.id == 0)
                {
                    hatSpion = true;
                }
   
                if (k.id == 1)
                {
                    hatEinbrecher = true;
                }
                
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
                }
                
            }
            return null;
        }

        public void RemoveHandkarte(Karte item)
        {
            if (item.id >= 19) {
                spieler.decreaseGuthaben((item as CreditsKarte).Credits);
            }            
            handKarten.Remove(item);
            checkFlags();
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
            handKarten.Add(item);
            checkFlags();
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