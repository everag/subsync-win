using SubSync.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubSync.SubDb.Client
{
    public class SubDbSubtitleProvider : ISubtitleProvider
    {
        private static readonly string IDENTIFIER = "SubDB";
        private static readonly Regex REGEX_GET_EXTENSION = new Regex(@".*filename=[a-zA-Z0-9]+\.([a-z]+)$");

        public SubDbSubtitleProvider(ReleaseInfo clientVersion)
        {
            ClientVersion = clientVersion;
        }

        public ISet<CultureInfo> GetSupportedLanguages()
        {
            var uri = new Uri(String.Format("{0}/?action=languages", ApiUrl));

            var responseInfo = SendRequest(uri);

            if (responseInfo != null)
                return GetLanguagesFromResponse(responseInfo.Item1);

            return new HashSet<CultureInfo>();
        }

        public ISet<CultureInfo> GetAvailableLanguagesForVideo(FileStream file)
        {
            var hash = file.CalculateSubDbHash();
            var uri = new Uri(String.Format("{0}/?action=search&hash={1}", ApiUrl, hash));

            var responseInfo = SendRequest(uri);

            if (responseInfo != null)
                return GetLanguagesFromResponse(responseInfo.Item1);

            return new HashSet<CultureInfo>();
        }

        public ISet<CultureInfo> GetLanguagesFromResponse(Stream responseStream)
        {
            var availableLanguages = new HashSet<CultureInfo>();

            if (responseStream != null)
            {
                using (responseStream)
                {
                    var languageCodes = new StreamReader(responseStream).ReadToEnd();

                    languageCodes.Split(',').ToList().ForEach(lan => availableLanguages.Add(CultureInfo.GetCultureInfo(lan)));
                }
            }

            return availableLanguages;
        }

        public SubtitleStream GetSubtitle(FileStream file, CultureInfo language)
        {
            var subtitleStream = GetSubtitle(file, language.TwoLetterISOLanguageName);

            if (subtitleStream != null && subtitleStream.Language == null)
                subtitleStream.Language = language;

            return subtitleStream;
        }

        public SubtitleStream GetFirstSubtitleFound(FileStream file, IList<CultureInfo> languages)
        {
            // Temporary solution until we find out why SubDB is not returning the second choice subtitle

            var availableLanguages = GetAvailableLanguagesForVideo(file);
            var language = languages.First(l => availableLanguages.Contains(l));

            return language != null ? GetSubtitle(file, language) : null;
        }

        public IList<SubtitleStream> GetAllSubtitles(FileStream file, ISet<CultureInfo> languages)
        {
            List<SubtitleStream> subtitles = new List<SubtitleStream>();

            foreach (var lang in languages)
            {
                var subtitle = GetSubtitle(file, lang);

                if (subtitle != null)
                    subtitles.Add(subtitle);
            }

            return subtitles;
        }

        private SubtitleStream GetSubtitle(FileStream file, string languageCodes)
        {
            string hash = file.CalculateSubDbHash();
            
            var requestUri = new Uri(String.Format("{0}/?action=download&hash={1}&language={2}", ApiUrl, hash, languageCodes));

            var responseData = SendRequest(requestUri);

            if (responseData != null)
            {
                var languageCode = responseData.Item2.Get("Content-Language");
                var language = languageCode != null ? new CultureInfo(languageCode) : null;

                var cd = responseData.Item2.Get("Content-Disposition");
                string extension = null;

                if (REGEX_GET_EXTENSION.IsMatch(cd))
                    extension = REGEX_GET_EXTENSION.Match(cd).Groups[1].Value;

                return new SubtitleStream(responseData.Item1, new FileInfo(file.Name), language, SubtitleFormat.ForExtension(extension), IDENTIFIER);
            }
            else
                return null;
        }

        public Tuple<Stream, WebHeaderCollection> SendRequest(Uri uri)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add("user-agent", UserAgent);
                    return new Tuple<Stream, WebHeaderCollection>(client.OpenRead(uri.ToString()), client.ResponseHeaders);
                }
            }
            catch (WebException e)
            {
                var httpStatusCode = (e.Response as HttpWebResponse).StatusCode;

                var defaultException = new ApiRequestException(string.Format("An error occurred when trying to make a request to URL {0} - Error: {1}", uri.ToString(), e.Message), httpStatusCode, e); ;

                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    switch (httpStatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return null;
                        case HttpStatusCode.BadRequest:
                            throw new ApiRequestException(string.Format("Malformed URL: {0}", uri.ToString()), httpStatusCode, e);
                        default:
                            throw defaultException;
                    }
                }
                else
                    throw defaultException;
            }
        }

        public ReleaseInfo ClientVersion { get; set; }

        private string UserAgent
        {
            get
            {
                return String.Format("{0}/{1} ({2}/{3}; {4})", "SubDB", "1.0", ClientVersion.ApplicationName, ClientVersion.Version, "https://subsync.codeplex.com");
            }
        }

        public string ApiUrl
        {
            get
            {
                return Configuration.SubDbApiUrl;
            }
        }
    }
}
