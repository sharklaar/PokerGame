using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokerGame.Library
{
    public class CardValue
    {
        public string StringValue { get; set; }
        public int NumericValue { get; set; }

        public CardValue(string stringValue, int numericValue)
        {
            StringValue = stringValue;
            NumericValue = numericValue;
        }
    }
}