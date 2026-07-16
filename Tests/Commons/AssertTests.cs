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

        // OnType throws AssertException (not NullReferenceException) for a null object
        public static void OnTypeThrowsForNull() {

            Check.Throws<AssertException>(() => Assert.OnType(null, typeof(string)));
        }

        // OnTrue throws for a false condition
        public static void OnTrueThrowsForFalse() {

            Check.Throws<AssertException>(() => Assert.OnTrue(false));
        }

        // OnTrue does not throw for a true condition
        public static void OnTrueDoesNotThrowForTrue() {

            Assert.OnTrue(true);
        }

        // OnFalse throws for a true condition
        public static void OnFalseThrowsForTrue() {

            Check.Throws<AssertException>(() => Assert.OnFalse(true));
        }

        // OnFalse does not throw for a false condition
        public static void OnFalseDoesNotThrowForFalse() {

            Assert.OnFalse(false);
        }

        // OnEqual throws for unequal values
        public static void OnEqualThrowsForUnequalValues() {

            Check.Throws<AssertException>(() => Assert.OnEqual(1, 2));
        }

        // OnEqual does not throw for equal values
        public static void OnEqualDoesNotThrowForEqualValues() {

            Assert.OnEqual(1, 1);
        }

    }
}
