using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Models
{
    [Serializable]
    public class Region
    {
        public Region(string regionCode, string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is invalid", nameof(regionCode));
            }

            if (string.IsNullOrWhiteSpace(regionName))
            {
                throw new ArgumentException("Argument is invalid", nameof(regionName));
            }

            RegionCode = regionCode;
            RegionName = regionName;
        }

        public string RegionCode { get; }
        public string RegionName { get; }
    }
}
