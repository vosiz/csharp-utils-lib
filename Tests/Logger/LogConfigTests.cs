using System;
using System.IO;
using Vosiz.Logger;
using Severity = Vosiz.Enums.Severity;

namespace Tests.Logger
{

    public static class LogConfigTests
    {

        // Constructor without arguments applies the documented defaults
        public static void ConstructorAppliesDefaults() {

            var config = new LogConfig();

            Check.Equal(Severity.Debug, config.MinLevel);
            Check.False(config.WriteToFile, "WriteToFile should default to false until explicitly enabled");
            Check.False(config.WriteToConsole, "WriteToConsole should default to false until explicitly enabled");
            Check.Equal("./logs", config.Directory);
            Check.Equal(LogConfig.DEFAULT_TIMESTAMP_FORMAT, config.TimestampFormat);
            Check.Equal(LogConfig.DEFAULT_SEPARATOR, config.Separator);
        }

        // Constructor stores every given value as-is
        public static void ConstructorStoresGivenValues() {

            var config =
                new LogConfig(
                    min_level: Severity.Error,
                    directory: "custom",
                    file_name: "custom.log",
                    timestamp_format: "yyyy-MM-dd",
                    separator: " :: ");

            Check.Equal(Severity.Error, config.MinLevel);
            Check.Equal("custom", config.Directory);
            Check.Equal("custom.log", config.FileName);
            Check.Equal("yyyy-MM-dd", config.TimestampFormat);
            Check.Equal(" :: ", config.Separator);
        }

        // WriteToFile and WriteToConsole can be toggled after construction
        public static void WriteTogglesCanBeSetAfterConstruction() {

            var config = new LogConfig();
            config.WriteToFile = true;
            config.WriteToConsole = true;

            Check.True(config.WriteToFile, "WriteToFile should be settable");
            Check.True(config.WriteToConsole, "WriteToConsole should be settable");
        }

        // FileName defaults to a date-stamped log file name when not given
        public static void FileNameDefaultsToDateStampedPattern() {

            var config = new LogConfig();
            string expected = string.Format("log-{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));

            Check.Equal(expected, config.FileName);
        }

        // FilePath combines Directory and FileName
        public static void FilePathCombinesDirectoryAndFileName() {

            var config = new LogConfig(directory: "some/dir", file_name: "x.log");

            Check.Equal(Path.Combine("some/dir", "x.log"), config.FilePath);
        }

    }
}
