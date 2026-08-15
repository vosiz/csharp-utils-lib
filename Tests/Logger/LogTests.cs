using System;
using System.IO;
using Vosiz.Logger;
using Severity = Vosiz.Enums.Severity;

namespace Tests.Logger
{

    public static class LogTests
    {

        // Configure replaces Config with the given instance
        public static void ConfigureReplacesConfig() {

            string dir = TempDir();
            var config = new LogConfig(directory: dir);

            Log.Configure(config);

            Check.True(ReferenceEquals(config, Log.Config), "Log.Config should be the configured instance");
        }

        // A severity shortcut (Error) forwards to Write at the matching severity
        public static void ShortcutForwardsToWriteAtMatchingSeverity() {

            string dir = TempDir();
            var config = new LogConfig(min_level: Severity.Debug, directory: dir);
            config.WriteToFile = true;
            Log.Configure(config);

            Log.Error("boom");

            string[] lines = File.ReadAllLines(Log.Config.FilePath);
            Check.True(lines[0].Contains("Error"), "Line should contain the Error level");
            Check.True(lines[0].Contains("boom"), "Line should contain the message");

            CleanupDir(dir);
        }

        // Reader reflects entries written through the currently configured writer
        public static void ReaderReadsBackWrittenEntries() {

            string dir = TempDir();
            var config = new LogConfig(min_level: Severity.Debug, directory: dir);
            config.WriteToFile = true;
            Log.Configure(config);

            Log.Info("hello");

            var entries = Log.Reader.Read();

            Check.Equal(1, entries.Count);
            Check.Equal("hello", entries[0].Message);

            CleanupDir(dir);
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
