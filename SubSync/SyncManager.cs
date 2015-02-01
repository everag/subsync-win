using Quartz;
using Quartz.Impl;
using SubSync.Events;
using SubSync.Lib;
using SubSync.SubDb.Client;
using SubSync.Tasks;
using SubSync.Utils;
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
            VideoFound += OnVideoFound;
            VideoFound += (sender, e) => VideoFilesFound.Add((e as VideoFoundEventArgs).VideoFile.FullName);

            schedulerFactory = new StdSchedulerFactory();
            scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();
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

        private ISubtitleProvider SubDbProvider = new SubDbSubtitleProvider(Properties.Resources.AppName, Properties.Resources.AppVersion);

        public SyncStatus Status = SyncStatus.NOT_RUNNING;

        public IList<CultureInfo> PreferredLanguages { get; private set; }
        public ISet<DirectoryInfo> MediaDirectories { get; private set; }
        public IDictionary<DirectoryInfo, FileSystemWatcher> DirectoriesWatchers { get; private set; }
        
        private IDictionary<DirectoryInfo, ITrigger> DirectoryScheduledCheckTrigger { get; set; }
        
        public ISet<string> VideoFilesFound { get; private set; }
        public ISet<string> CurrentJobs { get; private set; }

        private ISchedulerFactory schedulerFactory;
        private IScheduler scheduler;

        private object thisLock = new Object();

        public event EventHandler Started, Stopped, VideoFound, SubtitleDownloaded;

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
            
            VideoFilesFound = new HashSet<string>();
            CurrentJobs = new HashSet<string>();

            InitializeFileWatchers();
            InitializeDirectoriesVerificationScheduler();

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

            ShutdownFileWatchers();
            ShutdownDirectoriesVerificationScheduler();

            if (Stopped != null)
                Stopped(Started.Target, new StoppedEventArgs());

            Status = SyncStatus.NOT_RUNNING;

            return Status;
        }

        #region FileSystem Watchers

        private void InitializeFileWatchers()
        {
            DirectoriesWatchers = new Dictionary<DirectoryInfo, FileSystemWatcher>();

            foreach (var dir in MediaDirectories)
            {
                var watcher = new FileSystemWatcher()
                {
                    Path = dir.FullName,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.CreationTime,
                    Filter = "*.*",
                    IncludeSubdirectories = true,
                    InternalBufferSize = 64 * 1024,
                    EnableRaisingEvents = true,
                };

                watcher.Created += OnMediaFolderChanged;
                watcher.Changed += OnMediaFolderChanged;

                DirectoriesWatchers[dir] = watcher;
            }
        }

        private void ShutdownFileWatchers()
        {
            foreach (var watcher in DirectoriesWatchers.Values)
                watcher.EnableRaisingEvents = false;

            DirectoriesWatchers.Clear();
        }

        private void OnMediaFolderChanged(object sender, FileSystemEventArgs args)
        {
            if (WindowsUtils.IsDirectory(args.FullPath))
            {
                if (args.ChangeType == WatcherChangeTypes.Created)
                {
                    Task.Run(() =>
                    {
                        foreach (var filePath in Directory.GetFiles(args.FullPath, "*.*", SearchOption.AllDirectories))
                            OnMediaFolderChanged(sender, new FileSystemEventArgs(WatcherChangeTypes.Created, Path.GetDirectoryName(filePath), Path.GetFileName(filePath)));
                    });
                }
            }
            else
            {
                lock (thisLock)
                {
                    if (CurrentJobs.Contains(args.FullPath))
                        return;

                    CurrentJobs.Add(args.FullPath);
                }

                Task.Run(() =>
                {
                    FileInfo file = new FileInfo(args.FullPath);

                    if (!file.IsVideoFile() || file.HasSubtitleAlongside() || !file.HasMinimumSizeForSubtitleSearch())
                    {
                        CurrentJobs.Remove(args.FullPath);
                        return;
                    }

                    while (!file.IsReady())
                        Thread.Sleep(100);

                    VideoFound(VideoFound.Target, new VideoFoundEventArgs(file));
                });
            }
        }

        private void OnVideoFound(object sender, EventArgs args)
        {
            var videoFile = (args as VideoFoundEventArgs).VideoFile;

            try
            {
                using (FileStream videoStream = File.OpenRead(videoFile.FullName))
                {
                    SubtitleStream subtitleStream = DownloadSubtitle(videoStream);

                    if (subtitleStream != null)
                    {
                        using (var subFileStream = subtitleStream.Stream)
                        {
                            SubtitleInfo subtitleFile = subtitleStream.WriteToFile(new FileInfo(Path.ChangeExtension(videoFile.FullName, subtitleStream.Format.Extension)));

                            if (subtitleFile != null)
                                SubtitleDownloaded(SubtitleDownloaded.Target, new SubtitleDownloadedEventArgs(videoFile, subtitleFile));
                        }
                    }
                }
            }
            catch (Exception)
            {
                // File not ready (e.g.: still being downloaded/copied/etc)
            }

            CurrentJobs.Remove(videoFile.FullName);
        }

        #endregion

        #region Directories Verification Scheduler

        private void InitializeDirectoriesVerificationScheduler()
        {
            DirectoryScheduledCheckTrigger = new Dictionary<DirectoryInfo, ITrigger>();

            foreach (var dir in MediaDirectories)
            {
                var trigger = TriggerBuilder.Create()
                    .WithIdentity(string.Format("Trigger.CheckDir [{0}]", dir.FullName), "Tasks")
                    .StartNow()
                    .WithSimpleSchedule(t => t
                        .WithIntervalInMinutes(Configuration.ScheduledFoldersCheckingDelay)
                        .RepeatForever())
                    .Build();

                DirectoryScheduledCheckTrigger[dir] = trigger;

                var job = JobBuilder.Create<CheckForVideosJob>()
                    .WithIdentity(string.Format("Job.CheckDir [{0}]", dir.FullName), "Tasks")
                    .UsingJobData(CheckForVideosJob.KEY_DIR_PATH, dir.FullName)
                    .Build();

                scheduler.ScheduleJob(job, trigger);
            }
        }

        private void ShutdownDirectoriesVerificationScheduler()
        {
            scheduler.Clear();
        }

        public void CheckForSubtitlesNow()
        {
            foreach (var dir in DirectoryScheduledCheckTrigger.Keys.ToArray())
            {
                var trigger = DirectoryScheduledCheckTrigger[dir];

                var updatedTrigger = trigger.GetTriggerBuilder().Build();
                scheduler.RescheduleJob(trigger.Key, updatedTrigger);

                DirectoryScheduledCheckTrigger[dir] = updatedTrigger;
            }
        }

        public void CheckFile(FileInfo file)
        {
            OnMediaFolderChanged(this, new FileSystemEventArgs(WatcherChangeTypes.Created, file.DirectoryName, file.Name));
        }

        #endregion

        private SubtitleStream DownloadSubtitle(FileStream videoStream)
        {
            return SubDbProvider.GetFirstSubtitleFound(videoStream, PreferredLanguages);
        }
    }
}