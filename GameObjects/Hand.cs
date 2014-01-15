using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class Hand
    {
        private List<Card> _cards;
 
        public Hand(List<Card> cards)
        {
            _cards = cards;
        }
    }
}
