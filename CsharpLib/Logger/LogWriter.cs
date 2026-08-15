using System;
using System.IO;
using Vosiz.Commons;
using Severity = Vosiz.Enums.Severity;

namespace Vosiz.Logger
{

    public class LogWriter
    {

        public LogConfig Config { private set; get; }


        // Constructor with the config driving where/whether/how entries are written
        public LogWriter(LogConfig config)
        {

            Assert.OnNull(config);

            Config = config;
        }

        // Writes a message at the given severity, honoring MinLevel/WriteToFile/WriteToConsole
        public void Write(Severity level, string message)
        {

            Assert.OnNull(message);

            // below the configured threshold - drop it
            if (level < Config.MinLevel)
                return;

            string line = new LogEntry(level, message).Format(Config);

            if (Config.WriteToFile)
                WriteToFile(line);

            if (Config.WriteToConsole)
                Console.WriteLine(line);
        }

        // Appends one formatted line to the configured log file, creating the directory if needed
        private void WriteToFile(string line)
        {

            Directory.CreateDirectory(Config.Directory);
            File.AppendAllText(Config.FilePath, line + Environment.NewLine);
        }

    }
}
