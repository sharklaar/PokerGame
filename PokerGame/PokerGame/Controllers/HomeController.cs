using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using PokerGame.Library;

namespace PokerGame.Controllers
{
    public class HomeController : Controller
    {
        private List<Card> _deck;
        private Game _game;

        public ActionResult Index()
        {
            _game = CreateGame();

            Deal();

            GetPlayerFinalHands();

            return View(_game);
        }

        private Game CreateGame()
        {
            var players = new List<Player>
            {
                new Player("Marc", new List<Card>()),
                new Player("Dave", new List<Card>()),
                new Player("Lee", new List<Card>()),
                new Player("Oli", new List<Card>())
            };

            _deck = CreateDeck();
            Shuffle();
            return new Game(players, _deck);
        }

        public void Deal()
        {
            for (var i = 0; i < 2; i++)
            {
                foreach (var player in _game.Players)
                {
                    player.HoleCards.Add(_game.Deck[0]);
                    _game.Deck.Remove(_game.Deck[0]);
                }
            }

            for (var i = 0; i < 5; i++)
            {
                _game.CommunityCards.Add(_game.Deck[0]);
                _game.Deck.Remove(_game.Deck[0]);
            }
        }

        public void GetPlayerFinalHands()
        {
            foreach (var player in _game.Players)
            {
                player.BestFinalHand = GetFinalHand(player);
            }
        }

        private Enums.Hands GetFinalHand(Player player)
        {
            player.FinalHand = new Hand(player.HoleCards.ToList());
            player.FinalHand.Cards.AddRange(_game.CommunityCards);

            var counts = new Dictionary<string, int>();
            foreach (var card in player.FinalHand.Cards)
            {
                int count;
                if (counts.TryGetValue(card.Value, out count))
                {
                    counts[card.Value] = count + 1;
                }
                else
                {
                    counts.Add(card.Value, 1);
                }
            }

            var mostPopularCardValue = GetMostPopularCardValue(counts, 0);
            var secondMostPopularCardValue = GetMostPopularCardValue(counts, 1);

            var mostPopularCardValueCount = player.FinalHand.Cards.Count(item => item.Value == mostPopularCardValue);
            var secondMostPopularCardValueCount = player.FinalHand.Cards.Count(item => item.Value == secondMostPopularCardValue);

            //if (PlayerHasStraightFlush())
            //{
            //    return Enums.Hands.StraightFlush;
            //}

            if (mostPopularCardValueCount >= 4)
            {
                return Enums.Hands.FourOfAKind;
            }

            if (mostPopularCardValueCount == 3 &&
                (secondMostPopularCardValueCount == 2 || secondMostPopularCardValueCount == 3))
            {
                return Enums.Hands.FullHouse;
            }

            if (PlayerHasFlush(player))
            {
                return Enums.Hands.Flush;
            }

            if (PlayerHasStraight(player))
            {
                return Enums.Hands.Straight;
            }

            if (mostPopularCardValueCount == 3 && secondMostPopularCardValueCount <= 1)
            {
                return Enums.Hands.ThreeOfAKind;
            }

            if (mostPopularCardValueCount == 2 && secondMostPopularCardValueCount == 2)
            {
                return Enums.Hands.TwoPair;
            }

            if (mostPopularCardValueCount == 2 && secondMostPopularCardValueCount < 2)
            {
                return Enums.Hands.Pair;
            }
            return Enums.Hands.HighCard;
        }

        private bool PlayerHasStraight(Player player)
        {
            var numericValues = from card in player.FinalHand.Cards
                select card.NumericValue;

            for (int i = 0; i < numericValues.Count(); i++)
            {
                
            }

            return false;
        }

        private bool PlayerHasFlush(Player player)
        {
            var clubs = (from card in player.FinalHand.Cards
                         where card.Suit == "Clubs"
                         select card.Suit).Count();

            var diamonds = (from card in player.FinalHand.Cards
                            where card.Suit == "Diamonds"
                            select card.Suit).Count();

            var spades = (from card in player.FinalHand.Cards
                          where card.Suit == "Spades"
                          select card).Count();

            var hearts = (from card in player.FinalHand.Cards
                          where card.Suit == "Hearts"
                          select card.Suit).Count();

            if (clubs >= 5 || diamonds >= 5 || spades >= 5 || hearts >= 5)
            {
                return true;
            }
            return false;
        }

        private string GetMostPopularCardValue(Dictionary<string, int> counts, int index)
        {
            var mostPopular = counts.Distinct().OrderByDescending(s => s.Value).ToList();
            return mostPopular[index].Key;
        }

        private static List<Card> CreateDeck()
        {
            var suits = GetSuits();

            var cardValues = GetCardValues();

            var deck = new List<Card>();

            foreach (var currentCard in cardValues)
            {
                for (var s = 0; s < suits.GetLength(0); s++)
                {
                    var cardClass = string.Concat(currentCard.Key, " ", suits[s, 0]);
                    var card = new Card(suits[s, 0], currentCard.Key, cardClass, currentCard.Value);
                    deck.Add(card);
                }
            }

            return deck;
        }

        private static string[,] GetSuits()
        {
            var suits = new string[4, 2]
            {
                {"Hearts", "h"},
                {"Spades", "s"},
                {"Diamonds", "d"},
                {"Clubs", "c"}
            };
            return suits;
        }

        private static Dictionary<string, int> GetCardValues()
        {
            var cardValues = new Dictionary<string, int>
            {
                {"Ace", 1},
                {"Deuce", 2},
                {"Three", 3},
                {"Four", 4},
                {"Five", 5},
                {"Six", 6},
                {"Seven", 7},
                {"Eight", 8},
                {"Nine", 9},
                {"Ten", 10},
                {"Jack", 11},
                {"Queen", 12},
                {"King", 13}
            };
            return cardValues;
        }

        private void Shuffle()
        {
            var random = new Random();
            var count = _deck.Count;

            while (count > 1)
            {
                count--;
                var i = random.Next(count + 1);
                var card = _deck[i];
                _deck[i] = _deck[count];
                _deck[count] = card;
            }
        }
    }
}
