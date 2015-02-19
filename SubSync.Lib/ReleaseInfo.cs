using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubSync.Lib
{
    public class ReleaseInfo
    {
        public string ApplicationName { get; set; }
        
        public int MajorVersion { get; set; }
        public int FeatureNumber { get; set; }
        public int HotfixNumber { get; set; }

        public string Version
        {
            get
            {
                string version = MajorVersion + "." + FeatureNumber;

                if (HotfixNumber > 0)
                    version += "." + HotfixNumber;

                return version;
            }

            set
            {
                if (!ExtractVersionNumbers.IsMatch(value))
                    throw new ArgumentException("Version String is not in the expected format", "versionString");

                var versionMatch = ExtractVersionNumbers.Match(value);

                MajorVersion = Int32.Parse(versionMatch.Groups[1].Value);
                FeatureNumber = Int32.Parse(versionMatch.Groups[2].Value);

                var hotfix = versionMatch.Groups[3].Value;

                if (!string.IsNullOrWhiteSpace(hotfix))
                    HotfixNumber = Int32.Parse(hotfix);
                else
                    HotfixNumber = 0;
            }
        }

        public DevelopmentStages Stage { get; set; }
        
        public string Build { get; set; }

        /// <summary>
        /// See https://regex101.com/r/bW7rY7/1
        /// </summary>
        private static readonly Regex ExtractReleaseInfo = new Regex(
            @"^([a-z0-9-_.]+)(?:\s(alpha|beta))? ([0-9.]+)$",
            RegexOptions.IgnoreCase
        );

        /// <summary>
        /// See https://regex101.com/r/hH1vU0/1
        /// </summary>
        private static readonly Regex ExtractVersionNumbers = new Regex(
            @"^([0-9]{1,5})\.([0-9]{1,5})(?:\.([0-9]{1,5}))?(?:\.([0-9]{6}))?$",
            RegexOptions.IgnoreCase
        );

        public ReleaseInfo() { }

        public ReleaseInfo(string releaseString)
        { 
            // e.g.: SubSync Alpha 0.6.150217

            if (!ExtractReleaseInfo.IsMatch(releaseString))
                throw new ArgumentException("Release String is not in the expected format", "releaseString");

            var releaseMatch = ExtractReleaseInfo.Match(releaseString);

            ApplicationName = releaseMatch.Groups[1].Value;

            var stageString = releaseMatch.Groups[2].Value;

            if (!string.IsNullOrWhiteSpace(stageString))
            {
                stageString = stageString.ToTitleCase();

                DevelopmentStages stage;

                if (Enum.TryParse(stageString, out stage))
                {
                    Stage = stage;
                }
            }
            else
            {
                Stage = DevelopmentStages.Stable;
            }

            #region Version Info

            var versionString = releaseMatch.Groups[3].Value;

            if (!ExtractVersionNumbers.IsMatch(versionString))
                throw new ArgumentException("Version String is not in the expected format", "releaseString");

            var versionMatch = ExtractVersionNumbers.Match(versionString);

            MajorVersion = Int32.Parse(versionMatch.Groups[1].Value);
            FeatureNumber = Int32.Parse(versionMatch.Groups[2].Value);

            var hotfix = versionMatch.Groups[3].Value;

            if (!string.IsNullOrWhiteSpace(hotfix))
                HotfixNumber = Int32.Parse(hotfix);
            else
                HotfixNumber = 0;

            var build = versionMatch.Groups[4].Value;

            if (!string.IsNullOrWhiteSpace(build))
                Build = build;

            #endregion
        }

        public ReleaseInfo(string applicationName, string versionString, string stageString, string build)
        {
            ApplicationName = applicationName;

            stageString = stageString.ToTitleCase();

            DevelopmentStages stage;

            if (!Enum.TryParse(stageString, out stage))
                throw new ArgumentException("Invalid Stage string", "stageString");

            Stage = stage;

            Version = versionString;

            Build = build;
        }

        public bool IsNewerThan(ReleaseInfo other)
        {
            // Too magical to explain
            // TODO: Just kidding, lets refactor later
            var boom = new List<Tuple<int, int>>();

            boom.Add(new Tuple<int, int>(this.MajorVersion, other.MajorVersion));
            boom.Add(new Tuple<int, int>(this.FeatureNumber, other.FeatureNumber));
            boom.Add(new Tuple<int, int>(this.HotfixNumber, other.HotfixNumber));
            boom.Add(new Tuple<int, int>((int)this.Stage, (int) other.Stage));
            
            foreach (var zing in boom)
            {
                if (zing.Item1 > zing.Item2)
                    return true;
                else if (zing.Item1 < zing.Item2)
                    return false;
            }

            return false;
        }

        public string ToString(bool includeBuild, bool includeStage, bool includeStableInfo, bool supressAbsentHotfix)
        {
            var str = ApplicationName;

            if (includeStage && (Stage != DevelopmentStages.Stable || includeStableInfo))
                str += " " + Stage;

            str += " " + MajorVersion + "." + FeatureNumber;

            if (HotfixNumber > 0 || !supressAbsentHotfix)
                str += "." + HotfixNumber;

            if (!string.IsNullOrWhiteSpace(Build)  && includeBuild)
                str += "." + Build;

            return str;
        }

        public override string ToString()
        {
            return ToString(true, true, true, false);
        }

        public override bool Equals(object obj)
        {
            if (!typeof(ReleaseInfo).IsAssignableFrom(obj.GetType()))
                return false;

            var other = (ReleaseInfo) obj;

            return 
                this.ApplicationName == other.ApplicationName &&
                this.Stage == other.Stage &&
                this.MajorVersion == other.MajorVersion &&
                this.FeatureNumber == other.FeatureNumber &&
                this.HotfixNumber == other.HotfixNumber &&
                (this.Build == null || other.Build == null || this.Build == other.Build)
            ;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                if (ApplicationName != null)
                    hash = hash * 23 + ApplicationName.GetHashCode();

                hash = hash * 23 + Stage.GetHashCode();
                hash = hash * 23 + MajorVersion.GetHashCode();
                hash = hash * 23 + FeatureNumber.GetHashCode();
                hash = hash * 23 + HotfixNumber.GetHashCode();
                hash = hash * 23 + Build.GetHashCode();

                return hash;
            }
        }
    }
}
