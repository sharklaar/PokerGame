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
        public Enums.Hands BestFinalHand { get; set; }

        public Player(string playerName, List<Card> holeCards)
        {
            Name = playerName;
            HoleCards = holeCards;
            BestFinalHand = Enums.Hands.HighCard;
        }
    }
}