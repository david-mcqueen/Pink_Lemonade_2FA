using OtpNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinkLemonade.Core.Models
{
    public class StoredToken
    {

        public StoredToken(Totp totp, ScannedToken token)
        {
            this._totp = totp;
            this._scannedToken = token;
        }

        private Totp _totp { get; set; }
        private ScannedToken _scannedToken { get; set; }

        public string Issuer
        {
            get
            {
                return _scannedToken.Issuer;
            }
        }

        public string Label
        {
            get
            {
                return _scannedToken.Label;
            }
        }

        // TODO:- Don't expose this
        public string Secret
        {
            get
            {
                return _scannedToken.Secret;
            }
        }

        public string TokenCode
        {
            get
            {
                return _totp.ComputeTotp();
            }
        }

        public int RemainingSeconds
        {
            get
            {
                return _totp.RemainingSeconds();
            }
        }

        public string RemainingSecondsString
        {
            get
            {
                return RemainingSeconds.ToString();
            }
        }

    }
}
