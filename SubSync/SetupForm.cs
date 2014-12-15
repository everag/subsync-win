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
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync
{
    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();
        }

        private ISubtitleProvider subDbProvider = new SubDbSubtitleProvider();
        private SyncStatus status = SyncStatus.NOT_RUNNING;

        private void SetupForm_Load(object sender, EventArgs e)
        {
            NotifyIcon.Icon = Properties.Resources.SubSync_Logo_16x16;
            Icon = Properties.Resources.SubSync_Logo_32x32;

            LoadSupportedLanguages();

            SetupDefaultFolders();
            SetupDefaultLanguages();
        }

        private void ShowTrayNotification(string message, NotificationPeriod period)
        {
            NotifyIcon.ShowBalloonTip((int)period, "SubSync Alpha", message, ToolTipIcon.Info);
        }

        #region Media folders

        private ISet<string> mediaFolders = new HashSet<string>();

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
            if (mediaFolders.Contains(folderPath))
            {
                MessageBox.Show("This folder was already selected");
                return;
            }

            mediaFolders.Add(folderPath);
            LstDirectories.Items.Add(folderPath);
        }

        private void RemoveFolder(string folderPath)
        {
            mediaFolders.Remove(folderPath);
            LstDirectories.Items.Remove(folderPath);
        }

        private void SetupDefaultFolders()
        {
            var defaultVideoDirectories = new HashSet<DirectoryInfo>(MediaLibraries.VideosDirectories);
            defaultVideoDirectories.Add(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)));

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
            var supportedLanguages = subDbProvider.GetSupportedLanguages();

            foreach (var lang in supportedLanguages)
            {
                var listItem = lang.ToSubSyncDescription();

                langDescriptionsToLanguages[listItem] = lang;
                LstLanguages.Items.Add(listItem);
            }
        }

        private void LstLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnLanguageAdd.Enabled = LstLanguages.SelectedIndices.Count > 0;
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
        }

        private void LstLanguagePreferences_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LstLanguagePreferences.SelectedIndex != -1)
            {
                BtnLanguageRemove.Enabled = true;
                BtnLanguageUp.Enabled = LstLanguagePreferences.SelectedIndex > 0;
                BtnLanguageDown.Enabled = LstLanguagePreferences.SelectedIndex < LstLanguagePreferences.Items.Count - 1;
            }
            else
            {
                BtnLanguageRemove.Enabled = false;
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

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            ShowTrayNotification("We're almost there ;)", NotificationPeriod.NORMAL);
        }
    }
}
