using OtpNet;
using PinkLemonade.Core.Models;
using PinkLemonade.DataAccess;
using System;
using System.Collections.Generic;

namespace PinkLemonade.Core
{
    public class OtpManager
    {
        private List<Token> _tokens { get; set; }

        public List<Token> Tokens
        {
            get
            {
                return _tokens;
            }
        }

        public OtpManager()
        {
            _tokens = new List<Token>();
        }

        /// <summary>
        /// Takes a raw barcode, converts it to Token, and stores a new entry in the DB
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public Token TokenScanned(string raw)
        {
            var newToken = new Token(raw);
            newToken.StoreToken();

            _tokens.Add(newToken);

            return newToken;
        }

        public Token GetFirstToken()
        {
            if(_tokens.Count > 0)
                return _tokens.ToArray()[0];

            return null;
        }

        public List<Token> LoadTokens()
        {
            _tokens.Clear();

            foreach (var item in DataProvider.GetTokens())
                _tokens.Add(new Token(item));

            return _tokens;
        }

    }
}
