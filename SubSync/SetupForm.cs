using SubSync.Events;
using SubSync.Lib;
using SubSync.SubDb.Client;
using SubSync.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync
{
    public partial class SetupForm : Form
    {
        private string AppNameVersion { get { return string.Format("{0} {1}", Properties.Resources.AppName, Properties.Resources.AppVersion); } }

        public SetupForm()
        {
            InitializeComponent();
        }

        private SyncManager SyncManager = SubSync.SyncManager.Instance;

        private void SetupForm_Load(object sender, EventArgs e)
        {
            NotifyIcon.Icon = Properties.Resources.SubSync_Logo;
            NotifyIcon.Text = AppNameVersion;

            Icon = Properties.Resources.SubSync_Logo;

            SetupSyncManager();

            LoadSupportedLanguages();

            SetupDefaultFolders();
            SetupDefaultLanguages();

            CheckNotStableRelease();
        }

        private void SetupSyncManager()
        {
            SyncManager.Started += (sender, e) => ShowTrayNotification("SubSync is running!", NotificationPeriod.NORMAL);
            SyncManager.Stopped += (sender, e) => ShowTrayNotification("SubSync stopped!", NotificationPeriod.NORMAL);
            
            SyncManager.SubtitleDownloaded += (sender, e) => 
            {
                var subtitleInfo = (e as SubtitleDownloadedEventArgs).SubtitleFile;

                ShowTrayNotification(String.Format("Subtitle downloaded for {0}", subtitleInfo.VideoFile.Name), NotificationPeriod.NORMAL);
            };
        }
        
        #region Media folders

        private ISet<DirectoryInfo> mediaFolders = new HashSet<DirectoryInfo>();

        private void BtnAddDirectory_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFolderPath = FolderBrowserDialog.SelectedPath;
                AddFolder(selectedFolderPath);
            }
        }

        private void BtnRemoveDirectory_Click(object sender, EventArgs e)
        {
            string[] foldersToRemove = new string[LstDirectories.SelectedItems.Count];
            LstDirectories.SelectedItems.CopyTo(foldersToRemove, 0);

            foreach (string folderPath in foldersToRemove)
            {
                RemoveFolder(folderPath);
            }
        }

        private void LstDirectories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BtnRemoveDirectory_Click(sender, e as EventArgs);
            }
        }

        private void LstDirectories_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnRemoveDirectory.Enabled = LstDirectories.SelectedIndices.Count > 0;
        }

        private void AddFolder(string folderPath)
        {
            DirectoryInfo folderToAdd = new DirectoryInfo(folderPath);

            if (mediaFolders.Any(mf => mf.IsSame(folderToAdd)))
            {
                MessageBox.Show("This folder was already selected");
                return;
            }

            mediaFolders.Add(folderToAdd);
            LstDirectories.Items.Add(folderPath);

            CheckStartButton();
        }

        private void RemoveFolder(string folderPath)
        {
            var folderToRemove = new DirectoryInfo(folderPath);

            int removed = (mediaFolders as HashSet<DirectoryInfo>).RemoveWhere(mf => mf.IsSame(folderToRemove));
            
            if (removed > 0)
                LstDirectories.Items.Remove(folderPath);

            CheckStartButton();
        }

        private void SetupDefaultFolders()
        {
            var defaultVideoDirectories = new HashSet<DirectoryInfo>(MediaLibraries.VideosDirectories);
            var myVideosDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));

            if (!defaultVideoDirectories.Any(vd => vd.IsSame(myVideosDir)))
                defaultVideoDirectories.Add(myVideosDir);

            foreach (var videoDirectory in defaultVideoDirectories)
            {
                AddFolder(videoDirectory.FullName);
            }
        }

        #endregion

        #region Languages

        private IDictionary<string, CultureInfo> langDescriptionsToLanguages = new Dictionary<string, CultureInfo>();
        private IList<CultureInfo> languagePreferences = new List<CultureInfo>();

        private void LoadSupportedLanguages()
        {
            foreach (var lang in SyncManager.SupportedLanguages)
            {
                var listItem = lang.ToSubSyncDescription();

                langDescriptionsToLanguages[listItem] = lang;
                LstLanguages.Items.Add(listItem);
            }
        }

        private void LstLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnAddLanguage.Enabled = LstLanguages.SelectedIndices.Count > 0;
        }

        private void BtnLanguageAdd_Click(object sender, EventArgs e)
        {
            string[] languagesToAdd = new string[LstLanguages.SelectedItems.Count];
            LstLanguages.SelectedItems.CopyTo(languagesToAdd, 0);

            foreach (string lang in languagesToAdd)
            {
                AddLanguage(lang);
            }
        }

        private void LstLanguages_DoubleClick(object sender, EventArgs e)
        {
            BtnLanguageAdd_Click(sender, e);
        }

        private void AddLanguage(string langDescription)
        {
            AddLanguage(langDescriptionsToLanguages[langDescription]);
        }

        private void AddLanguage(CultureInfo language)
        {
            var description = language.ToSubSyncDescription();

            if (langDescriptionsToLanguages.ContainsKey(description))
            {
                languagePreferences.Add(language);

                LstLanguagePreferences.Items.Add(description);

                LstLanguages.Items.Remove(description);

                CheckStartButton();
            }

            if (language.Parent != CultureInfo.InvariantCulture)
                AddLanguage(language.Parent);
        }

        private void BtnLanguageRemove_Click(object sender, EventArgs e)
        {
            string languageToRemove = LstLanguagePreferences.SelectedItem as string;
            RemoveLanguage(languageToRemove);
        }

        private void LstLanguagePreferences_DoubleClick(object sender, EventArgs e)
        {
            BtnLanguageRemove_Click(sender, e);
        }

        private void RemoveLanguage(string langDescription)
        {
            CultureInfo language = langDescriptionsToLanguages[langDescription];

            languagePreferences.Remove(language);

            LstLanguages.Items.Add(langDescription);

            LstLanguagePreferences.Items.Remove(langDescription);

            CheckStartButton();
        }

        private void LstLanguagePreferences_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstLanguagePreferences.SelectedIndex != -1)
            {
                BtnRemoveLanguage.Enabled = true;
                BtnLanguageUp.Enabled = LstLanguagePreferences.SelectedIndex > 0;
                BtnLanguageDown.Enabled = LstLanguagePreferences.SelectedIndex < LstLanguagePreferences.Items.Count - 1;
            }
            else
            {
                BtnRemoveLanguage.Enabled = false;
                BtnLanguageUp.Enabled = false;
                BtnLanguageDown.Enabled = false;
            }
        }

        private void BtnLanguageUp_Click(object sender, EventArgs e)
        {
            var languageToMove = LstLanguagePreferences.SelectedItem as string;
            var currentIdx = LstLanguagePreferences.SelectedIndex;

            LstLanguagePreferences.Items.Insert(currentIdx - 1, languageToMove);
            LstLanguagePreferences.SelectedIndex = currentIdx - 1;
            LstLanguagePreferences.Items.RemoveAt(currentIdx + 1);

            languagePreferences.Insert(currentIdx - 1, langDescriptionsToLanguages[languageToMove]);
            languagePreferences.RemoveAt(currentIdx + 1);
        }

        private void BtnLanguageDown_Click(object sender, EventArgs e)
        {
            var languageToMove = LstLanguagePreferences.SelectedItem as string;
            var currentIdx = LstLanguagePreferences.SelectedIndex;

            LstLanguagePreferences.Items.Insert(currentIdx + 2, languageToMove);
            LstLanguagePreferences.SelectedIndex = currentIdx + 2;
            LstLanguagePreferences.Items.RemoveAt(currentIdx);

            languagePreferences.Insert(currentIdx + 2, langDescriptionsToLanguages[languageToMove]);
            languagePreferences.RemoveAt(currentIdx);
        }

        private void SetupDefaultLanguages()
        {
            var systemCulture = CultureInfo.InstalledUICulture;

            AddLanguage(systemCulture);
        }

        #endregion

        #region Start/Stop

        private void CheckStartButton()
        {
            bool enableStart = languagePreferences.Any() && mediaFolders.Any();

            BtnStartStop.Enabled = enableStart;
        }

        private void ToggleControlsEnabled(bool enable)
        {
            LstLanguages.Enabled = enable;
            LstLanguagePreferences.Enabled = enable;
            LstDirectories.Enabled = enable;

            BtnAddDirectory.Enabled = enable;
            BtnRemoveDirectory.Enabled = enable;

            BtnAddLanguage.Enabled = enable;
            BtnRemoveLanguage.Enabled = enable;
            BtnLanguageUp.Enabled = enable;
            BtnLanguageDown.Enabled = enable;
        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            LstDirectories.ClearSelected();
            LstLanguagePreferences.ClearSelected();
            LstLanguages.ClearSelected();

            if (SyncManager.Status == SyncStatus.NOT_RUNNING)
            {
                BkgWorkerStartStopSync.RunWorkerAsync(SyncAction.START);
            }
            else if (SyncManager.Status == SyncStatus.RUNNING)
            {
                BkgWorkerStartStopSync.RunWorkerAsync(SyncAction.STOP);
            }
        }

        private void BkgWorkerStartStopSync_DoWork(object sender, DoWorkEventArgs e)
        {
            var action = (SyncAction) e.Argument;

            if (action == SyncAction.START)
            {
                BkgWorkerStartStopSync.ReportProgress(0, SyncStartStopProgress.STARTING);

                SyncManager.Start(languagePreferences, mediaFolders);

                BkgWorkerStartStopSync.ReportProgress(100, SyncStartStopProgress.STARTED);
            }
            else if (action == SyncAction.STOP)
            {
                BkgWorkerStartStopSync.ReportProgress(0, SyncStartStopProgress.STOPPING);

                SyncManager.Stop();

                BkgWorkerStartStopSync.ReportProgress(100, SyncStartStopProgress.STOPPED);
            }
        }

        private void BkgWorkerStartStopSync_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var progress = (SyncStartStopProgress)e.UserState;

            switch (progress)
            {
                case SyncStartStopProgress.STARTING:
                    ToggleControlsEnabled(false);
                    BtnStartStop.Enabled = false;
                    BtnStartStop.Text = "Starting...";
                    break;

                case SyncStartStopProgress.STOPPING:
                    ToggleControlsEnabled(false);
                    BtnStartStop.Enabled = false;
                    BtnStartStop.Text = "Stopping...";
                    break;

                case SyncStartStopProgress.STARTED:
                    BtnStartStop.Text = "Stop SubSync";
                    BtnStartStop.Enabled = true;
                    break;

                case SyncStartStopProgress.STOPPED:
                    BtnStartStop.Text = "Start SubSync";
                    ToggleControlsEnabled(true);
                    BtnStartStop.Enabled = true;
                    break;
            }
        }

        #endregion

        #region Hide to Tray

        private void SetupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
        
        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Visible)
            {
                Show();
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        #endregion

        #region Tray Icon Context Menu

        private void NotifyIconContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == NotifyIconContextMenuItemExit)
            {
                Application.Exit();
                // Included due to application threads still running after the Application Exit
                // TODO: Validate if this the right way to do this
                Process.GetCurrentProcess().Kill();
            }
        }

        #endregion

        private void ShowTrayNotification(string message, NotificationPeriod period)
        {
            NotifyIcon.ShowBalloonTip((int)period, AppNameVersion, message, ToolTipIcon.Info);
        }

        private void CheckNotStableRelease()
        {
            if (Configuration.RunningEnvironment == "Debug")
                return;

            if (Properties.Resources.AppCurrentStage == "Alpha" || Properties.Resources.AppCurrentStage == "Beta")
            {
                MessageBox.Show
                (
                    string.Format
                    (
                        "SubSync is currently in {0} stage development. Basic functionality is provided, but expect some bugs!\n\nPlease inform the developer (ton.agner@gmail.com) if anything weird happens!",
                        Properties.Resources.AppCurrentStage
                    ),
                    AppNameVersion
                );
            }
        }
    }
}
