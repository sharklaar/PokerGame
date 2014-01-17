using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using PokerGame.Controllers;
using PokerGame.Library;

namespace PokerGame.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private HomeController _controller = new HomeController();
        private List<Card> _deck;
        private Game _game;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            CreateGame();
            _game.Deal();
        }

        [Test]
        public void Dealer_deals_twp_cards_to_each_player()
        {
            Assert.That(_game.Players[0].HoleCards.Count, Is.EqualTo(2));
        }

        [Test]
        public void Dealer_deals_five_community_cards()
        {
            Assert.That(_game.CommunityCards.Count, Is.EqualTo(5));
        }

        [Test]
        public void Player_with_straight_flush_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Five", "five diamonds", 5));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Hearts", "Six", "six spades", 6));
            _game.CommunityCards.Add(new Card("Hearts", "Seven", "seven clubs", 7));
            _game.CommunityCards.Add(new Card("Hearts", "Eight", "eight clubs", 8));
            _game.CommunityCards.Add(new Card("Spades", "Five", "five clubs", 5));
            _game.CommunityCards.Add(new Card("Clubs", "Five", "five clubs", 5));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<StraightFlush>());
        }

        [Test]
        public void Player_with_four_of_a_kind_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Diamonds", "Four", "four diamonds", 4));
            
            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Spades", "Four", "four spades", 4));
            _game.CommunityCards.Add(new Card("Clubs", "Four", "four clubs", 4));
            _game.CommunityCards.Add(new Card("Clubs", "Five", "five clubs", 5));
            _game.CommunityCards.Add(new Card("Spades", "Five", "five clubs", 5));
            _game.CommunityCards.Add(new Card("Hearts", "Five", "five clubs", 5));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<FourOfAKind>());
        }

        [Test]
        public void Player_with_full_house_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Diamonds", "Four", "four diamonds", 4));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Spades", "Four", "four spades", 4));
            _game.CommunityCards.Add(new Card("Clubs", "Seven", "seven clubs", 7));
            _game.CommunityCards.Add(new Card("Clubs", "Five", "five clubs", 5));
            _game.CommunityCards.Add(new Card("Spades", "Five", "five clubs", 5));
            _game.CommunityCards.Add(new Card("Hearts", "Ten", "Ten clubs", 10));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<FullHouse>());
        }

        [Test]
        public void Player_with_flush_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Five", "five hearts", 5));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Hearts", "Ten", "ten hearts", 10));
            _game.CommunityCards.Add(new Card("Hearts", "Jack", "jack hearts", 11));
            _game.CommunityCards.Add(new Card("Hearts", "Seven", "seven hearts", 7));
            _game.CommunityCards.Add(new Card("Spades", "Five", "five clubs", 5));
            _game.CommunityCards.Add(new Card("Hearts", "Ten", "Ten clubs", 10));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<Flush>());
        }

        [Test]
        public void Player_with_straight_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Five", "five hearts", 5));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Spades", "Six", "six hearts", 6));
            _game.CommunityCards.Add(new Card("Spades", "Seven", "seven hearts", 7));
            _game.CommunityCards.Add(new Card("Clubs", "Eight", "eight hearts", 8));
            _game.CommunityCards.Add(new Card("Spades", "Ten", "ten spades", 10));
            _game.CommunityCards.Add(new Card("Hearts", "Jack", "jack hearts", 11));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<Straight>());
        }

        [Test]
        public void Player_with_three_of_a_kind_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Clubs", "Four", "four clubs", 4));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Spades", "Four", "four spades", 4));
            _game.CommunityCards.Add(new Card("Spades", "Seven", "seven hearts", 7));
            _game.CommunityCards.Add(new Card("Clubs", "Eight", "eight hearts", 8));
            _game.CommunityCards.Add(new Card("Spades", "Ten", "ten spades", 10));
            _game.CommunityCards.Add(new Card("Hearts", "Jack", "jack hearts", 11));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<ThreeOfAKind>());
        }

        [Test]
        public void Player_with_two_pair_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Clubs", "Five", "five clubs", 5));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Spades", "Four", "four spades", 4));
            _game.CommunityCards.Add(new Card("Spades", "Five", "five spades", 5));
            _game.CommunityCards.Add(new Card("Clubs", "Eight", "eight hearts", 8));
            _game.CommunityCards.Add(new Card("Spades", "Ten", "ten spades", 10));
            _game.CommunityCards.Add(new Card("Hearts", "Jack", "jack hearts", 11));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<TwoPair>());
        }

        [Test]
        public void Player_with_one_pair_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Clubs", "Five", "five clubs", 5));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Spades", "Four", "four spades", 4));
            _game.CommunityCards.Add(new Card("Spades", "Six", "six spades", 6));
            _game.CommunityCards.Add(new Card("Clubs", "Eight", "eight hearts", 8));
            _game.CommunityCards.Add(new Card("Spades", "Ten", "ten spades", 10));
            _game.CommunityCards.Add(new Card("Hearts", "Jack", "jack hearts", 11));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<Pair>());
        }

        [Test]
        public void Player_with_high_card_has_correct_best_hand()
        {
            _game.Players[0].HoleCards.Clear();
            _game.Players[0].HoleCards.Add(new Card("Hearts", "Four", "four hearts", 4));
            _game.Players[0].HoleCards.Add(new Card("Clubs", "Five", "five clubs", 5));

            _game.CommunityCards.Clear();
            _game.CommunityCards.Add(new Card("Spades", "King", "king spades", 13));
            _game.CommunityCards.Add(new Card("Spades", "Seven", "seven spades", 7));
            _game.CommunityCards.Add(new Card("Clubs", "Eight", "eight hearts", 8));
            _game.CommunityCards.Add(new Card("Spades", "Ten", "ten spades", 10));
            _game.CommunityCards.Add(new Card("Hearts", "Jack", "jack hearts", 11));

            _game.GetPlayerFinalHands();

            Assert.That(_game.Players[0].BestFinalHand, Is.InstanceOf<HighCard>());
        }

        private void CreateGame()
        {
            var players = new List<Player>
            {
                new Player("Marc", new List<Card>()),
                new Player("Dave", new List<Card>()),
                new Player("Lee", new List<Card>()),
                new Player("Oli", new List<Card>())
            };

            _deck = _controller.CreateDeck();
            Shuffle();
            _game = new Game(players, _deck);
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
