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

        // Attempts to parse the string as an enum value of the given type, without throwing
        public static bool TryParseEnum(this string value, Type enum_type, bool ignore_case, out object result)
        {

            if (!enum_type.IsEnum)
                throw new ArgumentException("Type must be enum", nameof(enum_type));

            try
            {
                result = Enum.Parse(enum_type, value, ignore_case);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        // Converts to PascalCase, e.g. "some text" -> "SomeText"
        public static string ToPascalCase(this string str)
        {
            if (str == null) return null;

            List<string> parts = SplitToParts(str);
            StringBuilder result = new StringBuilder();

            foreach (string part in parts)
                result.Append(Capitalize(part));

            return result.ToString();
        }

        // Converts to camelCase, e.g. "some text" -> "someText"
        public static string ToCamelCase(this string str)
        {
            if (str == null) return null;

            List<string> parts = SplitToParts(str);

            if (parts.Count == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            result.Append(parts[0]);

            for (int i = 1; i < parts.Count; i++)
                result.Append(Capitalize(parts[i]));

            return result.ToString();
        }

        // Converts to snake_case, e.g. "some text" -> "some_text"
        public static string ToSnakeCase(this string str)
        {
            if (str == null) return null;

            return string.Join("_", SplitToParts(str));
        }

        // Converts to dashed/kebab-case, e.g. "some text" -> "some-text"
        public static string ToDashed(this string str)
        {
            if (str == null) return null;

            return string.Join("-", SplitToParts(str));
        }

        // Converts to SCREAMING_SNAKE_CASE, e.g. "some text" -> "SOME_TEXT"
        public static string ToScreamingSnakeCase(this string str)
        {
            if (str == null) return null;

            return string.Join("_", SplitToParts(str)).ToUpperInvariant();
        }

        // Converts to MODULE_Case, e.g. "some text of mine" -> "SOME_TextOfMine"
        public static string ToModuleCase(this string str)
        {
            if (str == null) return null;

            List<string> parts = SplitToParts(str);

            if (parts.Count == 0)
                return string.Empty;

            StringBuilder result = new StringBuilder();
            result.Append(parts[0].ToUpperInvariant());

            if (parts.Count > 1)
            {
                result.Append("_");

                for (int i = 1; i < parts.Count; i++)
                    result.Append(Capitalize(parts[i]));
            }

            return result.ToString();
        }

        // Converts to CAPS LOCK style, e.g. "some text" -> "SOME TEXT"
        public static string ToCapsLock(this string str)
        {
            if (str == null) return null;

            return string.Join(" ", SplitToParts(str)).ToUpperInvariant();
        }

        // Alias for ToUpper, kept for naming consistency with the other To*Case methods
        public static string ToCapital(this string str)
        {
            if (str == null) return null;

            return str.ToUpperInvariant();
        }

        // Splits a string into lowercase word parts, treating any non-alphanumeric character and
        // case transitions (camelCase/PascalCase boundaries, including acronym runs) as word breaks
        private static List<string> SplitToParts(string str)
        {
            List<string> parts = new List<string>();

            if (string.IsNullOrEmpty(str))
                return parts;

            StringBuilder current = new StringBuilder();
            char prev_char = '\0';

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];

                if (!char.IsLetterOrDigit(c))
                {
                    FlushPart(parts, current);
                    prev_char = '\0';
                    continue;
                }

                bool prev_is_lowerish = char.IsLower(prev_char) || char.IsDigit(prev_char);
                bool prev_is_acronym_upper = char.IsUpper(prev_char) && i + 1 < str.Length && char.IsLower(str[i + 1]);

                bool starts_new_word = current.Length > 0 && char.IsUpper(c) && (prev_is_lowerish || prev_is_acronym_upper);

                if (starts_new_word)
                    FlushPart(parts, current);

                current.Append(char.ToLowerInvariant(c));
                prev_char = c;
            }

            FlushPart(parts, current);

            return parts;
        }

        // Appends the current buffer as a word part if non-empty, then clears it
        private static void FlushPart(List<string> parts, StringBuilder current)
        {
            if (current.Length > 0)
                parts.Add(current.ToString());

            current.Clear();
        }

        // Capitalizes the first letter of a word part, leaving the rest untouched
        private static string Capitalize(string part)
        {
            if (string.IsNullOrEmpty(part))
                return part;

            return char.ToUpperInvariant(part[0]) + part.Substring(1);
        }
    }
}
