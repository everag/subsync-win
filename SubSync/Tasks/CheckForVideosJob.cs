using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Tasks
{
    class CheckForVideosJob : IJob
    {
        public static readonly string KEY_DIR_PATH = "directoryPath";

        private SyncManager SyncManager = SyncManager.Instance;

        public void Execute(IJobExecutionContext context)
        {
            var me = context.JobDetail.Key;
            var dataMap = context.JobDetail.JobDataMap;

            var directoryPath = dataMap.GetString(KEY_DIR_PATH);
            var directory = new DirectoryInfo(directoryPath);

            // TODO: Move to a Log message
            // Console.WriteLine(string.Format("[{0}] Execution: {1}", me, directory.FullName));

            foreach (var filePath in Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                SyncManager.CheckFile(new FileInfo(filePath));
            }
        }
    }
}
