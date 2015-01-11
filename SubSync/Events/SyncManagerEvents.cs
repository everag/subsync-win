using SubSync.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Events
{
    public class StartedEventArgs : EventArgs
    {
        public StartedEventArgs(string[] watchedDirectories, string[] languagePreferences) : base()
        {
            WatchedDirectories = watchedDirectories;
            LanguagePreferences = languagePreferences;
        }

        public string[] WatchedDirectories { get; set; }
        public string[] LanguagePreferences { get; set; }
    }

    public class StoppedEventArgs : EventArgs
    {
    }

    public class VideoFoundEventArgs : EventArgs
    {
        public VideoFoundEventArgs(FileInfo videoFile) : base()
        {
            VideoFile = videoFile;
        }

        public FileInfo VideoFile { get; set; }
    }

    public class SubtitleDownloadedEventArgs : EventArgs
    {
        public SubtitleDownloadedEventArgs(FileInfo videoFile, SubtitleInfo subtitleFile) : base()
        {
            VideoFile = videoFile;
            SubtitleFile = subtitleFile;
        }

        public FileInfo VideoFile { get; set; }
        public SubtitleInfo SubtitleFile { get; set; }
    }
}
