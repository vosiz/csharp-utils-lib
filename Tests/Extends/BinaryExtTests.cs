using System;
using System.Text;
using Vosiz.Extends;

namespace Tests.Extends
{

    public static class BinaryExtTests
    {

        // FromByteArray decodes UTF-8 bytes back to a string
        public static void FromByteArrayDecodesUtf8() {

            byte[] bytes = Encoding.UTF8.GetBytes("abc");

            Check.Equal("abc", bytes.FromByteArray());
        }

        // ToHexaTable returns the placeholder for an empty array
        public static void ToHexaTableReturnsPlaceholderForEmptyArray() {

            Check.Equal("<empty>", new byte[0].ToHexaTable());
        }

        // ToHexaTable formats each byte as an uppercase hex pair
        public static void ToHexaTableFormatsBytesAsHex() {

            byte[] bytes = new byte[] { 0x01, 0xFF };

            Check.Equal("0x01 0xFF ", bytes.ToHexaTable());
        }

        // ToHexaTable breaks into a new line after per_row values
        public static void ToHexaTableBreaksLineAfterPerRow() {

            byte[] bytes = new byte[] { 0x01, 0x02, 0x03 };
            string expected = "0x01 0x02 " + Environment.NewLine + "0x03 ";

            Check.Equal(expected, bytes.ToHexaTable(2));
        }

        // ToHexaTable throws for a non-positive per_row value
        public static void ToHexaTableThrowsForNonPositivePerRow() {

            byte[] bytes = new byte[] { 0x01 };

            Check.Throws<ArgumentException>(() => bytes.ToHexaTable(0));
        }

    }
}
