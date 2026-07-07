using System;
using System.Collections.Generic;
using System.Text;

namespace Vosiz.Commons
{

    public class SiPrefix
    {

        public int Exponent { private set; get; }
        public string Symbol { private set; get; }
        public string Description { private set; get; }


        // Constructor
        public SiPrefix() {

            Exponent = 0;
            Symbol = string.Empty;
            Description = string.Empty;
        }

        // Constructor with exponent, symbol and description
        public SiPrefix(int exponent, string symbol, string description) {

            Exponent = exponent;
            Symbol = symbol;
            Description = description;
        }

    }
}
