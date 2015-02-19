using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SubSync.SubDb.Client;
using SubSync.Test.Properties;
using SubSync.Lib;
using System.Collections;
using System.Collections.Generic;

namespace SubSync.Test
{
    [TestClass]
    public class ReleaseInfoTest
    {
        #region Test Mass

        private IList<Tuple<string, ReleaseInfo>> TestMass = new List<Tuple<string, ReleaseInfo>>()
        {
            new Tuple<string, ReleaseInfo>("SubSync Alpha 0.6.150217", new ReleaseInfo()
            {
                ApplicationName = "SubSync",
                MajorVersion = 0,
                FeatureNumber = 6,
                HotfixNumber = 0,
                Build = "150217",
                Stage = DevelopmentStages.Alpha
            }),
            new Tuple<string, ReleaseInfo>("SubSync 1.0", new ReleaseInfo()
            {
                ApplicationName = "SubSync",
                MajorVersion = 1,
                FeatureNumber = 0,
                HotfixNumber = 0,
                Stage = DevelopmentStages.Stable
            }),
        };

        #endregion

        [TestMethod]
        public void TestReleaseStrings()
        {
            foreach (var testCase in TestMass)
            {
                var release = new ReleaseInfo(testCase.Item1);
                Assert.AreEqual(testCase.Item2, release);
            }
        }
    }
}
