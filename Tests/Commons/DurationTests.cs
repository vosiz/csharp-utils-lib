using System;
using Vosiz.Commons;

namespace Tests.Commons
{

    public static class DurationTests
    {

        // Default constructor sets Value to zero
        public static void DefaultConstructorSetsZero() {

            Duration duration = new Duration();

            Check.Equal(TimeSpan.Zero, duration.Value);
        }

        // Constructor with a TimeSpan sets Value directly
        public static void ConstructorWithTimeSpanSetsValue() {

            TimeSpan value = TimeSpan.FromMinutes(5);
            Duration duration = new Duration(value);

            Check.Equal(value, duration.Value);
        }

        // Create from a double second count converts to the matching TimeSpan
        public static void CreateFromDoubleConvertsToTimeSpan() {

            Duration duration = Duration.Create(90.0);

            Check.Equal(TimeSpan.FromSeconds(90.0), duration.Value);
        }

        // Create from an int second count converts to the matching TimeSpan
        public static void CreateFromIntConvertsToTimeSpan() {

            Duration duration = Duration.Create(90);

            Check.Equal(TimeSpan.FromSeconds(90), duration.Value);
        }

        // Create from a long second count converts to the matching TimeSpan
        public static void CreateFromLongConvertsToTimeSpan() {

            Duration duration = Duration.Create(90L);

            Check.Equal(TimeSpan.FromSeconds(90), duration.Value);
        }

        // Create from a TimeSpan keeps the value as-is
        public static void CreateFromTimeSpanKeepsValue() {

            TimeSpan value = TimeSpan.FromHours(2);
            Duration duration = Duration.Create(value);

            Check.Equal(value, duration.Value);
        }

        // AddSeconds mutates the instance in place and accumulates across calls
        public static void AddSecondsAccumulatesInPlace() {

            Duration duration = Duration.Create(0);

            duration.AddSeconds(1);
            duration.AddSeconds(1);

            Check.Equal(TimeSpan.FromSeconds(2), duration.Value);
        }

        // ToBreakdownString omits leading zero-value units
        public static void ToBreakdownStringOmitsLeadingZeroUnits() {

            Duration duration = Duration.Create(32);

            Check.Equal("32s", duration.ToBreakdownString());
        }

        // ToBreakdownString includes every nonzero unit down to seconds
        public static void ToBreakdownStringIncludesAllNonzeroComponents() {

            Duration duration = Duration.Create(3661);

            Check.Equal("1h 1m 1s", duration.ToBreakdownString());
        }

        // ToBreakdownString omits a zero-value trailing component
        public static void ToBreakdownStringOmitsTrailingZeroComponent() {

            Duration duration = Duration.Create(3660);

            Check.Equal("1h 1m", duration.ToBreakdownString());
        }

        // ToBreakdownString on a zero duration falls back to "0s"
        public static void ToBreakdownStringAllZeroReturnsZeroSeconds() {

            Duration duration = Duration.Create(0);

            Check.Equal("0s", duration.ToBreakdownString());
        }

        // ToBreakdownString breaks days down into weeks and remaining days
        public static void ToBreakdownStringIncludesWeeks() {

            Duration duration = Duration.Create(2931152);

            Check.Equal("4w 5d 22h 12m 32s", duration.ToBreakdownString());
        }

        // ToBreakdownString floors a fractional leftover second instead of rounding
        public static void ToBreakdownStringFloorsFractionalSeconds() {

            Duration duration = Duration.Create(61.9);

            Check.Equal("1m 1s", duration.ToBreakdownString());
        }

    }
}
