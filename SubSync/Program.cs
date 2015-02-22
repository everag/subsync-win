using SubSync.GUI;
using SubSync.Lib;
using System;
using System.Collections.Generic;
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
            Application.Run(new SetupForm());
        }
    }
}