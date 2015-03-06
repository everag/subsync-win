using SubSync.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Utils
{
    public static class CurrentVersion
    {
        private static ReleaseInfo _releaseInfo;

        public static ReleaseInfo ReleaseInfo
        {
            get
            {
                if (_releaseInfo == null)
                {
                    _releaseInfo = new ReleaseInfo(Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
                }

                return _releaseInfo;
            }
        }
    }
}
