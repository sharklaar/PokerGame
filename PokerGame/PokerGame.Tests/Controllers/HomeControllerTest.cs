using System.Collections.Generic;
using NUnit.Framework;
using PokerGame.Library;

namespace PokerGame.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [TestFixture]
        public class TheHomeController
        {
            private Game _game;

            [Test]
            public void The_dealer_will_deal_two_cards_to_each_player()
            {
                // Arrange
                _game = CreateGame();
            }
        }

        private static Game CreateGame()
        {
            var players = new List<Player>
            {
                new Player("Marc"),
                new Player("Dave"),
                new Player("Lee"),
                new Player("Oli")
            };

            return new Game(players);
        }
    }
}
