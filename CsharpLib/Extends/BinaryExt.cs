using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Extends
{
    public static class BinaryExt
    {
        public static string FromByteArray(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string ToHexaTable(this byte[] bytes, int per_row = 16)
        {
            if (bytes == null || bytes.Length == 0)
                return "<empty>";

            if (per_row < 1)
                throw new ArgumentException("Count of values per row has to be greater than 0");

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                // new line
                if (i % per_row == 0 && i > 0)
                {
                    sb.AppendLine();
                }

                sb.AppendFormat("0x{0:X2} ", bytes[i]);
            }

            return sb.ToString();
        }

    }
}
