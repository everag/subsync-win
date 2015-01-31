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
                int value;

                if (Int32.TryParse(ConfigurationManager.AppSettings["Tasks.CheckForVideosJob.Delay"], out value))
                    return value;
                else
                    // TODO: Add Log message
                    return 1;
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
    }
}
