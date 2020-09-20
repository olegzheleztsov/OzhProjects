// Create By: Oleg Gelezcov                        (olegg )
// Project: NuclearWar     File: StringToCoordinateConverter.cs    Created at 2020/08/12/2:31 AM
// All rights reserved, for personal using only
// 

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NuclearWar.Services
{
    public class StringToCoordinateConverter
    {
        private static readonly Regex DoubleRegex = new Regex(@"^[0-9]*(?:\.[0-9]*)?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public (double latitude, double longitude) Convert(string latitudeString, string longitudeString)
            => (ConvertLatitudeToNumber(latitudeString), ConvertLongitudeToNumber(longitudeString));
        
        public double ConvertLatitudeToNumber(string latitudeString)
            => ConvertStringToNumber(latitudeString, 's');

        public double ConvertLongitudeToNumber(string longitudeString)
            => ConvertStringToNumber(longitudeString, 'w');

        private static double ConvertStringToNumber(string valueString, char negativeChar)
        {
            var match = DoubleRegex.Match(valueString);
            if (!double.TryParse(match.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                throw new ArgumentException($"Invalid string: {valueString}");
            var sign = valueString.ToLower()[valueString.Length - 1];
            if (sign == negativeChar)
            {
                result = -result;
            }

            return result;
        }
    }
}