using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class LimitedNumberTests
    {

        // RandomNumber stays within range for int
        public static void RandomNumberWithinRangeInt() {

            LimitedNumber<int> limited = new LimitedNumber<int>("int", 5, 10);
            int value = limited.RandomNumber();

            Check.True(value >= 5 && value <= 10, "int out of range");
        }

        // RandomNumber stays within range for long
        public static void RandomNumberWithinRangeLong() {

            LimitedNumber<long> limited = new LimitedNumber<long>("long", 100L, 200L);
            long value = limited.RandomNumber();

            Check.True(value >= 100L && value <= 200L, "long out of range");
        }

        // RandomNumber stays within range for short
        public static void RandomNumberWithinRangeShort() {

            LimitedNumber<short> limited = new LimitedNumber<short>("short", (short)1, (short)5);
            short value = limited.RandomNumber();

            Check.True(value >= 1 && value <= 5, "short out of range");
        }

        // RandomNumber stays within range for byte
        public static void RandomNumberWithinRangeByte() {

            LimitedNumber<byte> limited = new LimitedNumber<byte>("byte", (byte)1, (byte)5);
            byte value = limited.RandomNumber();

            Check.True(value >= 1 && value <= 5, "byte out of range");
        }

        // RandomNumber stays within range for decimal
        public static void RandomNumberWithinRangeDecimal() {

            LimitedNumber<decimal> limited = new LimitedNumber<decimal>("decimal", 1.0m, 2.0m);
            decimal value = limited.RandomNumber();

            Check.True(value >= 1.0m && value <= 2.0m, "decimal out of range");
        }

        // RandomNumber stays within range for double
        public static void RandomNumberWithinRangeDouble() {

            LimitedNumber<double> limited = new LimitedNumber<double>("double", 1.0, 2.0);
            double value = limited.RandomNumber();

            Check.True(value >= 1.0 && value <= 2.0, "double out of range");
        }

        // RandomNumber stays within range for float
        public static void RandomNumberWithinRangeFloat() {

            LimitedNumber<float> limited = new LimitedNumber<float>("float", 1.0f, 2.0f);
            float value = limited.RandomNumber();

            Check.True(value >= 1.0f && value <= 2.0f, "float out of range");
        }

        // RandomNumber throws for an unsupported numeric type
        public static void RandomNumberThrowsForUnsupportedType() {

            LimitedNumber<bool> limited = new LimitedNumber<bool>("bool", false, true);

            Check.Throws<NotSupportedException>(() => limited.RandomNumber());
        }

    }
}
