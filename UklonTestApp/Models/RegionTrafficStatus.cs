using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Models
{
    [Serializable]
    public class RegionTrafficStatus
    {
        public RegionTrafficStatus(
            string regionCode,
            string title,
            DateTimeOffset time,
            string level,
            string icon,
            string text)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Aargument is invalid", nameof(regionCode));
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Aargument is invalid", nameof(title));
            }

            if (string.IsNullOrWhiteSpace(level))
            {
                throw new ArgumentException("Aargument is invalid", nameof(level));
            }

            if (string.IsNullOrWhiteSpace(icon))
            {
                throw new ArgumentException("Aargument is invalid", nameof(icon));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Aargument is invalid", nameof(text));
            }

            RegionCode = regionCode;
            Title = title;
            Time = time;
            Level = level;
            Icon = icon;
            Text = text;
        }

        public string RegionCode { get; }
        public string Title { get; }
        public DateTimeOffset Time { get; }
        public string Level { get; }
        public string Icon { get; }
        public string Text { get; }
    }
}
