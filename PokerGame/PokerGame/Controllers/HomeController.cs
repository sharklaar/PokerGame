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
            PlayGame();
            return View(_game);
        }

        public Game CreateGame()
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

        public void PlayGame()
        {
            _game.Deal();

            _game.GetPlayerFinalHands();
        }
      

        public List<Card> CreateDeck()
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
