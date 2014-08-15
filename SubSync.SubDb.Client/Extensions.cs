using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.SubDb.Client
{
    public static class Extensions
    {
        public static string CalculateSubDbHash(this Stream file)
        {
            // MD5 hash generation

            int sliceSize = 64 * 1024;
            byte[] bytes = new byte[sliceSize * 2];

            using (file)
            {
                file.Read(bytes, 0, sliceSize);
                file.Seek(-sliceSize, SeekOrigin.End);
                file.Read(bytes, sliceSize, sliceSize);
            }

            MD5 md5 = MD5.Create();

            byte[] bHash = md5.ComputeHash(bytes);

            // MD5 Hex string generation

            StringBuilder hex = new StringBuilder(bHash.Length * 2);

            foreach (byte b in bHash)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
    }
}
