using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.App_Code
{
    public class Spielverwaltung
    {
        Queue<Spieler> reihenfolge;
        JuntaHub _hub;
        Deck deck;//
        
        public void VerteileKarten(Spieler sp) {
            Spieler temp = reihenfolge.First();
            do {
                Spieler aktuell = reihenfolge.Dequeue();
                _hub.KarteIdHinzu(aktuell, deck.Ziehen());
            } while (!reihenfolge.First().Equals(temp));
        }   
    }
}