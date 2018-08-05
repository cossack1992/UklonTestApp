using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Models
{
    public class Region
    {
        public Region(string regionCode, string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid", nameof(regionCode));
            }

            if (string.IsNullOrWhiteSpace(regionName))
            {
                throw new ArgumentException("Argument is not valid", nameof(regionName));
            }

            RegionCode = regionCode;
            RegionName = regionName;
        }

        public string RegionCode { get; }
        public string RegionName { get; }
    }
}
