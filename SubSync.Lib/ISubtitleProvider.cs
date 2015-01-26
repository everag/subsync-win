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

        ISet<CultureInfo> GetAvailableLanguagesForVideo(FileStream videoFile);

        SubtitleStream GetSubtitle(FileStream videoFile, CultureInfo language);

        SubtitleStream GetFirstSubtitleFound(FileStream videoFile, IList<CultureInfo> languages);

        IList<SubtitleStream> GetAllSubtitles(FileStream videoFile, ISet<CultureInfo> languages);

        Tuple<Stream, WebHeaderCollection> SendRequest(Uri uri);

        string ApiUrl { get; }
    }
}
