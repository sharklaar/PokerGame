using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Library
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        
        public Hand(List<Card> cards)
        {
            Cards = cards;
        }
    }
}