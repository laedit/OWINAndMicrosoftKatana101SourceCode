using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SecuredWebApi
{
    public class DigestAuthenticator
    {
        public static bool TryAuthenticate(string headerParameter, string method, out string userName)
        {
            string realm = null, nonce = null;
            string uri = null, response = null;
            string user = null;

            string pattern = @"(\w+)=""([^""\\]*)""\s*(?:,\s*|$)";
            var replacedString = Regex.Replace(headerParameter, pattern,
                                               (Match match) =>
                                               {
                                                   string key = match.Groups[1].Value.Trim();
                                                   string value = match.Groups[2].Value.Trim();

                                                   switch (key)
                                                   {
                                                       case "username": user = value; break;
                                                       case "realm": realm = value; break;
                                                       case "nonce": nonce = value; break;
                                                       case "uri": uri = value; break;
                                                       case "response": response = value; break;
                                                   }

                                                   return String.Empty;
                                               });

            if (user != null && realm != null && nonce != null && uri != null && response != null)
            {
                string password = user; // Just for simplicity
                string ha1 = String.Format("{0}:{1}:{2}", user, realm, password).ToMD5Hash();
                string ha2 = String.Format("{0}:{1}", method, uri).ToMD5Hash();
                string computedResponse = String.Format("{0}:{1}:{2}", ha1, nonce, ha2).ToMD5Hash();

                userName = user;
                return String.CompareOrdinal(response, computedResponse) == 0;
            }

            userName = null;
            return false;
        }
    }

    public static class HashHelper
    {
        public static string ToMD5Hash(this byte[] bytes)
        {
            StringBuilder hash = new StringBuilder();

            using (MD5 md5 = MD5.Create())
            {
                md5.ComputeHash(bytes).ToList().ForEach(b => hash.AppendFormat("{0:x2}", b));
            }

            return hash.ToString();
        }

        public static string ToMD5Hash(this string inputString)
        {
            return Encoding.UTF8.GetBytes(inputString).ToMD5Hash();
        }
    }
}