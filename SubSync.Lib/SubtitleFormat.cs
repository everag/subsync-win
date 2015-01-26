using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public class SubtitleFormat
    {
        private static IDictionary<string, SubtitleFormat> ExtensionsFormats = new Dictionary<string, SubtitleFormat>() 
        { 
            { "sub", new SubtitleFormat("sub", "SubViewer") },
            { "srt", new SubtitleFormat("srt", "SubRip") },
        };

        public static string[] Extensions
        {
            get
            {
                return ExtensionsFormats.Keys.ToArray();
            }
        }

        public string Extension, Format;

        private SubtitleFormat(string extension, string format)
        {
            Extension = extension;
            Format = format;
        }

        public static SubtitleFormat ForExtension(string extension)
        {
            return ExtensionsFormats[extension.Replace(".", "").ToLower().Trim()];
        }
    }
}
