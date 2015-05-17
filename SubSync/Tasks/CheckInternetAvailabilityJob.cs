using Quartz;
using SubSync.GUI;
using SubSync.GUI.Localization;
using SubSync.Lib;
using SubSync.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync.Tasks
{
    [Quartz.DisallowConcurrentExecutionAttribute()]
    public class CheckInternetAvailabilityJob : IJob
    {
        private static readonly SyncManager SyncManager = SyncManager.Instance;
        private static readonly SetupForm SetupForm = WindowManager.GetOpenedForm(typeof(SetupForm)) as SetupForm;

        public void Execute(IJobExecutionContext context)
        {
            if (SyncManager.Status == SyncStatus.RUNNING && !NetworkUtils.IsInternetAvailable())
            {
                SyncManager.AdviseInternetConnectivityLost();
            }
        }
    }
}
