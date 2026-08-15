using System;
using System.IO;
using Severity = Vosiz.Enums.Severity;

namespace Vosiz.Logger
{

    public class LogConfig
    {

        public const string DEFAULT_TIMESTAMP_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";
        public const string DEFAULT_SEPARATOR        = " | ";


        public Severity MinLevel { set; get; }
        public bool WriteToFile { set; get; }
        public bool WriteToConsole { set; get; }
        public string Directory { set; get; }
        public string FileName { set; get; }
        public string TimestampFormat { set; get; }
        public string Separator { set; get; }

        // Combines Directory and FileName on every access, so changing either is reflected immediately
        public string FilePath
        {
            get { return Path.Combine(Directory, FileName); }
        }


        // Constructor with named init parameters, all optional
        public LogConfig(Severity min_level = Severity.Debug, string directory = "./logs", string file_name = null, string timestamp_format = DEFAULT_TIMESTAMP_FORMAT, string separator = DEFAULT_SEPARATOR)
        {

            MinLevel = min_level;
            Directory = directory;
            FileName = file_name ?? string.Format("log-{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));
            TimestampFormat = timestamp_format;
            Separator = separator;
        }

    }
}
