using System;
using System.Globalization;
using Vosiz.Helpers;

namespace Tests.Helpers
{

    public static class TimeHelperTests
    {

        // Now formats the current time-of-day using the given format
        public static void NowFormatsTimeOfDay() {

            string result = TimeHelper.Now(@"hh\:mm\:ss");

            Check.True(TimeSpan.TryParseExact(result, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out _), "Now() result should parse back as a TimeSpan");
        }

        // Today formats the current date and time using the given format
        public static void TodayFormatsCurrentDateTime() {

            string result = TimeHelper.Today("yyyy-MM-dd");

            Check.True(DateTime.TryParseExact(result, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _), "Today() result should parse back as a DateTime");
        }

        // Timestamp formats the current date and time using the given format
        public static void TimestampFormatsCurrentDateTime() {

            string result = TimeHelper.Timestamp("yyyy-MM-dd");

            Check.True(DateTime.TryParseExact(result, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _), "Timestamp() result should parse back as a DateTime");
        }

        // EpochTime returns a value close to the current Unix time
        public static void EpochTimeIsCloseToNow() {

            long expected = DateTimeOffset.Now.ToUnixTimeSeconds();
            long actual = TimeHelper.EpochTime();

            Check.True(Math.Abs(expected - actual) <= 5, "EpochTime should be within a few seconds of now");
        }

        // ToEpochTime converts a known UTC DateTime to its Unix timestamp
        public static void ToEpochTimeConvertsUtcDateTime() {

            DateTime utc = new DateTime(1970, 1, 1, 0, 0, 10, DateTimeKind.Utc);

            Check.Equal("10", TimeHelper.ToEpochTime(utc));
        }

        // ToTimestamp formats a known DateTime using the given format
        public static void ToTimestampFormatsGivenDateTime() {

            DateTime dt = new DateTime(2024, 1, 2, 3, 4, 5);

            Check.Equal("2024-01-02", TimeHelper.ToTimestamp(dt, "yyyy-MM-dd"));
        }

    }
}
