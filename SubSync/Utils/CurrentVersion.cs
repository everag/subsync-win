using SubSync.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    _releaseInfo = new ReleaseInfo(
                        Properties.Resources.AppName,
                        Properties.Resources.AppVersion,
                        Properties.Resources.AppCurrentStage,
                        Properties.Resources.AppBuildDate
                    );
                }

                return _releaseInfo;
            }
        }
    }
}
