using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubSync.Utils
{
    public class WindowsUtils
    {
        public static bool IsRunningFromIDE
        {
            get
            {
                bool inIDE = false;
                
                string[] args = System.Environment.GetCommandLineArgs();
                
                if (args != null && args.Length > 0)
                {
                    string prgName = args[0].ToUpper();
                    inIDE = prgName.EndsWith("VSHOST.EXE");
                }

                return inIDE && System.Diagnostics.Debugger.IsAttached;
            }
        }

        [DllImport("shell32.dll")]
        private static extern Int32 SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, UInt32 dwFlags, IntPtr hToken, ref IntPtr ppszPath);

        /// <summary>
        /// Returns the DirectoryInfo information based on a given Known Folder GUID
        /// </summary>
        /// <param name="knownFolderGuid">Known Folder GUID</param>
        /// <returns>DirectoryInfo</returns>
        public static DirectoryInfo GetDirectoryForKnownFolderGuid(Guid knownFolderGuid)
        {
            IntPtr dirPtr = default(IntPtr);

            SHGetKnownFolderPath(knownFolderGuid, 0, IntPtr.Zero, ref dirPtr);
            
            string path = System.Runtime.InteropServices.Marshal.PtrToStringUni(dirPtr);
            
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(dirPtr);

            return new DirectoryInfo(path);
        }

        /// <summary>
        /// Converts a UNC path (e.g. "\\my-pc\Movies") into a local drive path ("D:\Movies").
        /// Borrowed from https://briancaos.wordpress.com/2009/03/05/get-local-path-from-unc-path
        /// </summary>
        /// <param name="uncPath">UNC path</param>
        /// <returns>Local Drive Path for the UNC path specified, of the same UNC path if not possible to convert</returns>
        public static string GetDrivePathFromUNC(string uncPath)
        {
            try
            {
                // Removes the "\\" from the UNC path and split the path
                uncPath = uncPath.Replace(@"\\", "");

                string[] uncParts = uncPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (uncParts.Length < 2)
                    return uncPath;

                // Get a connection to the server as found in the UNC path
                ManagementScope scope = new ManagementScope(@"\\" + uncParts[0] + @"\root\cimv2");

                // Query the server for the share name
                SelectQuery query = new SelectQuery("Select * From Win32_Share Where Name = '" + uncParts[1] + "'");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                // Get the path
                string path = string.Empty;

                foreach (ManagementObject obj in searcher.Get())
                {
                    path = obj["path"].ToString();
                }

                // Append any additional folders to the local path name
                if (uncParts.Length > 2)
                {
                    for (int i = 2; i < uncParts.Length; i++)
                        path = path.EndsWith(@"\") ? path + uncParts[i] : path + @"\" + uncParts[i];
                }

                return path;
            }
            catch (Exception)
            {
                return uncPath;
            }
        }

        /// <summary>
        /// Checks if a given Path is a Directory
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>bool</returns>
        public static bool IsDirectory(string path)
        {
            var fileAttrs = File.GetAttributes(path);
            
            return (fileAttrs & FileAttributes.Directory) != 0;
        }

        /// <summary>
        /// Normalizes a given Path
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>string</returns>
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
               .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
               .ToUpperInvariant();
        }

        public static void SetApplicationStartupAtLogin(bool include)
        {
            if (IsRunningFromIDE)
                return;

            var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);

            if (include)
                key.SetValue(Properties.Resources.AppName, string.Format("\"{0}\" /startsync", Application.ExecutablePath.ToString()));
            else
                key.DeleteValue(Properties.Resources.AppName, false);
        }

        public static string GetOSFriendlyName()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }
    }
}
