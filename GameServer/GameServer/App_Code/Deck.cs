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
            for(int i = 0; i < 45; i++) {
                stapel.Add(new Karte(i));
            }
            Shuffle();
        }

        public void Gespielt(Karte karte) {
            ablage.Add(karte);
        }
        public void Shuffle() {
            Random rng = new Random();
            int n = stapel.Count();
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