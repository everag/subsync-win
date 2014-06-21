using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public interface ISubtitleProvider
    {
        List<CultureInfo> GetAvailableLanguages(FileStream file);

        Stream GetSubtitle(FileStream file, CultureInfo language);

        Stream GetFirstSubtitleFound(FileStream file, List<CultureInfo> languages);

        // Dictionary<CultureInfo, Stream> GetSubtitle(FileStream file, List<CultureInfo> languages);

        Stream SendRequest(string url);

        string ApiUrl { get; }
    }
}
