using SubSync.Lib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.SubDb.Client
{
    public class SubDbSubtitleProvider : ISubtitleProvider
    {
        public SubDbSubtitleProvider(string clientAppName, string clientAppVersion)
        {
            ClientAppName = clientAppName;
            ClientAppVersion = clientAppVersion;
        }

        private WebClient client = new WebClient();

        public ISet<CultureInfo> GetSupportedLanguages()
        {
            string url = String.Format("{0}/?action=languages", ApiUrl);

            var responseStream = SendRequest(url);
            var languages = new HashSet<CultureInfo>();

            if (responseStream != null)
            {
                using (responseStream)
                {
                    var languageCodes = new StreamReader(responseStream).ReadToEnd();
                    languages = new HashSet<CultureInfo>(languages.Concat(languageCodes.Split(',').Select(lan => CultureInfo.GetCultureInfo(lan))));
                }
            }

            return languages;
        }

        public ISet<CultureInfo> GetAvailableLanguages(FileStream file)
        {
            string hash = file.CalculateSubDbHash();
            string url = String.Format("{0}/?action=search&hash={1}", ApiUrl, hash);

            var responseStream = SendRequest(url);
            var languages = new HashSet<CultureInfo>();

            if (responseStream != null)
            {
                using (responseStream)
                {
                    var languageCodes = new StreamReader(responseStream).ReadToEnd();
                    languages = new HashSet<CultureInfo>(languages.Concat(languageCodes.Split(',').Select(lan => CultureInfo.GetCultureInfo(lan))));
                }
            }

            return languages;
        }

        public SubtitleStream GetSubtitle(FileStream file, CultureInfo language)
        {
            return GetSubtitle(file, language.TwoLetterISOLanguageName);
        }

        public SubtitleStream GetFirstSubtitleFound(FileStream file, IList<CultureInfo> languages)
        {
            return GetSubtitle(file, string.Join(",", languages.Select(lan => lan.TwoLetterISOLanguageName)));
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
            string url = String.Format("{0}/?action=download&hash={1}&language={2}", ApiUrl, hash, languageCodes);

            var subData = SendRequestForSubtitle(url);

            if (subData != null)
                return new SubtitleStream()
                {
                    Stream = subData.Item1,
                    Language = subData.Item2
                };
            else
                return null;
        }

        public Stream SendRequest(string url)
        {
            try
            {
                client.Headers.Add("user-agent", UserAgent);
                return client.OpenRead(url);
            }
            catch (WebException e)
            {
                var httpStatusCode = (e.Response as HttpWebResponse).StatusCode;

                var defaultException = new ApiRequestException(string.Format("An error occurred when trying to make a request to URL: {0} - Error: {1}", url), httpStatusCode, e);

                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    switch (httpStatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return null;
                        case HttpStatusCode.BadRequest:
                            throw new ApiRequestException(string.Format("Malformed URL: {0}", url), httpStatusCode, e);
                        default:
                            throw defaultException;
                    }
                }
                else
                    throw defaultException;
            }
        }

        public Tuple<Stream, CultureInfo> SendRequestForSubtitle(string url)
        {
            try
            {
                client.Headers.Add("user-agent", UserAgent);
                
                var stream = client.OpenRead(url);
                var languageCode = client.ResponseHeaders.Get("Content-Language");
                var language = languageCode != null ? new CultureInfo(languageCode) : null;

                return new Tuple<Stream, CultureInfo>(stream, language);
            }
            catch (WebException e)
            {
                var httpStatusCode = (e.Response as HttpWebResponse).StatusCode;

                var defaultException = new ApiRequestException(string.Format("An error occurred when trying to make a request to URL: {0} - Error: {1}", url), httpStatusCode, e);

                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    switch (httpStatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return null;
                        case HttpStatusCode.BadRequest:
                            throw new ApiRequestException(string.Format("Malformed URL: {0}", url), httpStatusCode, e);
                        default:
                            throw defaultException;
                    }
                }
                else
                    throw defaultException;
            }
        }

        public string ClientAppName { get; set; }

        public string ClientAppVersion { get; set; }

        private string UserAgent
        {
            get
            {
                return String.Format("{0}/{1} ({2}/{3}; {4})", "SubDB", "1.0", ClientAppName, ClientAppVersion, "https://subsync.codeplex.com");
            }
        }

        public string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["SubDb.Api.Url"];
            }
        }
    }
}
