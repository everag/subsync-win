using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync
{
    enum SyncStatus
    {
        NOT_RUNNING,
        RUNNING
    }

    enum NotificationPeriod
    {
        NORMAL = 5 * 1000,
        LONG = 15 * 1000
    }
}
