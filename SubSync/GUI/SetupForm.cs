using SubSync.Events;
using SubSync.GUI;
using SubSync.Lib;
using SubSync.SubDb.Client;
using SubSync.Tasks;
using SubSync.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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

namespace SubSync.GUI
{
    public partial class SetupForm : Form
    {
        private string AppCompleteDescription
        {
            get 
            {
                if (CurrentVersion.ReleaseInfo.Stage == DevelopmentStages.Stable)
                    return CurrentVersion.ReleaseInfo.ToString(false, false, false, true);

                else if (CurrentVersion.ReleaseInfo.Stage == DevelopmentStages.Beta)
                    return CurrentVersion.ReleaseInfo.ToString(false, true, false, true);

                else if (CurrentVersion.ReleaseInfo.Stage == DevelopmentStages.Alpha)
                    return CurrentVersion.ReleaseInfo.ToString(true, true, false, true);

                else
                    return CurrentVersion.ReleaseInfo.ApplicationName; 
            } 
        }

        private string AppShortDescription
        {
            get
            {
                if (CurrentVersion.ReleaseInfo.Stage == DevelopmentStages.Stable)
                    return CurrentVersion.ReleaseInfo.ApplicationName;

                else
                    return CurrentVersion.ReleaseInfo.ApplicationName + " " + CurrentVersion.ReleaseInfo.Stage;
            }
        }

        private UserSettings Settings;

        public SetupForm()
        {
            InitializeComponent();
        }

        private SyncManager SyncManager = SubSync.SyncManager.Instance;

        private void SetupForm_Load(object sender, EventArgs e)
        {
            NotifyIcon.Icon = Properties.Resources.SubSync_Logo;
            NotifyIcon.Text = AppCompleteDescription;

            LbWelcomeTitle.Text = string.Format("Welcome to {0}!", AppShortDescription);

            if (SubSync.Lib.Configuration.RunningEnvironment == "Debug")
                Text = string.Format("{0} [{1}]", AppCompleteDescription, SubSync.Lib.Configuration.RunningEnvironment);
            else
                Text = AppCompleteDescription;

            Icon = Properties.Resources.SubSync_Logo;

            Settings = new UserSettings();

            if (Settings.FirstRun)
            {
                Settings.MediaFolders = GetDefaultFolders();
                Settings.SubtitleLanguagesPreference = GetDefaultLanguages();

                Settings.FirstRun = false;

                Settings.Save();
            }

            SetupSyncManager();

            LoadSupportedLanguages();

            FillSettingsInfo();

            CheckStartButton();

            TaskManager.StartTask<CheckForUpdatesJob>();

            if (StartupArgs.InitializeInStartState && IsConfigurationOk())
            {
                StartStopSync();
            }
        }

        private void FillSettingsInfo()
        {
            foreach (var videoDirectory in Settings.MediaFolders)
            {
                LstDirectories.Items.Add(videoDirectory.FullName);
            }

            var languagesToRemoveFromSettings = new HashSet<CultureInfo>();

            for (int i = 0; i < Settings.SubtitleLanguagesPreference.Count; i++)
            {
                var language = Settings.SubtitleLanguagesPreference[i];
                var description = language.ToSubSyncDescription();

                if (langDescriptionsToLanguages.ContainsKey(description))
                {
                    LstLanguagePreferences.Items.Add(description);
                    LstLanguages.Items.Remove(description);
                }
                else if (language.Parent != CultureInfo.InvariantCulture && !Settings.SubtitleLanguagesPreference.Contains(language.Parent))
                {
                    Settings.SubtitleLanguagesPreference[i] = language.Parent;
                    Settings.Save();
                    i--;
                }
            }

            NotifyIconContextMenuItemRunAtLogin.Checked = Settings.RunAtStartup;
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

        private ISet<DirectoryInfo> GetDefaultFolders()
        {
            var defaultVideoDirectories = new HashSet<DirectoryInfo>(MediaLibraries.VideosDirectories);
            var myVideosDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));

            if (!defaultVideoDirectories.Any(vd => vd.IsSame(myVideosDir)))
                defaultVideoDirectories.Add(myVideosDir);

            return defaultVideoDirectories;
        }

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

            if (Settings.MediaFolders.Any(mf => mf.IsSame(folderToAdd)))
            {
                MessageBox.Show("This folder was already selected");
                return;
            }

            Settings.MediaFolders.Add(folderToAdd);
            LstDirectories.Items.Add(folderPath);

            Settings.Save();

