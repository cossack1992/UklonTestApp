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
            string title,
            DateTimeOffset time,
            string level,
            string icon,
            string text)
        {
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
