using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Utils
{
    public class WindowsUtils
    {
        [DllImport("shell32.dll")]
        private static extern Int32 SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, UInt32 dwFlags, IntPtr hToken, ref IntPtr ppszPath);

        public static DirectoryInfo GetDirectoryForGuid(Guid knownFolderGuid)
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
    }
}
