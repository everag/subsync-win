using SubSync.GUI;
using SubSync.GUI.Localization;
using SubSync.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "{DD54655E-8A62-4304-A57E-1594E302A0EE}");

        [STAThread]
        static void Main(string[] args)
        {
            StartupArgs.SetArgs(args);

            if (Configuration.RunningEnvironment == "Debug")
                StartApplicationInDebug();
            else
                StartApplication();
        }

        private static void StartApplication()
        {
            // Prevent multiple executions of application
            // See: http://stackoverflow.com/questions/19147/what-is-the-correct-way-to-create-a-single-instance-application

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                InitializeGUI();
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("SubSync is already running!", "SubSync");
            }
        }

        private static void StartApplicationInDebug()
        {
            // Initialize application regardless

            InitializeGUI();
        }

        private static void InitializeGUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SetupUiLanguage();

            Application.Run(new SetupForm());
        }

        private static void SetupUiLanguage()
        {
            var settings = new UserSettings();

            if (settings.GuiLanguage == null || !L10n.AvailableLanguages.Contains(settings.GuiLanguage))
            {
                settings.GuiLanguage = GuessAppropriateUiLanguage();
                settings.Save();
            }

            Thread.CurrentThread.CurrentUICulture = settings.GuiLanguage;
        }

        private static CultureInfo GuessAppropriateUiLanguage()
        {
            CultureInfo guessedLanguage = null;

            if (L10n.AvailableLanguages.Contains(CultureInfo.CurrentUICulture))
                guessedLanguage = CultureInfo.CurrentUICulture;
            else
            {
                foreach (var lang in L10n.AvailableLanguages)
                {
                    if (lang.Parent.Equals(CultureInfo.CurrentUICulture) || lang.Equals(CultureInfo.CurrentUICulture.Parent))
                    {
                        guessedLanguage = lang;
                        break;
                    }
                }
            }

            return guessedLanguage ?? L10n.DefaultLanguage;
        }
    }
}