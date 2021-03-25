using NUnit.Framework;
using LoveKicher.AssetCloudSDK;
using LoveKicher.AssetCloudSDK.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetCloudSDKTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HashTest()
        {
            var key = "123456";
            var message = "qwerty";
            Assert.AreEqual(
                HashUtil.HMACSHA256(key, message),
                "3364ad93c083dc76d7976b875912442615cc6f7e3ce727b2316173800ca32b3a"
            );
        }

    }
}