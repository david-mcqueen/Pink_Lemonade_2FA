using OtpNet;
using PinkLemonade.Core.Models;
using System;
using System.Collections.Generic;

namespace PinkLemonade.Core
{
    public class OtpManager
    {
        public Totp Token;

        public Totp TokenScanned(ScannedToken token)
        {
            if (token != null)
            {
                var totp = new Totp(token.SecretAsBytes);

                var code = totp.ComputeTotp(DateTime.UtcNow);
                var remainingTime = totp.RemainingSeconds();
                Token = totp;
                return totp;
            }

            return new Totp(new byte[1]);
        }

    }
}
