using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Code {
    public class GameLoop {
        
        public static void Main() {
            init();
                  
        }
        public static void sada() {
            while (true) {
                sleep();
                sv._hub.ZeigeNachricht("testetetst");
            }
        }

        public static void Start() {
            foreach (Spieler s in sv.spieler) {
                sv._hub.AddOtherPlayer(s);
            }
            while (true) {
                sv.KartenZiehen();
                sv.VersprechungStart(sv.imperator);
                while (waitForVersprechen) {
                    sleep();
                }
                sv.FlottenStart();
                while (waitForFlotten < anzSpieler) {
                    sleep();
                }
                sv.KaempfeAustragen();
                sv.Kaufen();
                while (waitForKaufen < anzSpieler) {
                    sleep();
                }
                sv.HandkartenlimitPrüfen();
                reset();
            }
        }

        private static void init() {
            sv = new Spielverwaltung();
            sv._hub = new JuntaHub(sv);
            sv.deck = new Deck();
            sv.spieler = new List<Spieler>();
        }

        public static void initSpieler() {
            if(anzSpieler < 5) {
                Planet planet = new Planet();
                Hand hand = new Hand();
                Spieler a = new Spieler(1, 1, anzSpieler == 0, planet, hand, sv);
                planet.spieler = a;
                hand.spieler = a;
                sv.spieler.Add(a);
                if (a.imperator) {
                    sv.imperator = a;
                }
                sv._hub.setSpieler(a);
                anzSpieler++;
            }
        }

        private static void reset() {
            waitForVersprechen = true;
            waitForFlotten = 0;
            waitForKaufen = 0;
        }


        private static void sleep() {
            System.Threading.Thread.Sleep(1000);
        }

        public static Spielverwaltung sv;
        public static int anzSpieler = 0;

        //stoptrigger

        public static bool waitForVersprechen = true;

        public static int waitForFlotten = 0;

        public static int waitForKaufen = 0;

    }
}