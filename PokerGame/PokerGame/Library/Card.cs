using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Library
{

    public class Card
    {
        public string CardValue { get; set; }
        public string CardClass { get; set; }
        public string Suit { get; set; }
        public string Value { get; set; }
        public int NumericValue { get; set; }

        public Card(string suit, string cardValue, string cardClass, int numericValue)
        {
            Suit = suit;
            Value = cardValue;
            CardValue = string.Concat(cardValue, " of ", suit);
            CardClass = cardClass.ToLower();
            NumericValue = numericValue;
        }
    }
}