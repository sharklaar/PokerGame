using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Library
{
    public class Player
    {
        public string Name { get; private set; }
        public List<Card> HoleCards { get; set; } 
        public Hand FinalHand { get; set; } 
        public Hand BestFinalHand { get; set; }
        public List<Hand> Hands { get; set; } 
        public bool IsWinner { get; set; }

        public Player(string playerName, List<Card> holeCards)
        {
            Name = playerName;
            HoleCards = holeCards;
            Hands = new List<Hand>();
        }

        public void GetBestFinalHand()
        {
            BestFinalHand = this.Hands.OrderBy(x => x.Rank).FirstOrDefault();
        }
    }
}