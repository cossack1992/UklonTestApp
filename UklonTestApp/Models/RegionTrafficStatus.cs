using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Models
{
    public class RegionTrafficStatus
    {
        public RegionTrafficStatus(
            string regionCode,
            DateTimeOffset time,
            string level,
            string icon,
            string text)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException(nameof(regionCode));
            }

            if (string.IsNullOrWhiteSpace(level))
            {
                throw new ArgumentException(nameof(level));
            }

            if (string.IsNullOrWhiteSpace(icon))
            {
                throw new ArgumentException(nameof(icon));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException(nameof(text));
            }

            RegionCode = regionCode;
            Time = time;
            Level = level;
            Icon = icon;
            Text = text;
        }

        public string RegionCode { get; }
        public DateTimeOffset Time { get; }
        public string Level { get; }
        public string Icon { get; }
        public string Text { get; }
    }
}
