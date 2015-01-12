﻿using SubSync.Events;
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
            VideoFound += (sender, e) => VideoFilesFound.Add((e as VideoFoundEventArgs).VideoFile.FullName);
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
        
        public ISet<string> VideoFilesFound { get; private set; }

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
                    EnableRaisingEvents = true,
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                    Filter = "*.*",
                    IncludeSubdirectories = true,
                    InternalBufferSize = 64 * 1024
                };

                watcher.Changed += (source, e) =>
                {
                    Task.Run(() => 
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
                    });
                };

                DirectoriesWatchers[dir] = watcher;
            }
        }

        private void ShutdownFileWatchers()
        {
            foreach (var watcher in DirectoriesWatchers.Values)
                watcher.EnableRaisingEvents = false;

            DirectoriesWatchers.Clear();
        }

        #endregion

        #region Directories Verification Scheduler

        private void InitializeDirectoriesVerificationScheduler()
        {
            // To-do! To-do! To-do to-do to-do to-do to-dooooooo
        }

        private void ShutdownDirectoriesVerificationScheduler()
        {
            // To-do! To-do! To-do to-do to-do to-do to-dooooooo
        }

        #endregion

        private SubtitleStream DownloadSubtitle(FileStream videoStream)
        {
            return SubDbProvider.GetFirstSubtitleFound(videoStream, PreferredLanguages);
        }
    }
}