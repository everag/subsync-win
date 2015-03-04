using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync.GUI.Localization
{
    public static class L10n
    {
        private static IDictionary<Type, ResourceManager> FormMessages;
        private static ResourceManager AppMessages;
        private static UserSettings Settings = new UserSettings();

        public static ISet<CultureInfo> AvailableLanguages;
        public static CultureInfo DefaultLanguage = new CultureInfo("en");

        static L10n()
        {
            FormMessages = new Dictionary<Type, ResourceManager>();
            AppMessages = Messages.ResourceManager;
            AvailableLanguages = new HashSet<CultureInfo>() { new CultureInfo("pt-BR"), new CultureInfo("en") };
        }

        private static ResourceManager GetResourceManager(Form form)
        {
            if (!FormMessages.ContainsKey(form.GetType()))
            {
                var res = new ResourceManager(form.GetType().FullName, form.GetType().Assembly);
                FormMessages[form.GetType()] = res;
            }

            return FormMessages[form.GetType()];
        }

        public static string Get(string key, Form form, params string[] values)
        {
            return GetLocalizedMessage(GetResourceManager(form), key, values);
        }

        public static string Get(string key, params string[] values)
        {
            return GetLocalizedMessage(AppMessages, key, values);
        }

        private static string GetLocalizedMessage(ResourceManager res, string key, params string[] values)
        {
            var localizedMessage = res.GetString(key, Settings.GuiLanguage);

            if (localizedMessage == null)
                throw new ArgumentException(string.Format("Message key {0} not found!", key));

            if (values.Any())
                return string.Format(localizedMessage, values).Replace("\\r", "\r").Replace("\\n", "\n");
            else
                return localizedMessage.Replace("\\r", "\r").Replace("\\n", "\n");
        }
    }
}
