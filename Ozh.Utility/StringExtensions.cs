using System;
using System.Collections.Generic;
using System.Text;

namespace Ozh.Utility
{
    public static class StringExtensions
    {
        public static bool IsValidUrl(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }

            if(Uri.TryCreate(source, UriKind.Absolute, out Uri result) &&
                (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps))
            {
                return true;
            }
            return false;
        } 
    }
}