            CheckStartButton();
        }

        private void RemoveFolder(string folderPath)
        {
            var folderToRemove = new DirectoryInfo(folderPath);

            int removed = (Settings.MediaFolders as HashSet<DirectoryInfo>).RemoveWhere(mf => mf.IsSame(folderToRemove));
            
            if (removed > 0)
            {
                LstDirectories.Items.Remove(folderPath);
                Settings.Save();
            }

            CheckStartButton();
        }

        #endregion

        #region Languages

        private IDictionary<string, CultureInfo> langDescriptionsToLanguages = new Dictionary<string, CultureInfo>();

        private void LoadSupportedLanguages()
        {
            foreach (var lang in SyncManager.SupportedLanguages)
            {
                var listItem = lang.ToSubSyncDescription();

                langDescriptionsToLanguages[listItem] = lang;
                LstLanguages.Items.Add(listItem);
            }
        }

        private IList<CultureInfo> GetDefaultLanguages()
        {
            return new List<CultureInfo>() { CultureInfo.InstalledUICulture };
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
                Settings.SubtitleLanguagesPreference.Add(language);

                LstLanguagePreferences.Items.Add(description);
                LstLanguages.Items.Remove(description);

                Settings.Save();

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

            Settings.SubtitleLanguagesPreference.Remove(language);

            LstLanguages.Items.Add(langDescription);

            LstLanguagePreferences.Items.Remove(langDescription);

            Settings.Save();

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

            Settings.SubtitleLanguagesPreference.Insert(currentIdx - 1, langDescriptionsToLanguages[languageToMove]);
            Settings.SubtitleLanguagesPreference.RemoveAt(currentIdx + 1);

            Settings.Save();
        }

        private void BtnLanguageDown_Click(object sender, EventArgs e)
        {
            var languageToMove = LstLanguagePreferences.SelectedItem as string;
            var currentIdx = LstLanguagePreferences.SelectedIndex;

            LstLanguagePreferences.Items.Insert(currentIdx + 2, languageToMove);
            LstLanguagePreferences.SelectedIndex = currentIdx + 2;
            LstLanguagePreferences.Items.RemoveAt(currentIdx);

            Settings.SubtitleLanguagesPreference.Insert(currentIdx + 2, langDescriptionsToLanguages[languageToMove]);
            Settings.SubtitleLanguagesPreference.RemoveAt(currentIdx);

            Settings.Save();
        }

        #endregion

        #region Start/Stop

        private void CheckStartButton()
        {
            bool enableStart = IsConfigurationOk();

            BtnStartStop.Enabled = enableStart;
            NotifyIconContextMenuItemStartStop.Enabled = enableStart;
        }

        private bool IsConfigurationOk()
        {
            return Settings.SubtitleLanguagesPreference.Any() && Settings.MediaFolders.Any();
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

            NotifyIconContextMenuItemCheckNow.Enabled = !enable;
        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            StartStopSync();
        }

        private void StartStopSync()
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

                SyncManager.Start(Settings.SubtitleLanguagesPreference, Settings.MediaFolders);

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

                    NotifyIconContextMenuItemStartStop.Enabled = false;
                    NotifyIconContextMenuItemStartStop.Text = "Starting...";
                    
                    break;

                case SyncStartStopProgress.STOPPING:
                    ToggleControlsEnabled(false);
                    
                    BtnStartStop.Enabled = false;
                    BtnStartStop.Text = "Stopping...";
                    
                    NotifyIconContextMenuItemStartStop.Enabled = false;
                    NotifyIconContextMenuItemStartStop.Text = "Stopping...";
                    
                    break;

                case SyncStartStopProgress.STARTED:
                    BtnStartStop.Enabled = true;
                    BtnStartStop.Text = "Stop SubSync";
                    
                    NotifyIconContextMenuItemStartStop.Enabled = true;
                    NotifyIconContextMenuItemStartStop.Text = "Stop SubSync";
                    
                    break;

                case SyncStartStopProgress.STOPPED:
                    ToggleControlsEnabled(true);

                    BtnStartStop.Enabled = true;
                    BtnStartStop.Text = "Start SubSync";
                    
                    NotifyIconContextMenuItemStartStop.Enabled = true;
                    NotifyIconContextMenuItemStartStop.Text = "Start SubSync";

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
            ToggleMainWindow();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                ToggleMainWindow();
                return true;
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Tray Icon Context Menu

        private void NotifyIconContextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Open SubSync

            if (e.ClickedItem == NotifyIconContextMenuItemOpenGUI)
            {
                WindowManager.OpenWindow(typeof(SetupForm));
            }

            // Start/Stop SubSync
            
            if (e.ClickedItem == NotifyIconContextMenuItemStartStop)
            {
                StartStopSync();
            }

            // Check for subtitles now

            if (e.ClickedItem == NotifyIconContextMenuItemCheckNow)
            {
                SyncManager.CheckForSubtitlesNow();
            }

            // Check for updates

            if (e.ClickedItem == NotifyIconContextMenuItemCheckUpdates)
            {
                Task.Run(() =>
                {
                    TaskManager.AntecipateTask<CheckForUpdatesJob>(false);
                });
            }

            // About SubSync

            if (e.ClickedItem == NotifyIconContextMenuItemAbout)
            {
                WindowManager.OpenWindow(typeof(AboutForm));
            }

            // Exit

            if (e.ClickedItem == NotifyIconContextMenuItemExit)
            {
                Application.Exit();
                // Included due to application threads still running after Application Exit
                // TODO: Validate if this the right way to do this
                Process.GetCurrentProcess().Kill();
            }
        }

        private void NotifyIconContextMenuItemRunAtLogin_CheckedChanged(object sender, EventArgs e)
        {
            var itemChecked = NotifyIconContextMenuItemRunAtLogin.Checked;

            Settings.RunAtStartup = itemChecked;
            Settings.Save();

            WindowsUtils.SetApplicationStartupAtLogin(itemChecked);
        }

        #endregion

        private void ShowTrayNotification(string message, NotificationPeriod period)
        {
            NotifyIcon.ShowBalloonTip((int)period, AppShortDescription, message, ToolTipIcon.Info);
        }

        private void ToggleMainWindow()
        {
            if (!Visible)
                WindowManager.OpenWindow(typeof(SetupForm));
            else
                Hide();
        }
    }
}