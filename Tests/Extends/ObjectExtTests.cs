using System;
using Vosiz.Extends;

namespace Tests.Extends
{

    public static class ObjectExtTests
    {

        // TryConvert returns the same instance when already of the target type
        public static void TryConvertReturnsSameInstanceForMatchingType() {

            object value = "hello";

            bool ok = value.TryConvert(typeof(string), out object result);

            Check.True(ok, "Should succeed for a matching type");
            Check.Equal("hello", result);
        }

        // TryConvert converts a compatible IConvertible value
        public static void TryConvertConvertsCompatibleValue() {

            object value = "42";

            bool ok = value.TryConvert(typeof(int), out object result);

            Check.True(ok, "Should succeed for a convertible value");
            Check.Equal(42, result);
        }

        // TryConvert parses a string into the requested enum type
        public static void TryConvertParsesStringIntoEnum() {

            object value = "Monday";

            bool ok = value.TryConvert(typeof(DayOfWeek), out object result);

            Check.True(ok, "Should succeed for a valid enum name");
            Check.Equal(DayOfWeek.Monday, result);
        }

        // TryConvert returns true with a null result for a null reference type
        public static void TryConvertReturnsNullForNullOnReferenceType() {

            object value = null;

            bool ok = value.TryConvert(typeof(string), out object result);

            Check.True(ok, "Should succeed for a null reference type");
            Check.Equal(null, result);
        }

        // TryConvert returns false for a null value type
        public static void TryConvertReturnsFalseForNullOnValueType() {

            object value = null;

            bool ok = value.TryConvert(typeof(int), out object result);

            Check.False(ok, "Should fail for a null value type");
        }

        // TryConvert returns false for an incompatible value
        public static void TryConvertReturnsFalseForIncompatibleValue() {

            object value = "not a number";

            bool ok = value.TryConvert(typeof(int), out object result);

            Check.False(ok, "Should fail for an incompatible value");
        }

    }
}
