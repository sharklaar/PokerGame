using System.Collections.Generic;
using GameObjects;

namespace GameObjects
{
    public class Game
    {
        private List<Player> _players;

        public Game(List<Player> players)
        {
            _players = players;
        }
    }
}
