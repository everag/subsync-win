using Quartz;
using SubSync.Lib;
using SubSync.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync.Tasks
{
    [Quartz.DisallowConcurrentExecutionAttribute()]
    public class CheckForUpdatesJob : IJob
    {
        public static readonly string LATEST_VERSION_URL = "https://subsync.codeplex.com/releases";

        public static readonly Regex EXTRACT_TITLE = new Regex(@"<head>.*<title>(.*)</title>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
        public static readonly Regex EXTRACT_VERSION = new Regex(@".*Download(?:\&#58;)?\s(SubSync(?: Alpha| Beta)? [0-9.]+)");

        public void Execute(IJobExecutionContext context)
        {
            var latestVersionAvailable = GetLatestVersionAvailable();

            if (latestVersionAvailable == null)
                return;

            if (latestVersionAvailable.IsNewerThan(CurrentVersion.ReleaseInfo))
            {
                var res = MessageBox.Show(
                    string.Format("{0} available for download!\n\nPress OK to visit the website and download the new version", latestVersionAvailable.ToString(true, true, false, true)),
                    "New version available!",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Exclamation
                );

                if (res == DialogResult.OK)
                {
                    ProcessStartInfo sInfo = new ProcessStartInfo("https://subsync.codeplex.com/releases");
                    Process.Start(sInfo);
                }
            }
        }

        public ReleaseInfo GetLatestVersionAvailable()
        {
            string versionString = null;
            string htmlCode = null;

            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(LATEST_VERSION_URL);
            }

            if (EXTRACT_TITLE.IsMatch(htmlCode))
            {
                var title = EXTRACT_TITLE.Match(htmlCode).Groups[1].Value;

                if (EXTRACT_VERSION.IsMatch(title))
                {
                    versionString = EXTRACT_VERSION.Match(title).Groups[1].Value;
                }
            }

            return new ReleaseInfo(versionString);
        }
    }
}
