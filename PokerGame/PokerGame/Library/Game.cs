using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Library
{
    public class Game
    {
        public List<Card> Deck { get; set; } 
        public List<Player> Players { get; set; } 
        public List<Card> CommunityCards { get; set; } 

        public Game(List<Player> players, List<Card> deck)
        {
            Deck = deck;
            Players = players;
            CommunityCards = new List<Card>();
        }
    }
}