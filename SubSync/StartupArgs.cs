using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync
{
    public static class StartupArgs
    {
        public static void SetArgs(string[] commandLineArgs)
        {
            foreach (var arg in commandLineArgs)
            {
                switch (arg.ToLower())
                {
                    case "/startsync":
                        InitializeInStartState = true;
                        break;
                }
            }
        }

        public static bool InitializeInStartState = false;
    }
}
