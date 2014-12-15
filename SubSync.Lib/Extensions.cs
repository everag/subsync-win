using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public static class Extensions
    {
        public static string ToSubSyncDescription(this CultureInfo cultureInfo)
        {
            return String.Format("{0} ({1})", cultureInfo.TextInfo.ToTitleCase(cultureInfo.NativeName), cultureInfo.IetfLanguageTag.ToUpper());
        }
    }
}
