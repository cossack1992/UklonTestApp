using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Helpers;
using UklonTestApp.Models;
using UklonTestApp.Structure.TrafficStructure.Interfaces;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    public class LocalTrafficService : ITrafficSevice
    {
        /// <summary>
        /// Get traffic status for region by <paramref name="regionCode"/>.
        /// </summary>
        /// <param name="regionCode"></param>
        /// <returns></returns>
        public Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            return Task.Run(() =>
            {
                var random = new Random();

                string title = "SomeTitle_" + regionCode;
                var randomLevel = random.Next(1, 5).ToString();
                var randomIconId = random.Next(1, 3);
                var randomMessageId = random.Next(1, 3);

                var iconText = Icons.Where(icon => icon.Key == randomIconId).Single().Value;
                var messageText = Messages.Where(icon => icon.Key == randomMessageId).Single().Value;


                var regionTrafficStatus = new RegionTrafficStatus(
                    regionCode,
                    title,
                    dateTimeNow,
                    randomLevel,
                    iconText,
                    messageText);

                return regionTrafficStatus;
            });
        }

        public Task<IEnumerable<Region>> GetRegionsAsync()
        {
            string url = @"https://goo.gl/EKCY6i";
            return Task.Run(() =>
            {
                var htmlWeb = new HtmlWeb();
                var document = htmlWeb.Load(url);
                return HelperMethods.GetRegionsFromHTMLDocument(document);
            });
        }

        private IDictionary<int, string> Icons = new Dictionary<int, string>
        {
            { 1, "red" },
            { 2, "yellow" },
            { 3, "green" }
        };

        private IDictionary<int, string> Messages = new Dictionary<int, string>
        {
            { 1, "AAAAA" },
            { 2, "BBBBB" },
            { 3, "CCCCC" }
        };
    }
}
