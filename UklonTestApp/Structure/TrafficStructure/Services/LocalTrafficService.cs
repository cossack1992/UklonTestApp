using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;
using UklonTestApp.Structure.TrafficStructure.Interfaces;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    public class RandomTrafficService : ITrafficSevice
    {
        /// <summary>
        /// Get traffic status for region by regionCode.
        /// </summary>
        /// <param name="regionCode"></param>
        /// <returns></returns>
        public RegionTrafficStatus GetRegionTrafficStatus(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            var random = new Random();

            var iconText = Icons.Where(icon => icon.iconId == random.Next(1, 3)).Single().text;
            var messageText = Messages.Where(icon => icon.iconId == random.Next(1, 3)).Single().text;
            var regionTrafficStatus = new RegionTrafficStatus(
                regionCode,
                "Some title",
                dateTimeNow,
                random.Next(1, 5).ToString(),
                iconText,
                messageText);

            return regionTrafficStatus;
        }

        private IList<(int iconId, string text)> Icons = new List<(int iconId, string text)>
        {
            (1, "red"),
            (2, "yellow"),
            (3, "green")
        };

        private IList<(int iconId, string text)> Messages = new List<(int iconId, string text)>
        {
            (1, "AAAAA"),
            (2, "BBBBB"),
            (3, "CCCCC")
        };
    }
}
