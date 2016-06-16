﻿using GameServer.App_Code.Karten;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code {
    public class Deck {
        List<Karte> stapel;
        List<Karte> ablage;
        
        public Deck() {
            //ToDo Adde anstatt der 45 Kartenobjekte, die exakten Objekttypen dem stapel hinzufügen
            /*
            for(int i = 0; i < 45; i++) {
                stapel.Add(new Karte(i));
            }
            Shuffle();
            */


            // 45 Karten von ID 0 bis ID 44
            // ID 0 ist Spion
            // ID 1 ist Einbrecher
            // ID 2 bis 13 sind KAMPFPHASE KARTEN
            // ID 14 ist KEINE AUSWIRKUNGEN
            // ID 15,16 BONUSPUNKTE
            // ID 17 - 44 sind GELDPHASE KARTEN


            stapel.Add(new SpionKarte(0, null, this, null)); //Nach FLotten
            stapel.Add(new EinbrecherKarte(1, null, this, null)); //Nach Versprechungen

            //KAMPFPHASE KARTEN
            stapel.Add(new AblenkungsmanoeverKarte(2, null, this, null)); //Würfel neu wprfeln
            stapel.Add(new AblenkungsmanoeverKarte(3, null, this, null));
            stapel.Add(new AnexionKarte(4, null, this, null)); //vor Kampf wenn vert mehr gebäude
            stapel.Add(new AttentatKarte(5, null, this, null));//vor kampf zerstöre 1 flotte
            stapel.Add(new AttentatKarte(6, null, this, null));
            stapel.Add(new BestechungKarte(7, null, this, null)); //vor kampf ignoriere flotte des imperators
            stapel.Add(new InterplanetareGefechtstationKarte(8, null, this, null));//vor kampf +3 bonus
            stapel.Add(new InterplanetareGefechtstationKarte(9, null, this, null));
            stapel.Add(new PropagandaKampagneKarte(10, null, this, null)); //vor kampf alle vert flotten haben wert 2
            stapel.Add(new PropagandaKampagneKarte(11, null, this, null));
            stapel.Add(new Ueberraschungsangriff(12, null, this, null)); //vor kampf als angreifer, ignoriere gebäude
            stapel.Add(new Ueberraschungsangriff(13, null, this, null));
            //REST
            stapel.Add(new NeuwahlenKarte(14, null, this, null)); //keine auswirkungen
            //BONUSPUNTKE
            stapel.Add(new ArtefaktKarte(15, null, this, null));
            stapel.Add(new ArtefaktKarte(16, null, this, null));
            //GELDPHASE KARTEN
            stapel.Add(new PRKampagneKarte(17, null, this, null));
            stapel.Add(new SpendeKarte(18, null, this, null));

            for (int i = 19; i <= 30; i++)
            {
                stapel.Add(new CreditsKarte(i, 1000, null, this, null));
            }
            for (int i = 31; i < 42; i++)
            {
                stapel.Add(new CreditsKarte(i, 2000, null, this, null));
            }
            for (int i = 42; i <= 44; i++)
            {
                stapel.Add(new CreditsKarte(i, 3000, null, this, null));
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