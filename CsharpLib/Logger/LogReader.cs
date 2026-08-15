using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vosiz.Commons;
using Severity = Vosiz.Enums.Severity;

namespace Vosiz.Logger
{

    public class LogReader
    {

        public LogConfig Config { private set; get; }


        // Filters entries by minimum severity and/or an inclusive timestamp range
        public static List<LogEntry> Filter(IEnumerable<LogEntry> entries, Severity? min_level = null, DateTime? from = null, DateTime? to = null)
        {

            Assert.OnNull(entries);

            return entries
                .Where(entry => min_level == null || entry.Level >= min_level)
                .Where(entry => from == null || entry.Timestamp >= from)
                .Where(entry => to == null || entry.Timestamp <= to)
                .ToList();
        }


        // Constructor with the config providing the folder (and format) to read *.log files with
        public LogReader(LogConfig config)
        {

            Assert.OnNull(config);

            Config = config;
        }

        // Reads every entry from every *.log file found in Config.Directory, skipping unparsable lines
        public List<LogEntry> ReadAll()
        {

            var entries = new List<LogEntry>();

            if (!Directory.Exists(Config.Directory))
                return entries;

            string[] files = Directory.GetFiles(Config.Directory, "*.log");

            foreach (var file in files)
            {

                foreach (var line in File.ReadAllLines(file))
                {

                    LogEntry entry;
                    if (LogEntry.TryParse(line, Config, out entry))
                        entries.Add(entry);
                }
            }

            return entries;
        }

        // Reads every entry in Config.Directory and applies Filter in one call
        public List<LogEntry> Read(Severity? min_level = null, DateTime? from = null, DateTime? to = null)
        {

            return Filter(ReadAll(), min_level, from, to);
        }

    }
}
