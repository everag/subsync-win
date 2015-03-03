using SubSync.GUI.Localization;
using SubSync.Lib;
using SubSync.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync.GUI
{
    public partial class LanguageSelectionForm : Form
    {
        private UserSettings Settings = new UserSettings();

        private IDictionary<string, CultureInfo> LanguagesDescriptions;

        public LanguageSelectionForm()
        {
            LanguagesDescriptions = new Dictionary<string, CultureInfo>();

            foreach (var lang in L10n.AvailableLanguages)
                LanguagesDescriptions[lang.NativeName.ToTitleCase()] = lang;

            InitializeComponent();
        }

        private void LanguageSelectionForm_Load(object sender, EventArgs e)
        {
            Icon = Properties.Resources.SubSync_Logo;

            CboLanguage.DataSource = LanguagesDescriptions.Keys.OrderBy(s => s).ToList();
            CboLanguage.SelectedItem = Settings.GuiLanguage.NativeName.ToTitleCase();
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            var language = LanguagesDescriptions[CboLanguage.SelectedItem as string];

            if (!language.Equals(Settings.GuiLanguage))
            {
                Settings.GuiLanguage = language;
                Settings.Save();

                MessageBox.Show(L10n.Get("LanguageChangeRestart"), CurrentVersion.ReleaseInfo.ApplicationName);
            }

            Close();
        }
    }
}
