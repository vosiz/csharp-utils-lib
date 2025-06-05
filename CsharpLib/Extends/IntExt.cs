using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vosiz.Extends
{
    public static class IntExt
    {

        public static object[] Split(this int value, int parts, bool signed = false)
        {
            if (parts == 2)
            {
                if (signed)
                {
                    return new object[]
                    {
                    (short)(value & 0xFFFF),
                    (short)((value >> 16) & 0xFFFF)
                    };
                }
                else
                {
                    return new object[]
                    {
                    (ushort)(value & 0xFFFF),
                    (ushort)((value >> 16) & 0xFFFF)
                    };
                }

            }
            else if (parts == 4)
            {
                if (signed)
                {
                    return new object[]
                    {
                        (sbyte)(value & 0xFF),
                        (sbyte)((value >> 8) & 0xFF),
                        (sbyte)((value >> 16) & 0xFF),
                        (sbyte)((value >> 24) & 0xFF)
                    };
                }
                else
                {
                    return new object[]
                    {
                        (byte)(value & 0xFF),
                        (byte)((value >> 8) & 0xFF),
                        (byte)((value >> 16) & 0xFF),
                        (byte)((value >> 24) & 0xFF)
                    };
                }
            }
            else
            {
                throw new ArgumentException("Only 2 or 4 splits are supported.");
            }
        }
    }
}
