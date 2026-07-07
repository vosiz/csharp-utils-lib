using System;
using Vosiz.Extends;

namespace Tests.Extends
{

    public static class IntExtTests
    {

        // Split into 2 unsigned parts extracts the low and high 16 bits
        public static void SplitInto2UnsignedParts() {

            int value = 0x12345678;
            object[] parts = value.Split(2);

            Check.Equal((ushort)0x5678, (ushort)parts[0]);
            Check.Equal((ushort)0x1234, (ushort)parts[1]);
        }

        // Split into 2 signed parts wraps a high bit into a negative short
        public static void SplitInto2SignedParts() {

            int value = 0x00008000;
            object[] parts = value.Split(2, signed: true);

            Check.Equal((short)-32768, (short)parts[0]);
            Check.Equal((short)0, (short)parts[1]);
        }

        // Split into 4 unsigned parts extracts each byte
        public static void SplitInto4UnsignedParts() {

            int value = 0x12345678;
            object[] parts = value.Split(4);

            Check.Equal((byte)0x78, (byte)parts[0]);
            Check.Equal((byte)0x56, (byte)parts[1]);
            Check.Equal((byte)0x34, (byte)parts[2]);
            Check.Equal((byte)0x12, (byte)parts[3]);
        }

        // Split into 4 signed parts wraps a high bit into a negative sbyte
        public static void SplitInto4SignedParts() {

            int value = unchecked((int)0x80010203);
            object[] parts = value.Split(4, signed: true);

            Check.Equal((sbyte)3, (sbyte)parts[0]);
            Check.Equal((sbyte)2, (sbyte)parts[1]);
            Check.Equal((sbyte)1, (sbyte)parts[2]);
            Check.Equal((sbyte)-128, (sbyte)parts[3]);
        }

        // Split throws for an unsupported part count
        public static void SplitThrowsForUnsupportedPartCount() {

            int value = 42;

            Check.Throws<ArgumentException>(() => value.Split(3));
        }

    }
}
