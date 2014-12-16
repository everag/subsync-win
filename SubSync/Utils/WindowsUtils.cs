using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    }
}
