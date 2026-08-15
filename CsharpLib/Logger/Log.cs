using Vosiz.Commons;
using Severity = Vosiz.Enums.Severity;

namespace Vosiz.Logger
{

    public static class Log
    {

        public static LogConfig Config { private set; get; } = new LogConfig();
        public static LogReader Reader { private set; get; } = new LogReader(Config);

        private static LogWriter Writer = new LogWriter(Config);


        // Swaps in a new config, rebuilding the writer/reader that use it
        public static void Configure(LogConfig config)
        {

            Assert.OnNull(config);

            Config = config;
            Writer = new LogWriter(Config);
            Reader = new LogReader(Config);
        }

        // Writes a message at the given severity - see LogWriter.Write
        public static void Write(Severity level, string message) { Writer.Write(level, message); }

        // Shortcut for Write(Severity.Debug, message)
        public static void Debug(string message) { Write(Severity.Debug, message); }

        // Shortcut for Write(Severity.Notice, message)
        public static void Notice(string message) { Write(Severity.Notice, message); }

        // Shortcut for Write(Severity.Info, message)
        public static void Info(string message) { Write(Severity.Info, message); }

        // Shortcut for Write(Severity.Warning, message)
        public static void Warning(string message) { Write(Severity.Warning, message); }

        // Shortcut for Write(Severity.Error, message)
        public static void Error(string message) { Write(Severity.Error, message); }

        // Shortcut for Write(Severity.Fatal, message)
        public static void Fatal(string message) { Write(Severity.Fatal, message); }

    }
}
