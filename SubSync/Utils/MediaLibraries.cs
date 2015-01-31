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
                        DirectoryInfo videoDir = null;

                        if (regex.IsMatch(videoUrl))
                        {
                            // Windows Known Folder
                            var guid = new Guid(regex.Match(videoUrl).Groups[1].Value);
                            videoDir = WindowsUtils.GetDirectoryForKnownFolderGuid(guid);
                        }
                        else
                        {
                            videoDir = new DirectoryInfo(videoUrl);
                        }

                        // If the directory path is a UNC path (e.g.: \\my-pc\Movies)
                        if (videoDir.FullName.StartsWith(@"\\"))
                            videoDir = new DirectoryInfo(WindowsUtils.GetDrivePathFromUNC(videoDir.FullName));

                        // Just to be safe
                        if (!_videosDirectories.Any(vd => vd.IsSame(videoDir)))
                            (_videosDirectories as HashSet<DirectoryInfo>).Add(videoDir);
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