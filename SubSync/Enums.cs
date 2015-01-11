using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync
{
    public enum SyncStatus
    {
        NOT_RUNNING,
        RUNNING
    }

    public enum NotificationPeriod
    {
        NORMAL = 5 * 1000,
        LONG = 15 * 1000
    }
}
