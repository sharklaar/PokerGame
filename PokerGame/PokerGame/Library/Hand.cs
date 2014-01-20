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
        public CardValue HighestCard { get; set; }
        public string Suit { get; set; }

        public StraightFlush(CardValue highestCard, string suit)
        {
            HighestCard = highestCard;
            Suit = suit;
            HandDescription = "Straight flush, " + suit + ", " + highestCard.StringValue + " high";
            Rank = 1;
        }
    }

    public class FourOfAKind : Hand
    {
        public CardValue HighCard { get; set; }

        public FourOfAKind(CardValue highCard)
        {
            HighCard = highCard;

            HandDescription = "Four of a kind, " + highCard.StringValue;

            Rank = 2;
        }
    }

    public class FullHouse : Hand
    {
        public CardValue Three { get; set; }
        public CardValue Two { get; set; }

        public FullHouse(CardValue threeCard, CardValue twoCard)
        {
            Two = twoCard;
            Three = threeCard;

            HandDescription = "Full house, " + threeCard.StringValue + "s over " + twoCard.StringValue + "s";

            Rank = 3;
        }
    }

    public class Flush : Hand
    {
        public CardValue HighestCard { get; set; }
        public string Suit { get; set; }

        public Flush(CardValue highestCard, string suit)
        {
            HighestCard = highestCard;
            Suit = suit;

            HandDescription = "Flush, " + highestCard.StringValue + " high, " + suit;

            Rank = 4;
        }
    }

    public class Straight : Hand
    {
        public CardValue HighestCard { get; set; }

        public Straight(CardValue highestCard)
        {
            HighestCard = highestCard;
            HandDescription = "Straight, " + highestCard.StringValue + " high";

            Rank = 5;
        }
    }

    public class ThreeOfAKind : Hand
    {
        public CardValue HighestCard { get; set; }

        public ThreeOfAKind(CardValue highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "Three of a kind, " + highestCard.StringValue + "s";

            Rank = 6;
        }
    }

    public class TwoPair : Hand
    {
        public CardValue FirstPair { get; set; }
        public CardValue SecondPair { get; set; }

        public TwoPair(CardValue firstPair, CardValue secondPair)
        {
            FirstPair = firstPair;
            SecondPair = secondPair;

            HandDescription = "Two pair, " + firstPair.StringValue + "s and " + secondPair.StringValue + "s";

            Rank = 7;
        }
    }

    public class Pair : Hand
    {
        public CardValue HighestCard { get; set; }

        public Pair(CardValue highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "One pair, " + highestCard.StringValue + "s";

            Rank = 8;
        }
    }

    public class HighCard : Hand
    {
        public CardValue HighestCard { get; set; }

        public HighCard(CardValue highestCard)
        {
            HighestCard = highestCard;

            HandDescription = "High card, " + highestCard.StringValue;

            Rank = 9;
        }
    }
}