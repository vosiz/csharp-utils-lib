using System;
using System.Collections.Generic;
using System.IO;
using Vosiz.Logger;
using Severity = Vosiz.Enums.Severity;

namespace Tests.Logger
{

    public static class LogReaderTests
    {

        // ReadAll returns an empty list when the directory does not exist
        public static void ReadAllReturnsEmptyListForMissingDirectory() {

            var reader = ReaderFor(TempDir());

            List<LogEntry> entries = reader.ReadAll();

            Check.Equal(0, entries.Count);
        }

        // ReadAll collects entries from every *.log file in the directory
        public static void ReadAllParsesEntriesAcrossMultipleFiles() {

            string dir = TempDir();
            WriteLogFile(dir, "a.log", new LogEntry(new DateTime(2026, 1, 1), Severity.Info, "first").ToString());
            WriteLogFile(dir, "b.log", new LogEntry(new DateTime(2026, 1, 2), Severity.Warning, "second").ToString());

            var reader = ReaderFor(dir);
            List<LogEntry> entries = reader.ReadAll();

            Check.Equal(2, entries.Count);

            CleanupDir(dir);
        }

        // ReadAll ignores files that are not *.log
        public static void ReadAllIgnoresNonLogFiles() {

            string dir = TempDir();
            WriteLogFile(dir, "a.log", new LogEntry(Severity.Info, "included").ToString());
            WriteLogFile(dir, "notes.txt", new LogEntry(Severity.Info, "excluded").ToString());

            var reader = ReaderFor(dir);
            List<LogEntry> entries = reader.ReadAll();

            Check.Equal(1, entries.Count);
            Check.Equal("included", entries[0].Message);

            CleanupDir(dir);
        }

        // ReadAll skips lines that don't match the expected format
        public static void ReadAllSkipsUnparsableLines() {

            string dir = TempDir();
            Directory.CreateDirectory(dir);
            File.WriteAllLines(Path.Combine(dir, "a.log"), new string[] {
                "not a log line",
                new LogEntry(Severity.Info, "valid").ToString()
            });

            var reader = ReaderFor(dir);
            List<LogEntry> entries = reader.ReadAll();

            Check.Equal(1, entries.Count);
            Check.Equal("valid", entries[0].Message);

            CleanupDir(dir);
        }

        // Filter keeps only entries at or above the given minimum severity
        public static void FilterByMinLevel() {

            var entries = new List<LogEntry>();
            entries.Add(new LogEntry(Severity.Debug, "low"));
            entries.Add(new LogEntry(Severity.Error, "high"));

            List<LogEntry> filtered = LogReader.Filter(entries, min_level: Severity.Warning);

            Check.Equal(1, filtered.Count);
            Check.Equal("high", filtered[0].Message);
        }

        // Filter keeps only entries within the given inclusive timestamp range
        public static void FilterByDateRange() {

            var entries = new List<LogEntry>();
            entries.Add(new LogEntry(new DateTime(2026, 1, 1), Severity.Info, "too early"));
            entries.Add(new LogEntry(new DateTime(2026, 1, 15), Severity.Info, "in range"));
            entries.Add(new LogEntry(new DateTime(2026, 2, 1), Severity.Info, "too late"));

            List<LogEntry> filtered = LogReader.Filter(entries, from: new DateTime(2026, 1, 10), to: new DateTime(2026, 1, 20));

            Check.Equal(1, filtered.Count);
            Check.Equal("in range", filtered[0].Message);
        }

        // Read combines ReadAll and Filter in a single call
        public static void ReadAppliesFilterToFilesOnDisk() {

            string dir = TempDir();
            WriteLogFile(dir, "a.log", new LogEntry(Severity.Debug, "low").ToString());
            WriteLogFile(dir, "b.log", new LogEntry(Severity.Error, "high").ToString());

            var reader = ReaderFor(dir);
            List<LogEntry> entries = reader.Read(min_level: Severity.Warning);

            Check.Equal(1, entries.Count);
            Check.Equal("high", entries[0].Message);

            CleanupDir(dir);
        }

        // Creates a unique, not-yet-existing temp directory path for a single test
        private static string TempDir() {

            return Path.Combine(Path.GetTempPath(), "vosiz-logger-tests-" + Guid.NewGuid().ToString("N"));
        }

        // Builds a LogReader configured to read from the given directory
        private static LogReader ReaderFor(string dir) {

            return new LogReader(new LogConfig(directory: dir));
        }

        // Writes a single-line *.log fixture file, creating the directory if needed
        private static void WriteLogFile(string dir, string file_name, string content) {

            Directory.CreateDirectory(dir);
            File.WriteAllText(Path.Combine(dir, file_name), content);
        }

        // Removes a temp directory if it was created during the test
        private static void CleanupDir(string dir) {

            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
        }

    }
}
