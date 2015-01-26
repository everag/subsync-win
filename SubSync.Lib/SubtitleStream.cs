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
        public SubtitleStream(Stream stream, FileInfo videoFile, CultureInfo language, SubtitleFormat format)
        {
            Stream = stream;
            VideoFile = videoFile;
            Language = language;
            Format = format;
        }

        public SubtitleStream(Stream stream, FileInfo videoFile, CultureInfo language, SubtitleFormat format, string provider)
            : this(stream, videoFile, language, format)
        {
            Provider = provider;
        }

        public Stream Stream { get; set; }
        public FileInfo VideoFile { get; set; }
        public CultureInfo Language { get; set; }
        public SubtitleFormat Format { get; set; }
        public string Provider { get; set; }

        public SubtitleInfo WriteToFile(FileInfo destinationFile)
        {
            using (Stream fileStream = File.Create(destinationFile.FullName))
            {
                Stream.CopyTo(fileStream);
            }

            if (Provider != null)
                return new SubtitleInfo(VideoFile, destinationFile, Language, Provider);
            else
                return new SubtitleInfo(VideoFile, destinationFile, Language);
        }
    }
}