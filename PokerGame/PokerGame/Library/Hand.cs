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
        public const int Rank = 1;
        
        public StraightFlush(int highestCard, string suit)
        {
            HighestCard = highestCard;
            Suit = suit;
            HandDescription = "Straight flush, " + suit + ", " + highestCard + " high";
        }
    }

    public class FourOfAKind : Hand
    {
        public string HighCard { get; set; }
        public const int Rank = 2;

        public FourOfAKind(string highCard)
        {
            HighCard = highCard;

            HandDescription = "Four of a kind, " + highCard;
        }
    }

    public class FullHouse : Hand
    {
        public string Three { get; set; }
        public string Two { get; set; }
        public const int Rank = 3;

        public FullHouse(string threeCard, string twoCard)
        {
            Two = twoCard;
            Three = threeCard;

            HandDescription = "Full house, " + threeCard + "s over " + twoCard + "s";
        }
    }

    public class Flush : Hand
    {
        public Card HighestCard { get; set; }
        public string Suit { get; set; }
        public const int Rank = 4;

        public Flush(Card highestCard, string suit)
        {
            HighestCard = highestCard;
            Suit = suit;

            HandDescription = "Flush, " + highestCard.Value + " high, " + suit;
        }
    }

    public class Straight : Hand
    {
        public int HighestCard { get; set; }
        public const int Rank = 5;

        public Straight(int highestCard)
        {
            HighestCard = highestCard;
            HandDescription = "Straight, " + highestCard + " high";
        }
    }

    public class ThreeOfAKind : Hand
    {
        public string HighestCard { get; set; }
        public const int Rank = 6;

        public ThreeOfAKind(string highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "Three of a kind, " + highestCard + "s";
        }
    }

    public class TwoPair : Hand
    {
        public string FirstPair { get; set; }
        public string SecondPair { get; set; }
        public const int Rank = 7;

        public TwoPair(string firstPair, string secondPair)
        {
            FirstPair = firstPair;
            SecondPair = secondPair;

            HandDescription = "Two pair, " + firstPair + "s and " + secondPair + "s";
        }
    }

    public class Pair : Hand
    {
        public string HighestCard { get; set; }
        public const int Rank = 8;

        public Pair(string highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "One pair, " + highestCard + "s";
        }
    }

    public class HighCard : Hand
    {
        public string HighestCard { get; set; }
        public const int Rank = 9;

        public HighCard(string highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "High card, " + highestCard;
        }
    }
}