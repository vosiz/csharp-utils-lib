using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vosiz.Utils;

namespace Vosiz.Extends
{
    public static class StringExt
    {
        public static byte[] ToByteArray(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string Limit(this string str, int maxLength)
        {
            if (str == null) return null;
            return str.Length <= maxLength ? str : str.Substring(0, maxLength);
        }

        public static string RandomSubstring(this string str, int index = -1, int length = -1)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            int str_len = str.Length;
            if (index == -1)
                index = Randomizer.Next(str_len);

            if (index >= str_len)
                return string.Empty;

            if (length == -1)
                length = Randomizer.Next(str_len - index + 1);

            if (index + length > str_len)
                length = str_len - index;

            return str.Substring(index, length);
        }
    }
}
