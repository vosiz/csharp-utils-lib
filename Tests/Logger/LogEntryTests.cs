using System;
using Vosiz.Logger;
using Severity = Vosiz.Enums.Severity;

namespace Tests.Logger
{

    public static class LogEntryTests
    {

        // Constructor with an explicit timestamp stores every field as-is
        public static void ConstructorWithTimestampStoresAllFields() {

            var timestamp = new DateTime(2026, 8, 15, 12, 0, 0);
            var entry = new LogEntry(timestamp, Severity.Warning, "hello");

            Check.Equal(timestamp, entry.Timestamp);
            Check.Equal(Severity.Warning, entry.Level);
            Check.Equal("hello", entry.Message);
        }

        // Constructor without a timestamp stamps the current time
        public static void ConstructorWithoutTimestampStampsNow() {

            var before = DateTime.Now;
            var entry = new LogEntry(Severity.Info, "hello");
            var after = DateTime.Now;

            Check.True(entry.Timestamp >= before && entry.Timestamp <= after, "Timestamp should be stamped to now");
        }

        // ToString produces the fixed "timestamp | level | message" layout
        public static void ToStringProducesPipeDelimitedLine() {

            var timestamp = new DateTime(2026, 8, 15, 12, 0, 0, 500);
            var entry = new LogEntry(timestamp, Severity.Error, "boom");

            Check.Equal("2026-08-15 12:00:00.500 | ERR | boom", entry.ToString());
        }

        // TryParse round-trips a line produced by ToString back into an equal entry
        public static void TryParseRoundTripsToString() {

            var original = new LogEntry(new DateTime(2026, 8, 15, 12, 0, 0, 500), Severity.Error, "boom");

            LogEntry parsed;
            bool ok = LogEntry.TryParse(original.ToString(), out parsed);

            Check.True(ok, "TryParse should succeed for a well-formed line");
            Check.Equal(original.Timestamp, parsed.Timestamp);
            Check.Equal(original.Level, parsed.Level);
            Check.Equal(original.Message, parsed.Message);
        }

        // TryParse fails for an empty line
        public static void TryParseFailsForEmptyLine() {

            LogEntry parsed;
            bool ok = LogEntry.TryParse("", out parsed);

            Check.False(ok, "TryParse should fail for an empty line");
        }

        // TryParse fails when the line does not have exactly 3 pipe-separated parts
        public static void TryParseFailsForMalformedLine() {

            LogEntry parsed;
            bool ok = LogEntry.TryParse("not a valid line", out parsed);

            Check.False(ok, "TryParse should fail for a malformed line");
        }

        // TryParse fails for an unrecognized severity level
        public static void TryParseFailsForInvalidLevel() {

            LogEntry parsed;
            bool ok = LogEntry.TryParse("2026-08-15 12:00:00.500 | NotALevel | boom", out parsed);

            Check.False(ok, "TryParse should fail for an invalid level");
        }

        // TryParse rejects the old full enum name
        public static void TryParseFailsForFullEnumName() {

            LogEntry parsed;
            bool ok = LogEntry.TryParse("2026-08-15 12:00:00.500 | Error | boom", out parsed);

            Check.False(ok, "TryParse should fail for the full enum name");
        }

        // TryParse round-trips every Severity value
        public static void TryParseRoundTripsEverySeverityValue() {

            foreach (Severity level in Enum.GetValues(typeof(Severity)))
            {

                var original = new LogEntry(new DateTime(2026, 8, 15, 12, 0, 0, 500), level, "boom");

                LogEntry parsed;
                bool ok = LogEntry.TryParse(original.ToString(), out parsed);

                Check.True(ok, "TryParse should succeed for level " + level);
                Check.Equal(level, parsed.Level);
            }
        }

        // TryParse fails for an unparsable timestamp
        public static void TryParseFailsForInvalidTimestamp() {

            LogEntry parsed;
            bool ok = LogEntry.TryParse("not-a-date | Error | boom", out parsed);

            Check.False(ok, "TryParse should fail for an invalid timestamp");
        }

        // Format(LogConfig) uses the config's TimestampFormat and Separator instead of the defaults
        public static void FormatWithConfigUsesConfigTimestampFormatAndSeparator() {

            var timestamp = new DateTime(2026, 8, 15, 12, 0, 0);
            var entry = new LogEntry(timestamp, Severity.Error, "boom");
            var config = new LogConfig(timestamp_format: "yyyy-MM-dd", separator: " :: ");

            Check.Equal("2026-08-15 :: ERR :: boom", entry.Format(config));
        }

        // TryParse(line, LogConfig, out) round-trips a line produced by Format(config) using that same config
        public static void TryParseWithConfigRoundTripsFormatWithConfig() {

            var original = new LogEntry(new DateTime(2026, 8, 15, 12, 0, 0), Severity.Error, "boom");
            var config = new LogConfig(timestamp_format: "yyyy-MM-dd HH:mm:ss", separator: " :: ");

            LogEntry parsed;
            bool ok = LogEntry.TryParse(original.Format(config), config, out parsed);

            Check.True(ok, "TryParse should succeed for a line matching the config's format");
            Check.Equal(original.Timestamp, parsed.Timestamp);
            Check.Equal(original.Level, parsed.Level);
            Check.Equal(original.Message, parsed.Message);
        }

    }
}
