using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PokerGame.Library
{
    public class Game
    {
        public List<Card> Deck { get; set; } 
        public List<Player> Players { get; set; } 
        public List<Card> CommunityCards { get; set; } 
        public string WinnerText { get; set; }

        public Game(List<Player> players, List<Card> deck)
        {
            Deck = deck;
            Players = players;
            CommunityCards = new List<Card>();
        }

        public void Deal()
        {
            for (var i = 0; i < 2; i++)
            {
                foreach (var player in Players)
                {
                    player.HoleCards.Add(Deck[0]);
                    Deck.Remove(Deck[0]);
                }
            }

            for (var i = 0; i < 5; i++)
            {
                CommunityCards.Add(Deck[0]);
                Deck.Remove(Deck[0]);
            }
        }

        public void GetPlayerFinalHands()
        {
            foreach (var player in Players)
            {
                player.BestFinalHand = GetFinalHand(player);
            }

            var playersInOrderOfHandRank = Players.OrderBy(x => x.BestFinalHand.Rank).ToArray();

            var winners = new List<Player> {playersInOrderOfHandRank.First()};

            for (var i = 0; i < playersInOrderOfHandRank.Count() - 1; i++)
            {
                var diff = playersInOrderOfHandRank[i + 1].BestFinalHand.Rank - playersInOrderOfHandRank[i].BestFinalHand.Rank;

                if (diff == 0)
                {
                    winners.Add(playersInOrderOfHandRank[i+1]);
                }
                else
                {
                    i = playersInOrderOfHandRank.Count() - 1;
                }
            }

            //if (winners.Count > 1)
            //    winners = CheckWinnersListForHigherHandOfSameRank(winners);

            if (winners.Count == 1)
            {
                WinnerText = winners[0].Name + " is the winner with " + winners[0].BestFinalHand.HandDescription;
            }
            else
            {
                var winnerString = string.Empty;

                for (var i = 0; i < winners.Count; i++)
                {
                    if (i == 0)
                    {
                        winnerString = winners[i].Name;
                    }
                    else if (i == winners.Count - 1)
                    {
                        winnerString = winnerString + " and " + winners[i].Name;
                    }
                    else
                    {
                        winnerString = winnerString + ", " + winners[i].Name;
                    }                    
                }

                WinnerText = winnerString + " share the pot with " + winners[0].BestFinalHand.HandDescription;
            }
        }

        //private List<Player> CheckWinnersListForHigherHandOfSameRank(List<Player> winnerList)
        //{
        //    var finalWinnerList = new List<Player>();

        //    var winningHandType = winnerList[0].BestFinalHand;

        //    if (winningHandType is Pair)
        //    {
        //        var winnersWithHighCard = new Dictionary<Player, int>();
        //        foreach (var winner in winnerList)
        //        {
        //            var playerHand =winner.BestFinalHand as Pair;
        //            var highCard = playerHand.HighestCard.NumericValue;
        //            winnersWithHighCard.Add(winner, highCard);
        //        }   
        //    }

        //    return new List<Player>();
        //}

        private Hand GetFinalHand(Player player)
        {
            player.FinalHand = new Hand(player.HoleCards.ToList());
            player.FinalHand.Cards.AddRange(CommunityCards);

            var counts = new Dictionary<CardValue, int>();
            foreach (var card in player.FinalHand.Cards)
            {
                int count;
                if (counts.Any(x => x.Key.NumericValue == card.CardValue.NumericValue))
                {
                    count = counts.Where(x => x.Key.NumericValue == card.CardValue.NumericValue).Count();
                    counts[card.CardValue] = count + 1;
                }
                else
                {
                    counts.Add(card.CardValue, 1);
                }
            }

            var highestCard = player.FinalHand.Cards.OrderByDescending(x => x.CardValue.NumericValue).FirstOrDefault();

            var mostPopularCardValue = GetMostPopularCardValue(counts, 0);

            foreach (var count in counts)
            {
                if (count.Key.NumericValue == mostPopularCardValue.NumericValue)
                    counts.Remove(mostPopularCardValue);
            }

            var secondMostPopularCardValue = GetMostPopularCardValue(counts, 1);

            var mostPopularCardValueCount = player.FinalHand.Cards.Count(item => item.CardValue.NumericValue == mostPopularCardValue.NumericValue);
            var secondMostPopularCardValueCount = player.FinalHand.Cards.Count(item => item.CardValue.NumericValue == secondMostPopularCardValue.NumericValue);

            var straightFlushHand = PlayerHasStraightFlush(player) as StraightFlush;
            if (straightFlushHand != null)
            {
                return new StraightFlush(straightFlushHand.HighestCard, straightFlushHand.Suit);
            }

            if (mostPopularCardValueCount >= 4)
            {
                return new FourOfAKind(mostPopularCardValue);
            }

            if (mostPopularCardValueCount == 3 &&
                (secondMostPopularCardValueCount == 2 || secondMostPopularCardValueCount == 3))
            {
                return new FullHouse(mostPopularCardValue, secondMostPopularCardValue);
            }

            if (PlayerHasFlush(player) != null)
            {
                return PlayerHasFlush(player);
            }

            if (PlayerHasStraight(player.FinalHand) != null)
            {
                return PlayerHasStraight(player.FinalHand);
            }

            if (mostPopularCardValueCount == 3 && secondMostPopularCardValueCount <= 1)
            {
                return new ThreeOfAKind(mostPopularCardValue);
            }

            if (mostPopularCardValueCount == 2 && secondMostPopularCardValueCount == 2)
            {
                return new TwoPair(mostPopularCardValue, secondMostPopularCardValue);
            }

            if (mostPopularCardValueCount == 2 && secondMostPopularCardValueCount < 2)
            {
                return new Pair(mostPopularCardValue);
            }
            return new HighCard(highestCard.CardValue);
        }

        private Hand PlayerHasStraightFlush(Player player)
        {
            var flushHand = PlayerHasFlush(player);

            if (flushHand == null)
                return null;

            string suit = flushHand.Suit;

            foreach (var card in flushHand.Cards)
            {
                if (card.Suit != flushHand.Suit)
                {
                    flushHand.Cards.Remove(card);
                }
            }


            if (PlayerHasStraight(flushHand) != null)
            {
                var highestCard = PlayerHasStraight(flushHand).HighestCard;

                return new StraightFlush(highestCard, suit);
            }

            return null;
        }

        private Straight PlayerHasStraight(Hand hand)
        {
            var cardValues = (from card in hand.Cards
                                 select card.CardValue).ToList();

            var longestSequence = 0;
            CardValue highestCard = null;

            var sortedInput = cardValues.OrderBy(x => x.NumericValue).ToList();

            var diffArray = new List<int>();

            for (var i = 0; i < sortedInput.Count() - 1; i++)
            {
                var diff = sortedInput[i + 1].NumericValue - sortedInput[i].NumericValue;
                diffArray.Add(diff);

                if (diff == 1)
                    highestCard = sortedInput[i + 1];
            }

            var currentLongestSequence = 0;

            foreach (var i in diffArray)
            {
                if (i == 1)
                {
                    currentLongestSequence++;
                    if (currentLongestSequence > longestSequence)
                        longestSequence = currentLongestSequence;

                }
                else if (i == 0)
                {
                    
                }
                else
                {
                    currentLongestSequence = 0;
                }
            }

            if (longestSequence >= 4)
            {
                return new Straight(highestCard);
            }

            return null;
        }

        private Flush PlayerHasFlush(Player player)
        {
            
            var clubs = (from card in player.FinalHand.Cards
                         where (card.Suit == "Clubs")
                         select card);
            if (clubs.Count() >= 5)
            {
                var highestClub = clubs.OrderByDescending(x => x.CardValue.NumericValue).FirstOrDefault().CardValue;

                var flush = new Flush(highestClub, "Clubs") { Cards = new List<Card>() }; 

                foreach (var card in clubs)
                {
                    flush.Cards.Add(card);
                }
                return flush;

            }

            var diamonds = (from card in player.FinalHand.Cards
                            where card.Suit == "Diamonds"
                            select card);
            if (diamonds.Count() >= 5)
            {
                var highestDiamond = diamonds.OrderByDescending(x => x.CardValue.NumericValue).FirstOrDefault().CardValue;

                var flush = new Flush(highestDiamond, "Diamonds") { Cards = new List<Card>() }; 

                foreach (var card in diamonds)
                {
                    flush.Cards.Add(card);
                }
                return flush;
            }

            var spades = (from card in player.FinalHand.Cards
                          where card.Suit == "Spades"
                          select card);
            if (spades.Count() >= 5)
            {
                var highestSpade = spades.OrderByDescending(x => x.CardValue.NumericValue).FirstOrDefault().CardValue;
                var flush = new Flush(highestSpade, "Spades") { Cards = new List<Card>() };
                
                foreach (var card in spades)
                {
                    flush.Cards.Add(card);
                }
                return flush;
            }

            var hearts = (from card in player.FinalHand.Cards
                          where card.Suit == "Hearts"
                          select card);
            if (hearts.Count() >= 5)
            {
                var highestHeart = hearts.OrderByDescending(x => x.CardValue.NumericValue).FirstOrDefault().CardValue;
                var flush = new Flush(highestHeart, "Hearts") {Cards = new List<Card>()};

                foreach (var card in hearts)
                {
                    flush.Cards.Add(card);
                }
                return flush;
            }
            return null;
        }

        private CardValue GetMostPopularCardValue(Dictionary<CardValue, int> counts, int index)
        {
            var mostPopular = counts.Distinct().OrderByDescending(s => s.Value).ToList();
            return mostPopular[index].Key;
        }
    }
}