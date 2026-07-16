namespace Vosiz.Utils.Generator
{
    public interface IStringGenerator
    {
        IStringGenerator Generate(int word_count, bool no_spaces);
    }
}
