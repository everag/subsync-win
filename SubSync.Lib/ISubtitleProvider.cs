using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public interface ISubtitleProvider
    {
        ISet<CultureInfo> GetSupportedLanguages();

        ISet<CultureInfo> GetAvailableLanguages(FileStream file);

        SubtitleStream GetSubtitle(FileStream file, CultureInfo language);

        SubtitleStream GetFirstSubtitleFound(FileStream file, List<CultureInfo> languages);

        IList<SubtitleStream> GetAllSubtitles(FileStream file, ISet<CultureInfo> languages);

        Stream SendRequest(string url);

        string ApiUrl { get; }
    }
}
