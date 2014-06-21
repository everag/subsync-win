using SubSync.Lib;
using SubSync.SubDb.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            videoFileDialog.InitialDirectory = @"C:\";
            videoFileDialog.Title = @"Choose your video file";
            videoFileDialog.CheckFileExists = true;
            videoFileDialog.CheckPathExists = true;
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            if (videoFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbxFile.Text = videoFileDialog.FileName.ToString();
                btnShowLanguages.Enabled = true;
            }
        }

        private void btnShowLanguages_Click(object sender, EventArgs e)
        {
            ISubtitleProvider subProvider = new SubDbSubtitleProvider();

            tbxLog.Text = string.Format("Contacting {0}\r\n", subProvider.ApiUrl);

            var availableLanguages = subProvider.GetAvailableLanguages(File.OpenRead(videoFileDialog.FileName));

            if (availableLanguages.Any())
            {
                tbxLog.Text += string.Join("\r\n", availableLanguages.Select(lan => lan.DisplayName));

                using (Stream subtitleStream = subProvider.GetSubtitle(File.OpenRead(videoFileDialog.FileName), availableLanguages[0]))
                using (Stream outputStream = File.Create(@"E:\foi.srt"))
                {
                    subtitleStream.CopyTo(outputStream, 8 * 1024);
                    tbxLog.Text += "Subtitle downloaded!";
                }
            }
            else
            {
                tbxLog.Text += string.Format("No subtitles found for {0}", videoFileDialog.FileName);
            }
        }
    }
}
