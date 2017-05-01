using SubSync.GUI.Localization;
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
        private static readonly Uri HomePageUri = new Uri("https://github.com/everton-agner/subsync-win");
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

                var bodyFmt = L10n.Get("ContactUsEmailBody");

                var body = string.Format(
                    bodyFmt,
                    appInfo,
                    WindowsUtils.GetOSFriendlyName(),
                    Environment.Is64BitOperatingSystem ? "64 bits" : "32 bits",
                    Environment.Version.ToString()
                );

                var mailtoFmt = "mailto://{0}?subject={1}&body={2}";
                var email = "ton.agner+subsync@gmail.com";

                return new Uri(string.Format(mailtoFmt, email, L10n.Get("Contact"), HttpUtility.UrlPathEncode(body)));
            }
        }

        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Text = L10n.Get("$this.Text", this, CurrentVersion.ReleaseInfo.ApplicationName);

            Icon = Properties.Resources.SubSync_Logo;

            LbVersion.Text = CurrentVersion.ReleaseInfo.ToString(false, true, true, true);

            if (CurrentVersion.ReleaseInfo.Build != null)
                LbBuild.Text = string.Format("Build {0}", CurrentVersion.ReleaseInfo.Build);

            var dev = "Everton Agner Ramos";

            LnkDevelopedBy.Text = L10n.Get("DevelopedBy", dev);
            LnkDevelopedBy.Links.Clear();

            var devBegin = LnkDevelopedBy.Text.IndexOf(dev);
            var devEnd = LnkDevelopedBy.Text.Count() - devBegin;

            LnkDevelopedBy.Links.Add(devBegin, devEnd);

            var design = "Diogo Cezar";

            LnkLogoBy.Text = L10n.Get("DesignBy", design);
            LnkLogoBy.Links.Clear();

            var designBegin = LnkLogoBy.Text.IndexOf(design);
            var designEnd = LnkLogoBy.Text.Count() - designBegin;

            LnkLogoBy.Links.Add(designBegin, designEnd);

            var contactUs = L10n.Get("ContactUs");

            LnkContactUs.Text = L10n.Get("ReportBugs", contactUs);
            LnkContactUs.Links.Clear();

            var contactUsBegin = LnkContactUs.Text.IndexOf(contactUs);
            var contactUsEnd = LnkContactUs.Text.Count() - contactUsBegin;

            LnkContactUs.Links.Add(contactUsBegin, contactUsEnd);
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
