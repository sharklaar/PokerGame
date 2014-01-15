using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PokerGame.Library;

namespace PokerGame.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private Game CreateGame()
        {
            var players = new List<Player>
            {
                new Player("Marc"),
                new Player("Dave"),
                new Player("Lee"),
                new Player("Oli")
            };

            var deck = CreateDeck();

            return new Game(players, deck);
        }

        private List<Card> CreateDeck()
        {

            var suits = Enum.GetValues(typeof (Enums.Suit)).Cast<Enums.Suit>();
            var cardValues = Enum.GetValues(typeof(Enums.CardValue)).Cast<Enums.CardValue>();

            var deck = (from cardValue in cardValues from suit in suits select new Card(suit, cardValue)).ToList();

            return deck;
        }
    }
}
