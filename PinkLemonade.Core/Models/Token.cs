using OtpNet;
using PinkLemonade.DataAccess;
using PinkLemonade.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinkLemonade.Core.Models
{
    public class Token
    {
        public Token(string raw)
        {
            _storedToken = Utilities.ParseBarcode(raw);
            _totp = new Totp(SecretAsBytes);
        }

        public Token(StoredToken token)
        {
            this._storedToken = token;
            this._totp = new Totp(SecretAsBytes);
        }

        public Token(Totp totp, StoredToken token)
        {
            this._totp = totp;
            this._storedToken = token;
        }

        private Totp _totp { get; set; }
        private StoredToken _storedToken { get; set; }

        public string Issuer
        {
            get
            {
                return _storedToken.Issuer;
            }
        }

        public string Label
        {
            get
            {
                return _storedToken.Label;
            }
        }

        // TODO:- Don't expose this
        public string Secret
        {
            get
            {
                return _storedToken.Secret;
            }
        }

        private byte[] SecretAsBytes
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Secret))
                    return new byte[0];

                var bytes = Base32Encoding.ToBytes(Secret);
                return bytes;
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

        public void StoreToken()
        {
            DataProvider.StoreToken(_storedToken);
        }

    }
}
