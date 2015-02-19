using SubSync.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace SubSync.GUI
{
    public partial class AboutForm : Form
    {
        private static readonly Uri HomePageUri = new Uri("https://subsync.codeplex.com");
        private static readonly Uri FacebookPageUri = new Uri("https://fb.com/subsyncapp");
        private static readonly Uri DeveloperContactUri = new Uri("mailto://ton.agner+subsync@gmail.com");
        private static readonly Uri LogoDesignContactUri = new Uri("http://www.diogocezar.com");
        
        private static Uri ContactUsUri
        {
            get
            {
                var appInfo = string.Format(
                    "{0} - Build {1} - {2} bits",
                    CurrentVersion.ReleaseInfo.ToString(false, true, true, true),
                    CurrentVersion.ReleaseInfo.Build ?? "???",
                    Environment.Is64BitProcess ? "64" : "32"
                );

                var bodyFmt = "\r\n\r\n(Please keep the following information)\r\n\r\nApp Version: {0}\r\nOS Version: {1} {2}\r\n.NET runtime: {3}";

                var body = string.Format(
                    bodyFmt,
                    appInfo,
                    WindowsUtils.GetOSFriendlyName(),
                    Environment.Is64BitOperatingSystem ? "64 bits" : "32 bits",
                    Environment.Version.ToString()
                );

                var mailtoFmt = "mailto://{0}?subject=Contact&body={1}";
                var email = "ton.agner+subsync@gmail.com";

                return new Uri(string.Format(mailtoFmt, email, HttpUtility.UrlPathEncode(body)));
            }
        }

        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.SubSync_Logo;

            LbVersion.Text = CurrentVersion.ReleaseInfo.ToString(false, true, true, true);

            if (CurrentVersion.ReleaseInfo.Build != null)
                LbBuild.Text = string.Format("Build {0}", CurrentVersion.ReleaseInfo.Build);

            LnkDevelopedBy.Text = "Developed with lots of love by Everton Agner Ramos";
            LnkDevelopedBy.Links.Clear();
            LnkDevelopedBy.Links.Add(31, 21);

            LnkLogoBy.Text = "Logo design by Diogo Cezar";
            LnkLogoBy.Links.Clear();
            LnkLogoBy.Links.Add(15, 11);

            LnkContactUs.Text = "Got any bug to report or some gold bars to donate? Contact us!";
            LnkContactUs.Links.Clear();
            LnkContactUs.Links.Add(51, 10);
        }

        private void OpenUri(Uri uri)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(uri.ToString());
            Process.Start(sInfo);
        }

        private void LnkVisitHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUri(HomePageUri);
        }

        private void LnkGoToFacebook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUri(FacebookPageUri);
        }

        private void LnkDevelopedBy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUri(DeveloperContactUri);
        }

        private void LnkLogoBy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUri(LogoDesignContactUri);
        }

        private void LnkContactUs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUri(ContactUsUri);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
