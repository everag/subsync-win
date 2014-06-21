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
        private WebClient client = new WebClient();

        public List<CultureInfo> GetAvailableLanguages(FileStream file)
        {
            string hash = file.CalculateSubDbHash();
            string url = String.Format("{0}/?action=search&hash={1}", ApiUrl, hash);

            var responseStream = SendRequest(url);

            if (responseStream != null)
            {
                using (responseStream)
                {
                    var languages = new StreamReader(responseStream).ReadToEnd();
                    return languages.Split(',').Select(lan => CultureInfo.GetCultureInfo(lan)).ToList();
                }
            }
            else
            {
                return new List<CultureInfo>();
            }
        }

        public Stream GetSubtitle(FileStream file, CultureInfo language)
        {
            return GetSubtitle(file, language.TwoLetterISOLanguageName);
        }

        public Stream GetFirstSubtitleFound(FileStream file, List<CultureInfo> languages)
        {
            return GetSubtitle(file, string.Join(",", languages.Select(lan => lan.TwoLetterISOLanguageName)));
        }

        private Stream GetSubtitle(FileStream file, string languageCodes)
        {
            string hash = file.CalculateSubDbHash();
            string url = String.Format("{0}/?action=download&hash={1}&language={2}", ApiUrl, hash, languageCodes);

            return SendRequest(url);
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

        private string UserAgent
        {
            get
            {
                return String.Format("{0}/{1} ({2}/{3}; {4})", "SubDB", "1.0", "SubSync", "0.1", "http://noogenese.com.br");
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
