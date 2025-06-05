using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Helpers
{
    public static class TimeHelper
    {
        public static string Now(string fmt)
        {

            return DateTime.Now.TimeOfDay.ToString(fmt);
        }

        public static string Today(string fmt)
        {

            return DateTime.Now.ToString(fmt);
        }

        public static string Timestamp(string fmt)
        {

            return DateTime.Now.ToString(fmt);
        }

        public static long EpochTime()
        {

            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        public static string ToEpochTime(DateTime dt)
        {

            DateTimeOffset dto = dt.Kind == DateTimeKind.Utc
                ? new DateTimeOffset(dt)
                : new DateTimeOffset(dt.ToUniversalTime());

            return dto.ToUnixTimeSeconds().ToString();
        }

        public static string ToTimestamp(DateTime dt, string time_fmt)
        {

            return dt.ToString(time_fmt);
        }
    }
}
