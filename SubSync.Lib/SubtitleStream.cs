using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public class SubtitleStream
    {
        public Stream Stream { get; set; }
        public CultureInfo Language { get; set; }

        public SubtitleInfo WriteToFile(FileInfo destinationFile)
        {
            using (Stream fileStream = File.Create(destinationFile.FullName))
            {
                Stream.CopyTo(fileStream);
            }

            return new SubtitleInfo()
            {
                File = destinationFile,
                Language = Language
            };
        }
    }
}
