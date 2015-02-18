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
using System.Windows.Forms;

namespace SubSync.GUI
{
    public partial class AboutForm : Form
    {
        private static readonly Uri HomePageUri = new Uri("https://subsync.codeplex.com");
        private static readonly Uri FacebookPageUri = new Uri("https://fb.com/subsyncapp");
        private static readonly Uri DeveloperContactUri = new Uri("mailto://ton.agner+subsync@gmail.com");
        private static readonly Uri LogoDesignContactUri = new Uri("https://diogocezar.com");
        
        private static readonly Uri ContactUsUri = new Uri(string.Format(
            "mailto://{0}?subject=Contact&body={1}",
            "ton.agner+subsync@gmail.com",
            string.Format(
                "\n\nPlease keep the following information:\n\nSubSync Version: {0}\nOS Version: {1} {2}\n.NET runtime: {3}",
                string.Format(
                    "{0} {1} {2} - Build {3} - {4} bits",
                    Properties.Resources.AppName,
                    Properties.Resources.AppVersion,
                    Properties.Resources.AppCurrentStage,
                    Properties.Resources.AppBuildDate,
                    Environment.Is64BitProcess ? "64" : "32"
                ),
                WindowsUtils.GetOSFriendlyName(),
                Environment.Is64BitOperatingSystem ? "64 bits" : "32 bits",
                Environment.Version.ToString()
            )
        ).Replace(" ", "%20").Replace("\n", "%0D%0A"));

        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.SubSync_Logo;

            if (Properties.Resources.AppCurrentStage == "Stable")
                LbVersion.Text = string.Format("{0} {1}",
                    Properties.Resources.AppName,
                    Properties.Resources.AppVersion
                );
            else
                LbVersion.Text = string.Format("{0} {1} {2}",
                    Properties.Resources.AppName,
                    Properties.Resources.AppVersion,
                    Properties.Resources.AppCurrentStage
                );

            LbBuild.Text = string.Format("Build {0}", Properties.Resources.AppBuildDate);

            LnkDevelopedBy.Text = "Developed with lots of love by Everton Agner Ramos";
            LnkDevelopedBy.Links.Clear();
            LnkDevelopedBy.Links.Add(31, 21);

            LnkLogoBy.Text = "Logo design by Diogo Cezar";
            LnkLogoBy.Links.Clear();
            LnkLogoBy.Links.Add(15, 11);

            LnkContactUs.Text = "Got any bug to report? Contact us!";
            LnkContactUs.Links.Clear();
            LnkContactUs.Links.Add(23, 10);
        }

        private void OpenUri(Uri websiteUri)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo(websiteUri.ToString());
            Process.Start(sInfo);
        }

        private void ImgLogo_Click(object sender, EventArgs e)
        {
            OpenUri(HomePageUri);
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
    }
}
