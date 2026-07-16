using System;
using System.Linq;
using Vosiz.Utils;

namespace Tests.Utils
{

    public static class RandomizerTests
    {

        // Next produces a value without ever calling Init() explicitly first
        public static void NextWorksWithoutExplicitInit() {

            int value = Randomizer.Next(10);

            Check.True(value >= 0 && value < 10, "value out of range");
        }

        // Next with a min and max stays within the inclusive/exclusive bounds
        public static void NextWithRangeStaysWithinBounds() {

            for (int i = 0; i < 50; i++) {

                int value = Randomizer.Next(5, 10);

                Check.True(value >= 5 && value < 10, "value out of range");
            }
        }

        // Next throws when min is greater than max
        public static void NextThrowsWhenMinGreaterThanMax() {

            Check.Throws<ArgumentOutOfRangeException>(() => Randomizer.Next(10, 5));
        }

        // NextDouble stays within the given bounds
        public static void NextDoubleStaysWithinBounds() {

            double value = Randomizer.NextDouble(1.0, 2.0);

            Check.True(value >= 1.0 && value <= 2.0, "value out of range");
        }

        // NextFloat stays within the given bounds
        public static void NextFloatStaysWithinBounds() {

            float value = Randomizer.NextFloat(1.0f, 2.0f);

            Check.True(value >= 1.0f && value <= 2.0f, "value out of range");
        }

        // NextByte stays within the byte range
        public static void NextByteStaysWithinRange() {

            byte value = Randomizer.NextByte();

            Check.True(value >= 0 && value <= 255, "value out of range");
        }

        // NextChar returns a character from the expected alphabet
        public static void NextCharReturnsAlphanumericChar() {

            const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char value = Randomizer.NextChar();

            Check.True(alphabet.IndexOf(value) >= 0, "value outside expected alphabet");
        }

        // NextString returns a string of the requested length
        public static void NextStringReturnsRequestedLength() {

            string value = Randomizer.NextString(15);

            Check.Equal(15, value.Length);
        }

        // NextDecimal stays within the given bounds
        public static void NextDecimalStaysWithinBounds() {

            decimal value = Randomizer.NextDecimal(1.0m, 2.0m);

            Check.True(value >= 1.0m && value <= 2.0m, "value out of range");
        }

        // NextUshort stays within the given bounds
        public static void NextUshortStaysWithinBounds() {

            ushort value = Randomizer.NextUshort(10, 20);

            Check.True(value >= 10 && value <= 20, "value out of range");
        }

        // NextUint stays within the given bounds
        public static void NextUintStaysWithinBounds() {

            uint value = Randomizer.NextUint(10, 20);

            Check.True(value >= 10 && value <= 20, "value out of range");
        }

        // NextLong stays within the given bounds
        public static void NextLongStaysWithinBounds() {

            long value = Randomizer.NextLong(100L, 200L);

            Check.True(value >= 100L && value <= 200L, "value out of range");
        }

        // NextLong throws when min is greater than max
        public static void NextLongThrowsWhenMinGreaterThanMax() {

            Check.Throws<ArgumentOutOfRangeException>(() => Randomizer.NextLong(200L, 100L));
        }

        // NextULong stays within the given bounds
        public static void NextULongStaysWithinBounds() {

            ulong value = Randomizer.NextULong(100UL, 200UL);

            Check.True(value >= 100UL && value <= 200UL, "value out of range");
        }

        // NextDateTime stays within the given bounds
        public static void NextDateTimeStaysWithinBounds() {

            DateTime min = new DateTime(2020, 1, 1);
            DateTime max = new DateTime(2020, 12, 31);

            DateTime value = Randomizer.NextDateTime(min, max);

            Check.True(value >= min && value <= max.AddDays(1), "value out of range");
        }

        // Generic Next for an enum never returns an ignored value
        public static void NextEnumNeverReturnsIgnoredValue() {

            for (int i = 0; i < 20; i++) {

                DayOfWeek value = Randomizer.Next(DayOfWeek.Saturday, DayOfWeek.Sunday);

                Check.False(value == DayOfWeek.Saturday || value == DayOfWeek.Sunday, "should not return a weekend day");
            }
        }

        // Generic Next<T> can build a simple primitive value
        public static void NextGenericBuildsPrimitiveValue() {

            int value = Randomizer.Next<int>();

            Check.True(value >= 0 && value < 1000, "value out of expected generated range");
        }

        // Choice returns one of the given values
        public static void ChoiceReturnsOneOfGivenValues() {

            int[] options = { 10, 20, 30 };
            int value = Randomizer.Choice(options);

            Check.True(options.Contains(value), "Choice should return one of the provided values");
        }

        // Choice with a single value always returns that value
        public static void ChoiceWithSingleValueReturnsThatValue() {

            Check.Equal(42, Randomizer.Choice(42));
        }

    }
}
