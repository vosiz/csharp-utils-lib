using System;
using System.Globalization;
using Vosiz.Commons;
using Severity = Vosiz.Enums.Severity;

namespace Vosiz.Logger
{

    public class LogEntry
    {

        public DateTime Timestamp { private set; get; }
        public Severity Level { private set; get; }
        public string Message { private set; get; }


        // Parses a line formatted with the default timestamp format and separator
        public static bool TryParse(string line, out LogEntry entry)
        {

            return TryParse(line, LogConfig.DEFAULT_TIMESTAMP_FORMAT, LogConfig.DEFAULT_SEPARATOR, out entry);
        }

        // Parses a line formatted with the given config's TimestampFormat/Separator
        public static bool TryParse(string line, LogConfig config, out LogEntry entry)
        {

            Assert.OnNull(config);

            return TryParse(line, config.TimestampFormat, config.Separator, out entry);
        }

        // Splits a line into timestamp/level/message using the given timestamp format and separator
        private static bool TryParse(string line, string timestamp_format, string separator, out LogEntry entry)
        {

            entry = null;

            if (string.IsNullOrEmpty(line))
                return false;

            string[] parts = line.Split(new string[] { separator }, 3, StringSplitOptions.None);

            if (parts.Length != 3)
                return false;

            DateTime timestamp;
            if (!DateTime.TryParseExact(parts[0], timestamp_format, CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp))
                return false;

            Severity level;
            if (!Enum.TryParse(parts[1], out level))
                return false;

            entry = new LogEntry(timestamp, level, parts[2]);
            return true;
        }


        // Constructor with an explicit timestamp, used when reconstructing entries from disk
        public LogEntry(DateTime timestamp, Severity level, string message)
        {

            Assert.OnNull(message);

            Timestamp = timestamp;
            Level = level;
            Message = message;
        }

        // Constructor stamping the current time, used when writing a fresh entry
        public LogEntry(Severity level, string message) :
            this(DateTime.Now, level, message)
        { }

        // Formats the entry as "timestamp | level | message" using the default timestamp format and separator
        public override string ToString()
        {

            return Format(LogConfig.DEFAULT_TIMESTAMP_FORMAT, LogConfig.DEFAULT_SEPARATOR);
        }

        // Formats the entry using the given config's TimestampFormat/Separator
        public string Format(LogConfig config)
        {

            Assert.OnNull(config);

            return Format(config.TimestampFormat, config.Separator);
        }

        // Builds the formatted line from the given timestamp format and separator
        private string Format(string timestamp_format, string separator)
        {

            return string.Format("{0}{3}{1}{3}{2}", Timestamp.ToString(timestamp_format), Level, Message, separator);
        }

    }
}
