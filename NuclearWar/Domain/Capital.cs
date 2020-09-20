using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuclearWar.Domain
{
    public class Capital
    {
        public string Country { get; }
        public string CapitalCity { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public Capital(string country, string capital, double latitude, double longitude)
        {
            Country = country;
            CapitalCity = capital;
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{CapitalCity}({Country}): [{Latitude}, {Longitude}]";
        }
    }
}
