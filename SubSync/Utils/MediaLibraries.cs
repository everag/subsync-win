using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SubSync.Utils
{
    public class MediaLibraries
    {
        private static readonly string LibrariesFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft\\Windows\\Libraries");
        private static readonly string VideosLibraryFileName = "Videos.library-ms";
        private static readonly Regex RegexExtractGuidFromKnownFolder = new Regex(@"^knownfolder\:\{([0-9a-z\-]+)\}$", RegexOptions.IgnoreCase);

        private static IEnumerable<DirectoryInfo> _videosDirectories;

        public static IEnumerable<DirectoryInfo> VideosDirectories
        {
            get
            {
                if (_videosDirectories != null)
                    return _videosDirectories;

                _videosDirectories = new HashSet<DirectoryInfo>();

                var videosLibraryXmlFilePath = Path.Combine(LibrariesFolderPath, VideosLibraryFileName);

                if (!File.Exists(videosLibraryXmlFilePath))
                    return _videosDirectories;

                XDocument videosLibraryXml = XDocument.Load(File.OpenRead(videosLibraryXmlFilePath));
                XNamespace ns = videosLibraryXml.Root.Name.Namespace;

                try
                {
                    string[] videoFoldersUrls = videosLibraryXml
                                                    .Root
                                                    .Element(ns + "searchConnectorDescriptionList")
                                                    .Elements(ns + "searchConnectorDescription")
                                                    .Select(scd => scd.Element(ns + "simpleLocation").Element(ns + "url").Value).ToArray();

                    var regex = RegexExtractGuidFromKnownFolder;

                    foreach (var videoUrl in videoFoldersUrls)
                    {
                        if (regex.IsMatch(videoUrl))
                        {
                            // Windows Known Folder
                            var guid = new Guid(regex.Match(videoUrl).Groups[1].Value);
                            var dir = WindowsUtils.GetDirectoryForGuid(guid);
                            (_videosDirectories as HashSet<DirectoryInfo>).Add(dir);
                        }
                        else
                        {
                            // Regular Folder
                            (_videosDirectories as HashSet<DirectoryInfo>).Add(new DirectoryInfo(videoUrl));
                        }
                    }
                }
                catch (Exception)
                {
                }

                return _videosDirectories;
            }
        }
    }
}