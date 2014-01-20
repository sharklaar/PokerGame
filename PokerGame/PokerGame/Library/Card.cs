using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Library
{

    public class Card
    {
        public CardValue CardValue { get; set; }
        public string CardClass { get; set; }
        public string Suit { get; set; }
        public string DisplayValue { get; set; }

        public Card(string suit, CardValue cardValue)
        {
            Suit = suit;
            CardValue = cardValue;
            DisplayValue = string.Concat(cardValue, " of ", suit);
            CardClass = cardValue.StringValue + " " + suit;
        }
    }
}