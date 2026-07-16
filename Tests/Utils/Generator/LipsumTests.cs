using System;
using Vosiz.Utils.Generator;

namespace Tests.Utils.Generator
{

    public static class LipsumTests
    {

        // Generate with spaces produces a space separated run of words
        public static void GenerateWithSpacesProducesWords() {

            Lipsum lipsum = new Lipsum();
            lipsum.Generate(5, no_spaces: false);

            Check.True(lipsum.ToString().Contains(" "), "Result should contain spaces between words");
        }

        // Generate without spaces produces a single run of characters
        public static void GenerateWithoutSpacesProducesCharacterRun() {

            Lipsum lipsum = new Lipsum();
            lipsum.Generate(5, no_spaces: true);

            Check.False(lipsum.ToString().Contains(" "), "Result should not contain spaces");
        }

        // MaxLength trims the result down to the given length
        public static void MaxLengthTrimsResult() {

            Lipsum lipsum = new Lipsum();
            lipsum.Generate(20, no_spaces: false);
            lipsum.MaxLength(5);

            Check.True(lipsum.ToString().Length <= 5, "Result should be trimmed to the requested length");
        }

        // MaxLength returns the same instance for chaining
        public static void MaxLengthReturnsSameInstanceForChaining() {

            Lipsum lipsum = new Lipsum();
            lipsum.Generate(3, no_spaces: false);

            Lipsum chained = lipsum.MaxLength(10);

            Check.True(ReferenceEquals(lipsum, chained), "MaxLength should return the same instance");
        }

    }
}
