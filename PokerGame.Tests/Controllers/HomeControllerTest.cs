using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GameObjects;
using PokerGame;
using PokerGame.Controllers;
using NUnit.Framework;

namespace PokerGame.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        private Game _game;
        
        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [Test]
        public void There_are_52_unique_cards_in_the_pack()
        {


            Assert.Fail();
        }

        [Test]
        public void Dealer_deals_two_cards_to_each_player()
        {
            Assert.Fail();
            
        }

        [Test]
        public void Dealer_deals_a_5_card_flop()
        {
            Assert.Fail();
            
        }

        [Test]
        public void Dealer_deals_the_river()
        {
            Assert.Fail();
            
        }

        [Test]
        public void Dealer_deals_the_turn()
        {
            Assert.Fail();
            
        }

        [Test]
        public void Hands_are_shown()
        {
            Assert.Fail();
            
        }

        [Test]
        public void Greatest_value_hand_wins_the_round()
        {
            Assert.Fail();
            
        }



        private void CreateGame()
        {
            var players = new List<Player>();
            var player1 = new Player("Marc");
            var player2 = new Player("Dave");
            var player3 = new Player("Lee");
            var player4 = new Player("Oli");

            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
            players.Add(player4);

            _game = new Game(players);
        }







    }
}
