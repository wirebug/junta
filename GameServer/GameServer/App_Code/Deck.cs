using GameServer.App_Code.Karten;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Deck {
        List<Karte> stapel;
        List<Karte> ablage;

        /// <summary>
        /// 45 Karten von id 0 bis id 44
        /// id 0 ist Spion
        /// id 1 ist Einbrecher
        /// id 2 bis 13 sind KAMPFPHASE KARTEN
        /// id 14 ist KEINE AUSWIRKUNGEN
        /// id 15,16 BONUSPUNKTE
        /// id 17 - 44 sind GELDPHASE KARTEN
        /// </summary>
        public Deck() {
            //ToDo Adde anstatt der 45 Kartenobjekte, die exakten Objekttypen dem stapel hinzufügen
            /*
            for(int i = 0; i < 45; i++) {
                stapel.Add(new Karte(i));
            }
            Shuffle();
            */


            stapel.Add(new SpionKarte(0, null, this)); //Nach FLotten
            stapel.Add(new EinbrecherKarte(1, null, this)); //Nach Versprechungen

            //KAMPFPHASE KARTEN
            stapel.Add(new AblenkungsmanoeverKarte(2, null, this)); //Würfel neu wprfeln
            stapel.Add(new AblenkungsmanoeverKarte(3, null, this));
            stapel.Add(new AnexionKarte(4, null, this)); //vor Kampf wenn vert mehr gebäude
            stapel.Add(new AttentatKarte(5, null, this));//vor kampf zerstöre 1 flotte
            stapel.Add(new AttentatKarte(6, null, this));
            stapel.Add(new BestechungKarte(7, null, this)); //vor kampf ignoriere flotte des imperators
            stapel.Add(new InterplanetareGefechtstationKarte(8, null, this));//vor kampf +3 bonus
            stapel.Add(new InterplanetareGefechtstationKarte(9, null, this));
            stapel.Add(new PropagandaKampagneKarte(10, null, this)); //vor kampf alle vert flotten haben wert 2
            stapel.Add(new PropagandaKampagneKarte(11, null, this));
            stapel.Add(new Ueberraschungsangriff(12, null, this)); //vor kampf als angreifer, ignoriere gebäude
            stapel.Add(new Ueberraschungsangriff(13, null, this));
            //REST
            stapel.Add(new NeuwahlenKarte(14, null, this)); //keine auswirkungen
            //BONUSPUNTKE
            stapel.Add(new ArtefaktKarte(15, null, this));
            stapel.Add(new ArtefaktKarte(16, null, this));
            //GELDPHASE KARTEN
            stapel.Add(new PRKampagneKarte(17, null, this));
            stapel.Add(new SpendeKarte(18, null, this));

            for (int i = 19; i <= 30; i++)
            {
                stapel.Add(new CreditsKarte(i, 1000, null, this));
            }
            for (int i = 31; i < 42; i++)
            {
                stapel.Add(new CreditsKarte(i, 2000, null, this));
            }
            for (int i = 42; i <= 44; i++)
            {
                stapel.Add(new CreditsKarte(i, 3000, null, this));
            }
            Shuffle();

        }

        public void Gespielt(Karte karte) {
            ablage.Add(karte);
        }
        public void Shuffle() {
            Random rng = new Random();
            int n = stapel.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                Karte value = stapel[k];
                stapel[k] = stapel[n];
                stapel[n] = value;
            }
        }

        public Karte Ziehen() {
            Karte temp = stapel.First();
            stapel.RemoveAt(0);
            if (!stapel.Any()) {
                Nachfüllen();
            }
            return temp;
        }

        public void Nachfüllen() {
            foreach( Karte i in ablage){
                stapel.Add(i);
                ablage.Remove(i);
            }
            Shuffle();
        }
    }
}