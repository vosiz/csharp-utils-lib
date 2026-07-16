using Vosiz.Extends;

namespace Vosiz.Utils.Generator
{
    public class Lipsum : IStringGenerator
    {
        private static readonly string[] Words =
        {
            "lorem",
            "ipsum",
            "dolor",
            "sit",
            "amet"
        };

        private const string Chars = "abcdefghijklmnopqrstuvwxyz";

        public string Result { get; private set; }

        // Constructor
        public Lipsum() { }

        // Returns the generated result
        public override string ToString()
        {
            return Result;
        }

        // Generates word_count words, either space separated or as a single run of random characters
        public IStringGenerator Generate(int word_count, bool no_spaces)
        {

            Result = string.Empty;

            for (int i = 0; i < word_count; i++)
            {
                if (!no_spaces)
                    Result += Words.RandomValue() + " ";
                else
                    Result += Chars.RandomSubstring(-1, 1);
            }

            return this;
        }

        // Trims the result down to the given maximum length
        public Lipsum MaxLength(int count)
        {

            Result = Result.Limit(count);
            return this;
        }
    }
}
