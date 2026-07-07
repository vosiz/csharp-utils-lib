using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class AssertTests
    {

        // OnNull throws for null
        public static void OnNullThrowsForNull() {

            Check.Throws<AssertException>(() => Assert.OnNull(null));
        }

        // OnNull does not throw for non-null
        public static void OnNullDoesNotThrowForNonNull() {

            Assert.OnNull("value");
        }

        // OnType throws for a type mismatch
        public static void OnTypeThrowsForMismatch() {

            Check.Throws<AssertException>(() => Assert.OnType("string value", typeof(int)));
        }

        // OnType does not throw for a matching type
        public static void OnTypeDoesNotThrowForMatch() {

            Assert.OnType("string value", typeof(string));
        }

    }
}
