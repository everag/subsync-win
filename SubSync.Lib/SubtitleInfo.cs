using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public class SubtitleInfo
    {
        public SubtitleInfo(FileInfo videoFile, FileInfo subtitleFile, CultureInfo subtitleLanguage)
        {
            SubtitleFile = subtitleFile;
            VideoFile = videoFile;
            Language = subtitleLanguage;
        }

        public SubtitleInfo(FileInfo videoFile, FileInfo subtitleFile, CultureInfo subtitleLanguage, string provider) : this(videoFile, subtitleFile, subtitleLanguage)
        {
            Provider = provider;
        }

        public FileInfo VideoFile { get; set; }
        public FileInfo SubtitleFile { get; set; }
        public CultureInfo Language { get; set; }
        public string Provider { get; set; }
    }
}
