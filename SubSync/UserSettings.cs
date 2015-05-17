using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync
{
    public class UserSettings : ApplicationSettingsBase
    {
        #region User Scoped Settings

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool FirstRun
        {
            get
            {
                return ((bool) this["FirstRun"]);
            }
            set
            {
                this["FirstRun"] = (bool) value;
            }
        }

        [UserScopedSetting()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public CultureInfo GuiLanguage
        {
            get
            {
                return ((CultureInfo) this["GuiLanguage"]);
            }
            set
            {
                this["GuiLanguage"] = (CultureInfo) value;
            }
        }

        [UserScopedSetting()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public ISet<DirectoryInfo> MediaFolders
        {
            get
            {
                return ((ISet<DirectoryInfo>) this["MediaFolders"]);
            }
            set
            {
                this["MediaFolders"] = (ISet<DirectoryInfo>) value;
            }
        }

        [UserScopedSetting()]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public IList<CultureInfo> SubtitleLanguagesPreference
        {
            get
            {
                return ((IList<CultureInfo>)this["SubtitleLanguagesPreference"]);
            }
            set
            {
                this["SubtitleLanguagesPreference"] = (IList<CultureInfo>)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool RunAtStartup
        {
            get
            {
                return ((bool) this["RunAtStartup"]);
            }
            set
            {
                this["RunAtStartup"] = (bool) value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("true")]
        public bool UpgradeSettings
        {
            get
            {
                return ((bool) this["UpgradeSettings"]);
            }
            set
            {
                this["UpgradeSettings"] = (bool) value;
            }
        }

        #endregion
    }
}
