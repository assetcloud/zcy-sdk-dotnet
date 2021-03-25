using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using crypto = System.Security.Cryptography;

namespace LoveKicher.AssetCloudSDK.Util
{
    internal static class HashUtil
    {
        public static string HMACMD5(string key, string message)
        {
            using (var hasher = new crypto.HMACMD5(Encoding.UTF8.GetBytes(key)))
            {
                return hasher.ComputeHash(Encoding.UTF8.GetBytes(message)).ToHexString();
            }
        }

        public static string HMACSHA1(string key, string message)
        {
            using (var hasher = new crypto.HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {
                return hasher.ComputeHash(Encoding.UTF8.GetBytes(message)).ToHexString();
            }
        }

        public static string HMACSHA256(string key, string message)
        {
            using (var hasher = new crypto.HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                return hasher.ComputeHash(Encoding.UTF8.GetBytes(message)).ToHexString();
            }
        }
        public static string HMACSHA384(string key, string message)
        {
            using (var hasher = new crypto.HMACSHA384(Encoding.UTF8.GetBytes(key)))
            {
                return hasher.ComputeHash(Encoding.UTF8.GetBytes(message)).ToHexString();
            }
        }

        public static string HMACSHA512(string key, string message)
        {
            using (var hasher = new crypto.HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                return hasher.ComputeHash(Encoding.UTF8.GetBytes(message)).ToHexString();
            }
        }

        static string ToHexString(this IEnumerable<byte> bytes, bool upperCase = false)
        {
            return bytes.Aggregate("", (a, v) => 
                a + v.ToString((upperCase ? "X" : "x") + "2")
            );
        }
    }
}
