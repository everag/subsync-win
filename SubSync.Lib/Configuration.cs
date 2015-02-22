using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public static class Configuration
    {
        public static int ScheduledFoldersCheckingDelay
        {
            get
            {
                return GetIntValue("Tasks.CheckForVideosJob.Delay", 5);
            }
        }

        public static int ScheduledUpdatesCheckingDelay
        {
            get
            {
                return GetIntValue("Tasks.CheckForUpdatesJob.Delay", 6);
            }
        }

        public static string RunningEnvironment
        {
            get 
            {
                return ConfigurationManager.AppSettings["App.Environment"];
            }
        }

        public static string SubDbApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SubDb.Api.Url"];
            }
        }

        private static int GetIntValue(string key, int defaultValue = 1)
        {
            int value;

            if (Int32.TryParse(ConfigurationManager.AppSettings[key], out value))
                return value;
            else
                return defaultValue;
        }
    }
}
