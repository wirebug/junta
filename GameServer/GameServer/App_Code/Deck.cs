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
        
        public Deck() {
            //ToDo Adde anstatt der 45 Kartenobjekte, die exakten Objekttypen dem stapel hinzufügen
            /*
            for(int i = 0; i < 45; i++) {
                stapel.Add(new Karte(i));
            }
            Shuffle();
            */
            for(int i = 1; i < 12; i++)
            {
                string s = 
                new CreditsKarte(i, 1000, null, this, null);
            }
            Karte k1 = new CreditsKarte(0,1000, null, this, null);
            //stapel.Add(new Karte(id,null,this,null))

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