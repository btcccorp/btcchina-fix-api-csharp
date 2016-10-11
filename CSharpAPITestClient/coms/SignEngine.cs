using System;
using System.Security.Cryptography;
using System.Text;

namespace CSharpAPITestClient.coms
{
    internal class SignEngine
    {
        private static long CurrentTimeStamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }

        private static string ByteToString(byte[] buff)
        {
            StringBuilder sbinary = new StringBuilder();

            foreach (byte t in buff)
                sbinary.Append(t.ToString("X2").ToLower()); // hex format

            return sbinary.ToString();
        }


        private static string GetSignature(string data, string key)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes(key));
            byte[] signatureByte = hmacsha1.ComputeHash(Encoding.ASCII.GetBytes(data));
            return ByteToString(signatureByte);
        }

        public static string GetAccountString(string accessKey, string secretKey)
        {
            string methodstr = "method=getAccountInfo&params=balance";
            string tonce = "" + (CurrentTimeStamp() * 1000);
            string param = "tonce=" + tonce + "&accesskey=" + accessKey + "&requestmethod=post&id=1&" + methodstr;

            string hash = GetSignature(param, secretKey);
            string basicAuth = $"Basic {Convert.ToBase64String(Encoding.Default.GetBytes(accessKey + ":" + hash))}";

            return $"{tonce}:{basicAuth}";
        }
    }
}
