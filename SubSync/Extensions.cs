using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SubSync
{
    public static class Extensions
    {
        private static string[] VideoExtensions = { ".avi", ".mpg", ".mpeg", ".rm", ".rmvb", ".mp4", ".mkv" };

        public static bool IsVideoFile(this FileInfo file)
        {
            return Array.IndexOf(VideoExtensions, file.Extension.ToLower()) != -1;
        }

        public static bool HasExtensionAlongside(this FileInfo file)
        {
            return File.Exists(Path.ChangeExtension(file.FullName, "srt"));
        }

        public static bool HasMinimumSizeForSubtitleSearch(this FileInfo file)
        {
            // SubDB asks for a minimum of 128 bytes (64 bytes + N + 64 bytes) per file
            return file.Length > 128;
        }

        public static bool IsReady(this FileInfo file)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return inputStream.Length > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
