using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SubSync.SubDb.Client;
using SubSync.Test.Properties;

namespace SubSync.Test
{
    [TestClass]
    public class SubDbExtensionsTest
    {
        private const string VideoDexterPromoHashOk = "ffd8d4aa68033dc03d1c8ef373b9028c";

        [TestMethod]
        public void TestCalculateSubDbHash()
        {
            string calculatedHash;

            using (var stream = new MemoryStream(Resources.VideoDexterPromo))
            {
                calculatedHash = stream.CalculateSubDbHash();
            }
            
            Assert.AreEqual(calculatedHash, VideoDexterPromoHashOk, true);
        }
    }
}
