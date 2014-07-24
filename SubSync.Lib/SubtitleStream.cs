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
        public string Hash { get; set; }
        public CultureInfo Language { get; set; }
    }
}
