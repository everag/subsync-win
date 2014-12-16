using SubSync.Lib;
using SubSync.SubDb.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync
{
    public sealed class SyncManager
    {
        #region Singleton

        private SyncManager() {}

        private static readonly SyncManager _instance = new SyncManager();

        public static SyncManager Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region Supported Languages

        private ISubtitleProvider SubDbProvider = new SubDbSubtitleProvider();

        private ISet<CultureInfo> _supportedLanguages;

        public ISet<CultureInfo> SupportedLanguages
        {
            get
            {
                return _supportedLanguages ?? (_supportedLanguages = SubDbProvider.GetSupportedLanguages());
            }
        }

        #endregion

        // TODO: Mover pra cá o controle de STATUS que está no SetupForm
    }
}