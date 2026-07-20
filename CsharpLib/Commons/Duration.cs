using System;
using System.Text;

namespace Vosiz.Commons
{

    public class Duration
    {

        public const long WEEK2DAYS = 7;

        public TimeSpan Value { private set; get; }


        // Creates a Duration from a total second count (double)
        public static Duration Create(double total_seconds) {

            return new Duration(TimeSpan.FromSeconds(total_seconds));
        }

        // Creates a Duration from a total second count (int)
        public static Duration Create(int total_seconds) {

            return Create((double)total_seconds);
        }

        // Creates a Duration from a total second count (long)
        public static Duration Create(long total_seconds) {

            return Create((double)total_seconds);
        }

        // Creates a Duration from a TimeSpan
        public static Duration Create(TimeSpan value) {

            return new Duration(value);
        }


        // Constructor
        public Duration() {

            Value = TimeSpan.Zero;
        }

        // Constructor with initial value
        public Duration(TimeSpan value) {

            Value = value;
        }

        // Adds seconds to the running total
        public void AddSeconds(double seconds) {

            Value = Value.Add(TimeSpan.FromSeconds(seconds));
        }

        // Formats the current value as a breakdown string (weeks/days/hours/minutes/seconds), omitting zero components
        public string ToBreakdownString() {

            long weeks = Value.Days / WEEK2DAYS;
            long days  = Value.Days % WEEK2DAYS;

            StringBuilder builder = new StringBuilder();

            if (weeks > 0)
                builder.Append(weeks).Append("w ");

            if (days > 0)
                builder.Append(days).Append("d ");

            if (Value.Hours > 0)
                builder.Append(Value.Hours).Append("h ");

            if (Value.Minutes > 0)
                builder.Append(Value.Minutes).Append("m ");

            if (Value.Seconds > 0 || builder.Length == 0)
                builder.Append(Value.Seconds).Append("s ");

            return builder.ToString().TrimEnd();
        }

    }
}
