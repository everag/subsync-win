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
        private Icon appIcon = Properties.Resources.appIcon;
        private SyncStatus status = SyncStatus.NOT_RUNNING;

        private void SetupForm_Load(object sender, EventArgs e)
        {
            var supportedLanguages = subDbProvider.GetSupportedLanguages();

            foreach (var lang in supportedLanguages)
            {
                var listItem = String.Format("{0} ({1})", lang.TextInfo.ToTitleCase(lang.NativeName), lang.TwoLetterISOLanguageName.ToUpper());

                langDescriptionsToLanguages[listItem] = lang;
                LstLanguages.Items.Add(listItem);
            }

            NotifyIcon.Icon = appIcon;
            Icon = appIcon;
        }

        private void ShowTrayNotification(string message, NotificationPeriod period)
        {
            NotifyIcon.ShowBalloonTip((int) period, "SubSync Alpha", message, ToolTipIcon.Info);
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

        #endregion

        #region Languages

        private IDictionary<string, CultureInfo> langDescriptionsToLanguages = new Dictionary<string, CultureInfo>();
        private IList<CultureInfo> languagePreferences = new List<CultureInfo>();

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

        private void AddLanguage(string langDescription)
        {
            CultureInfo language = langDescriptionsToLanguages[langDescription];

            languagePreferences.Add(language);

            LstLanguagePreferences.Items.Add(langDescription);

            LstLanguages.Items.Remove(langDescription);
        }

        private void BtnLanguageRemove_Click(object sender, EventArgs e)
        {
            string languageToRemove = LstLanguagePreferences.SelectedItem as string;
            RemoveLanguage(languageToRemove);
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

        #endregion

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            ShowTrayNotification("We're almost there ;)", NotificationPeriod.NORMAL);
        }
    }
}
