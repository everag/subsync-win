using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Tasks
{
    [Quartz.DisallowConcurrentExecutionAttribute()]
    public class CheckForVideosJob : IJob
    {
        public static readonly string KEY_DIR_PATH = "directoryPath";

        private SyncManager SyncManager = SyncManager.Instance;

        public void Execute(IJobExecutionContext context)
        {
            var me = context.JobDetail.Key;
            var dataMap = context.JobDetail.JobDataMap;

            var directoryPath = dataMap.GetString(KEY_DIR_PATH);
            var directory = new DirectoryInfo(directoryPath);

            foreach (var filePath in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                SyncManager.CheckFile(new FileInfo(filePath));
            }
        }
    }
}
