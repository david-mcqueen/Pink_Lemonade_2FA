using PinkLemonade.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace PinkLemonade.Core
{
    public static class Utilities
    {
        public static StoredToken ParseBarcode(string raw)
        {
            var uri = new Uri(raw);
            var queryParts = HttpUtility.ParseQueryString(uri.Query);
            var path = uri.GetLeftPart(UriPartial.Path).Split('/');


            var labelParts = path[path.Length - 1].Split(':');
            var label = labelParts[labelParts.Length - 1];

            return new StoredToken()
            {
                TokenRaw = raw,
                Secret = queryParts["secret"].ToLower(),
                Issuer = queryParts["issuer"],


                // TODO:- HTML parse the label, split on any colon.
                // Check the RFC specs for what we expect as a 2FA token
                // Allow for different algorithmns
                Label = label
            };
        }
    }
}
