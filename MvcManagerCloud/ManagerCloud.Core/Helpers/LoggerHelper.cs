using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ManagerCloud.Core.Helpers
{
    public static class LoggerHelper
    {
        public static void AddInfoLog(EventLog eventLog, string log)
        {
            try
            {
                if (!EventLog.SourceExists("ManagerCloud"))
                {
                    EventLog.CreateEventSource("ManagerCloud", "ManagerCloud");
                }

                eventLog.Source = "ManagerCloud";
                eventLog.WriteEntry(string.Join(" - ", log, DateTime.Now), EventLogEntryType.Information);
            }
            catch
            {
                // ignored
            }
        }

        public static void AddErrorLog(EventLog eventLog, string log)
        {
            try
            {
                if (!EventLog.SourceExists("ManagerCloud"))
                {
                    EventLog.CreateEventSource("ManagerCloud", "ManagerCloud");
                }

                eventLog.Source = "ManagerCloud";
                eventLog.WriteEntry(string.Join(" - ", log, DateTime.Now), EventLogEntryType.Error);
            }
            catch
            {
                // ignored
            }
        }

        public static IReadOnlyCollection<string> ReadEventLogEntries(EventLog eventLog)
        {
            var listEntries = new List<string>();
            eventLog.Log = "ManagerCloud";
            foreach (EventLogEntry entry in eventLog.Entries)
            {
                listEntries.Add(entry.Message);
            }
            return listEntries;
        }
    }
}

