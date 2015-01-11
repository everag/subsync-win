using SubSync.Events;
using SubSync.Lib;
using SubSync.SubDb.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubSync
{
    public sealed class SyncManager
    {
        #region Singleton

        private SyncManager()
        {
            VideoFound += (sender, e) => VideoFilesFound++;
        }

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

        private ISet<CultureInfo> _supportedLanguages;

        public ISet<CultureInfo> SupportedLanguages
        {
            get
            {
                return _supportedLanguages ?? (_supportedLanguages = SubDbProvider.GetSupportedLanguages());
            }
        }

        #endregion

        private ISubtitleProvider SubDbProvider = new SubDbSubtitleProvider();

        public SyncStatus Status = SyncStatus.NOT_RUNNING;

        public IList<CultureInfo> PreferredLanguages { get; private set; }
        public ISet<DirectoryInfo> MediaDirectories { get; private set; }
        public IDictionary<DirectoryInfo, FileSystemWatcher> DirectoriesWatchers { get; private set; }

        // TODO: Events: OnStop/OnStart/OnVideoFound/OnSubDownloaded

        public event EventHandler Started, Stopped, VideoFound, SubtitleDownloaded;

        public int VideoFilesFound;

        private int Debug_WatcherFileHit;

        public SyncStatus Start(IList<CultureInfo> preferredLanguages, ISet<DirectoryInfo> mediaDirectories)
        {
            if (Status != SyncStatus.NOT_RUNNING)
                throw new InvalidOperationException("Synchronization Manager was asked to Start when it was currently running!");

            #region Arguments Validation

            if (preferredLanguages == null)
                throw new ArgumentNullException("preferredLanguages argument is null!");

            if (mediaDirectories == null)
                throw new ArgumentNullException("mediaDirectories argument is null!");

            if (!preferredLanguages.Any())
                throw new ArgumentException("preferredLanguages argument is empty!");

            if (!mediaDirectories.Any())
                throw new ArgumentException("mediaDirectories argument is empty!");

            #endregion

            PreferredLanguages = preferredLanguages;
            MediaDirectories = mediaDirectories;

            SetupFileWatchers();

            if (Started != null)
                Started(Started.Target, new StartedEventArgs(
                    MediaDirectories.Select(md => md.FullName).ToArray(), PreferredLanguages.Select(lang => lang.IetfLanguageTag).ToArray()
                ));

            Status = SyncStatus.RUNNING;

            return Status;
        }

        public SyncStatus Stop()
        {
            if (Status != SyncStatus.RUNNING)
                throw new InvalidOperationException("Synchronization Manager was asked to Stop when it was currently not running!");

            PreferredLanguages = null;
            MediaDirectories = null;

            if (Stopped != null)
                Stopped(Started.Target, new StoppedEventArgs());

            foreach (var watcher in DirectoriesWatchers.Values)
                watcher.EnableRaisingEvents = false;

            DirectoriesWatchers.Clear();

            Status = SyncStatus.NOT_RUNNING;

            return Status;
        }

        private void SetupFileWatchers()
        {
            DirectoriesWatchers = new Dictionary<DirectoryInfo, FileSystemWatcher>();

            foreach (var dir in MediaDirectories)
            {
                var watcher = new FileSystemWatcher()
                {
                    Path = dir.FullName,
                    EnableRaisingEvents = true,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                    Filter = "*.*"
                };

                watcher.Changed += (source, e) => Debug_WatcherFileHit++;

                watcher.Changed += (source, e) =>
                {
                    FileInfo videoFile = new FileInfo(e.FullPath);

                    if (!videoFile.IsVideoFile() || videoFile.HasExtensionAlongside() || !videoFile.HasMinimumSizeForSubtitleSearch())
                        return;

                    while (!videoFile.IsReady())
                        Thread.Sleep(100);

                    try
                    {
                        using (FileStream videoStream = File.OpenRead(videoFile.FullName))
                        {
                            VideoFound(VideoFound.Target, new VideoFoundEventArgs(videoFile));

                            SubtitleStream subtitleStream = DownloadSubtitle(videoStream);

                            if (subtitleStream == null)
                                return;

                            SubtitleInfo subtitleFile = subtitleStream.WriteToFile(new FileInfo(Path.ChangeExtension(videoFile.FullName, "srt")));

                            if (subtitleFile != null)
                                SubtitleDownloaded(SubtitleDownloaded.Target, new SubtitleDownloadedEventArgs(videoFile, subtitleFile));
                        }
                    }
                    catch (Exception)
                    {
                        // File not ready (e.g.: still being downloaded/copied/etc)
                    }
                };

                DirectoriesWatchers[dir] = watcher;
            }
        }

        private SubtitleStream DownloadSubtitle(FileStream videoStream)
        {
            return SubDbProvider.GetFirstSubtitleFound(videoStream, PreferredLanguages);
        }
    }
}