using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class SiPrefixTests
    {

        // Default constructor sets zero and empty strings
        public static void DefaultConstructorSetsZeroAndEmpty() {

            SiPrefix prefix = new SiPrefix();

            Check.Equal(0, prefix.Exponent);
            Check.Equal(string.Empty, prefix.Symbol);
            Check.Equal(string.Empty, prefix.Description);
        }

        // Constructor sets all fields
        public static void ConstructorSetsFields() {

            SiPrefix prefix = new SiPrefix(3, "k", "kilo");

            Check.Equal(3, prefix.Exponent);
            Check.Equal("k", prefix.Symbol);
            Check.Equal("kilo", prefix.Description);
        }

    }
}
