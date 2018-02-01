using OtpNet;
using PinkLemonade.Core.Models;
using System;
using System.Collections.Generic;

namespace PinkLemonade.Core
{
    public class OtpManager
    {
        private List<StoredToken> _tokens { get; set; }

        public OtpManager()
        {
            _tokens = new List<StoredToken>();
        }

        public StoredToken TokenScanned(string raw)
        {
            var parsed = new ScannedToken(raw);
            return TokenScanned(parsed);
        }

        public StoredToken TokenScanned(ScannedToken token)
        {
            if (token == null)
                return null;

            var totp = new Totp(token.SecretAsBytes);

            var code = totp.ComputeTotp(DateTime.UtcNow);
            var remainingTime = totp.RemainingSeconds();


            var newToken = new StoredToken(totp, token);
            _tokens.Add(newToken);

            return newToken;
            
        }

        public StoredToken GetFirstToken()
        {
            if(_tokens.Count > 0)
                return _tokens.ToArray()[0];

            return null;
        }

    }
}
