using System;
using System.Text;
using Vosiz.Commons;

namespace Vosiz.Assembly
{

    public enum VersionCompatibility
    {
        NotCompatible               = -1,
        CompatibleWithReservation   = 0,
        Compatible                  = 1,
    }

    public class VersionParseException : Exception
    {

        public VersionParseException() :
            base("Failed to parse version.")
        { }

        public VersionParseException(string value, string format, Exception inner_exc) :
            base(string.Format("Failed to parse version \"{0}\" using format \"{1}\": {2}", value, format, inner_exc.Message), inner_exc)
        { }

    }

    public class Version
    {

        private const string DEFAULT_FORMAT = "{0}.{1}";


        public int Major { private set; get; }
        public int Minor { private set; get; }
        public int Patch { private set; get; }
        public int Revision { private set; get; }
        public string Format { private set; get; }


        // Parses a version string against a positional format ("{0}.{1}.{2}.{3}", tokens strictly
        // sequential from {0}, no reordering/skipping). Missing trailing components stay -1.
        public static Version Create(string value, string format = DEFAULT_FORMAT)
        {

            Assert.OnNull(value);
            Assert.OnNull(format);

            try
            {

                string[] separators = ParseFormat(format);
                int[] components = SplitValue(value, separators);

                return new Version(components[0], components[1], components[2], components[3], format);
            }
            catch (FormattedException exc)
            {

                throw new VersionParseException(value, format, exc);
            }
        }

        // Extracts the literal separators between the {0}..{3} tokens of a format string
        private static string[] ParseFormat(string format)
        {

            const string token0 = "{0}";

            if (!format.StartsWith(token0))
                throw new FormattedException("Format must start with {0}, found \"{1}\"", token0, format);

            string[] separators = new string[3];
            int separator_count = 0;
            int pos = token0.Length;
            int expected_index = 1;

            while (expected_index <= 3)
            {

                string token = string.Format("{{{0}}}", expected_index);
                int token_pos = format.IndexOf(token, pos);

                if (token_pos == -1)
                    break;

                string separator = format.Substring(pos, token_pos - pos);

                if (string.IsNullOrEmpty(separator))
                    throw new FormattedException("Format separator before {0} cannot be empty, found \"{1}\"", token, format);

                separators[separator_count++] = separator;
                pos = token_pos + token.Length;
                expected_index++;
            }

            if (pos != format.Length)
                throw new FormattedException("Format has unexpected trailing content: \"{0}\"", format);

            string[] result = new string[separator_count];
            Array.Copy(separators, result, separator_count);

            return result;
        }

        // Splits a value string using the separators extracted from the format into up to 4 int components
        private static int[] SplitValue(string value, string[] separators)
        {

            int[] components = new int[] { -1, -1, -1, -1 };
            int pos = 0;

            for (int i = 0; i < separators.Length; i++)
            {

                string separator = separators[i];
                int separator_pos = value.IndexOf(separator, pos);

                if (separator_pos == -1)
                    throw new FormattedException("Value \"{0}\" does not match format (missing separator \"{1}\")", value, separator);

                components[i] = ParseComponent(value.Substring(pos, separator_pos - pos));
                pos = separator_pos + separator.Length;
            }

            components[separators.Length] = ParseComponent(value.Substring(pos));

            return components;
        }

        // Parses a single version component, rejecting anything that would collide with the unset (-1) sentinel
        private static int ParseComponent(string str)
        {

            int result;

            if (!int.TryParse(str, out result))
                throw new FormattedException("Version component \"{0}\" is not a valid integer", str);

            if (result < 0)
                throw new FormattedException("Version component \"{0}\" cannot be negative", str);

            return result;
        }


        // Constructor with all components and the format that produced them
        protected Version(int major, int minor, int patch, int revision, string format)
        {

            Major = major;
            Minor = minor;
            Patch = patch;
            Revision = revision;
            Format = format;
        }

        // Formats the version back to a string via Format, omitting trailing unset (-1) components
        public override string ToString()
        {

            string[] separators = ParseFormat(Format);
            int[] components = new int[] { Major, Minor, Patch, Revision };

            int last_set = -1;
            for (int i = 0; i <= separators.Length; i++)
            {

                if (components[i] != -1)
                    last_set = i;
            }

            if (last_set == -1)
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append(components[0]);

            for (int i = 1; i <= last_set; i++)
            {

                sb.Append(separators[i - 1]);
                sb.Append(components[i]);
            }

            return sb.ToString();
        }

        // Compares this version (the supported/reference one) against a queried version for protocol compatibility
        public VersionCompatibility Compare(Version other)
        {

            Assert.OnNull(other);

            if (Major != other.Major)
                return VersionCompatibility.NotCompatible;

            if (Minor < other.Minor)
                return VersionCompatibility.CompatibleWithReservation;

            if (Minor > other.Minor)
                return VersionCompatibility.NotCompatible;

            return VersionCompatibility.Compatible;
        }

    }
}
