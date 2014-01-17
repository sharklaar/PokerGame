using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Library
{
    public class Hand
    {
        public List<Card> Cards { get; set; }
        public int Rank { get; set; }
        public string HandDescription { get; set; }

        public Hand()
        {
            
        }

        public Hand(List<Card> cards)
        {
            Cards = cards;
        }
    }

    public class StraightFlush : Hand
    {
        public int HighestCard { get; set; }
        public string Suit { get; set; }
        
        public StraightFlush(int highestCard, string suit)
        {
            HighestCard = highestCard;
            Suit = suit;
            HandDescription = "Straight flush, " + suit + ", " + highestCard + " high";
            Rank = 1;
        }
    }

    public class FourOfAKind : Hand
    {
        public string HighCard { get; set; }

        public FourOfAKind(string highCard)
        {
            HighCard = highCard;

            HandDescription = "Four of a kind, " + highCard;

            Rank = 2;
        }
    }

    public class FullHouse : Hand
    {
        public string Three { get; set; }
        public string Two { get; set; }

        public FullHouse(string threeCard, string twoCard)
        {
            Two = twoCard;
            Three = threeCard;

            HandDescription = "Full house, " + threeCard + "s over " + twoCard + "s";

            Rank = 3;
        }
    }

    public class Flush : Hand
    {
        public Card HighestCard { get; set; }
        public string Suit { get; set; }

        public Flush(Card highestCard, string suit)
        {
            HighestCard = highestCard;
            Suit = suit;

            HandDescription = "Flush, " + highestCard.Value + " high, " + suit;

            Rank = 4;
        }
    }

    public class Straight : Hand
    {
        public int HighestCard { get; set; }

        public Straight(int highestCard)
        {
            HighestCard = highestCard;
            HandDescription = "Straight, " + highestCard + " high";

            Rank = 5;
        }
    }

    public class ThreeOfAKind : Hand
    {
        public string HighestCard { get; set; }

        public ThreeOfAKind(string highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "Three of a kind, " + highestCard + "s";

            Rank = 6;
        }
    }

    public class TwoPair : Hand
    {
        public string FirstPair { get; set; }
        public string SecondPair { get; set; }

        public TwoPair(string firstPair, string secondPair)
        {
            FirstPair = firstPair;
            SecondPair = secondPair;

            HandDescription = "Two pair, " + firstPair + "s and " + secondPair + "s";

            Rank = 7;
        }
    }

    public class Pair : Hand
    {
        public string HighestCard { get; set; }

        public Pair(string highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "One pair, " + highestCard + "s";

            Rank = 8;
        }
    }

    public class HighCard : Hand
    {
        public string HighestCard { get; set; }

        public HighCard(string highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "High card, " + highestCard;

            Rank = 9;
        }
    }
}