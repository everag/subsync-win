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
        public SubtitleInfo() {}

        public SubtitleInfo(FileInfo subtitleFile, CultureInfo subtitleLanguage)
        {
            File = subtitleFile;
            Language = subtitleLanguage;
        }

        public FileInfo File { get; set; }
        public CultureInfo Language { get; set; }
    }
}
