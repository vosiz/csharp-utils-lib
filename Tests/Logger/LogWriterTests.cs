using System;
using System.IO;
using Vosiz.Logger;
using Severity = Vosiz.Enums.Severity;

namespace Tests.Logger
{

    public static class LogWriterTests
    {

        // Write below MinLevel does not create the log file
        public static void WriteBelowMinLevelIsDropped() {

            string dir = TempDir();
            var config = new LogConfig(min_level: Severity.Warning, directory: dir);
            config.WriteToFile = true;
            var writer = new LogWriter(config);

            writer.Write(Severity.Info, "should be dropped");

            Check.False(File.Exists(config.FilePath), "File should not exist for a dropped entry");

            CleanupDir(dir);
        }

        // Write at or above MinLevel appends a line to the configured file
        public static void WriteAtOrAboveMinLevelAppendsLine() {

            string dir = TempDir();
            var config = new LogConfig(min_level: Severity.Info, directory: dir);
            config.WriteToFile = true;
            var writer = new LogWriter(config);

            writer.Write(Severity.Warning, "hello");

            string[] lines = File.ReadAllLines(config.FilePath);
            Check.Equal(1, lines.Length);
            Check.True(lines[0].Contains("hello"), "Line should contain the message");

            CleanupDir(dir);
        }

        // Write creates the target directory if it does not exist yet
        public static void WriteCreatesMissingDirectory() {

            string dir = TempDir();
            Check.False(Directory.Exists(dir), "Directory should not exist before writing");

            var config = new LogConfig(directory: dir);
            config.WriteToFile = true;
            var writer = new LogWriter(config);

            writer.Write(Severity.Info, "hello");

            Check.True(Directory.Exists(dir), "Directory should have been created");

            CleanupDir(dir);
        }

        // Write does nothing at all when neither WriteToFile nor WriteToConsole is enabled
        public static void WriteDoesNothingWhenBothTargetsDisabled() {

            string dir = TempDir();
            var config = new LogConfig(directory: dir);
            var writer = new LogWriter(config);

            writer.Write(Severity.Fatal, "nobody hears this");

            Check.False(Directory.Exists(dir), "Directory should not be created when WriteToFile is disabled");
        }

        // Write sends the formatted line to the console when WriteToConsole is enabled
        public static void WriteToConsoleWhenEnabled() {

            string dir = TempDir();
            var config = new LogConfig(min_level: Severity.Debug, directory: dir);
            config.WriteToConsole = true;
            var writer = new LogWriter(config);

            var original_out = Console.Out;
            var captured = new StringWriter();

            try {

                Console.SetOut(captured);
                writer.Write(Severity.Info, "to console");

            } finally {

                Console.SetOut(original_out);
            }

            Check.True(captured.ToString().Contains("to console"), "Console output should contain the message");
        }

        // Write does not touch the console when WriteToConsole is disabled
        public static void DoesNotWriteToConsoleWhenDisabled() {

            string dir = TempDir();
            var config = new LogConfig(min_level: Severity.Debug, directory: dir);
            var writer = new LogWriter(config);

            var original_out = Console.Out;
            var captured = new StringWriter();

            try {

                Console.SetOut(captured);
                writer.Write(Severity.Info, "should not appear");

            } finally {

                Console.SetOut(original_out);
            }

            Check.Equal("", captured.ToString());
        }

        // Creates a unique, not-yet-existing temp directory path for a single test
        private static string TempDir() {

            return Path.Combine(Path.GetTempPath(), "vosiz-logger-tests-" + Guid.NewGuid().ToString("N"));
        }

        // Removes a temp directory if it was created during the test
        private static void CleanupDir(string dir) {

            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
        }

    }
}
