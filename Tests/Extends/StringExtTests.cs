using System;
using Vosiz.Extends;

namespace Tests.Extends
{

    public static class StringExtTests
    {

        // ToByteArray encodes the string as UTF-8
        public static void ToByteArrayEncodesUtf8() {

            byte[] bytes = "abc".ToByteArray();

            Check.Equal(3, bytes.Length);
            Check.Equal((byte)'a', bytes[0]);
            Check.Equal((byte)'b', bytes[1]);
            Check.Equal((byte)'c', bytes[2]);
        }

        // Limit shortens a string longer than maxLength
        public static void LimitShortensLongString() {

            Check.Equal("abc", "abcdef".Limit(3));
        }

        // Limit leaves a shorter string untouched
        public static void LimitLeavesShortStringUntouched() {

            Check.Equal("ab", "ab".Limit(3));
        }

        // Limit returns null for a null input
        public static void LimitReturnsNullForNull() {

            string value = null;

            Check.Equal(null, value.Limit(3));
        }

        // RandomSubstring on an empty string returns empty
        public static void RandomSubstringOnEmptyStringReturnsEmpty() {

            Check.Equal(string.Empty, string.Empty.RandomSubstring());
        }

        // RandomSubstring with explicit index and length returns the exact slice
        public static void RandomSubstringWithExplicitIndexAndLength() {

            Check.Equal("cde", "abcdef".RandomSubstring(2, 3));
        }

        // RandomSubstring clamps the length so it does not run past the end of the string
        public static void RandomSubstringClampsLengthToStringEnd() {

            Check.Equal("ef", "abcdef".RandomSubstring(4, 10));
        }

        // RandomSubstring returns empty when the index is past the end of the string
        public static void RandomSubstringReturnsEmptyForIndexPastEnd() {

            Check.Equal(string.Empty, "abc".RandomSubstring(10));
        }

        // TryParseEnum parses a valid value
        public static void TryParseEnumParsesValidValue() {

            bool ok = "Monday".TryParseEnum(typeof(DayOfWeek), false, out object result);

            Check.True(ok, "Should successfully parse");
            Check.Equal(DayOfWeek.Monday, result);
        }

        // TryParseEnum honors ignore_case
        public static void TryParseEnumHonorsIgnoreCase() {

            bool ok = "monday".TryParseEnum(typeof(DayOfWeek), true, out object result);

            Check.True(ok, "Should successfully parse with ignored case");
            Check.Equal(DayOfWeek.Monday, result);
        }

        // TryParseEnum returns false for an invalid value
        public static void TryParseEnumReturnsFalseForInvalidValue() {

            bool ok = "NotADay".TryParseEnum(typeof(DayOfWeek), false, out object result);

            Check.False(ok, "Should fail to parse");
        }

        // TryParseEnum throws when the given type is not an enum
        public static void TryParseEnumThrowsForNonEnumType() {

            Check.Throws<ArgumentException>(() => "value".TryParseEnum(typeof(string), false, out object result));
        }

    }
}
