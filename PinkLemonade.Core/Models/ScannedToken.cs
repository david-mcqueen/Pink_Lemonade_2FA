
using OtpNet;
using System;
using System.Text;
using System.Web;

namespace PinkLemonade.Core.Models
{
    public enum TokenType
    {
        TOTP,
        HOTP
    }
    public class ScannedToken
    {
        public TokenType Type { get; set; }
        public string Secret { get; set; }
        public string Label { get; set; }
        public string Issuer { get; set; }
        public string RawToken { get; set; }
        public OtpHashMode Algorithm { get; set; }


        public byte[] SecretAsBytes
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Secret))
                    return new byte[0];

                
                
                var bytes = Base32Encoding.ToBytes(Secret);
                return bytes;
            }
        }

        public ScannedToken(string raw)
        {
            RawToken = raw;
            ParseBarcode(raw);
        }

        private void ParseBarcode(string raw)
        {
            var uri = new Uri(raw);
            var queryParts = HttpUtility.ParseQueryString(uri.Query);
            var path = uri.GetLeftPart(UriPartial.Path).Split('/');

            var secret = queryParts["secret"].ToLower();


            Secret = queryParts["secret"].ToLower();
            Issuer = queryParts["issuer"];


            // TODO:- HTML parse the label, split on any colon.
            // Check the RFC specs for what we expect as a 2FA token
            // Allow for different algorithmns
            Label = path[path.Length-1];
            
        }
    }
}
