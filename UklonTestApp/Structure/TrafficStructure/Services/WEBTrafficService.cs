using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UklonTestApp.Exceptions;
using UklonTestApp.Models;
using UklonTestApp.Structure.TrafficStructure.Interfaces;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    public class WEBTrafficService : ITrafficSevice
    {
        /// <summary>
        /// Get traffic status for region by regionCode.
        /// </summary>
        /// <param name="regionCode"></param>
        /// <param name="dateTimeNow"></param>
        /// <returns></returns>
        public RegionTrafficStatus GetRegionTrafficStatus(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            string url = $@"https://export.yandex.com/bar/reginfo.xml?region={regionCode}&bustCache={dateTimeNow}";
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url);

                var title = xmlDoc.SelectSingleNode("//title")?.InnerText ?? "There is no available information!";
                var level = xmlDoc.SelectSingleNode("//level")?.InnerText ?? "There is no available information!";
                var icon = xmlDoc.SelectSingleNode("//icon")?.InnerText ?? "There is no available information!";
                var text = xmlDoc.SelectSingleNode("//hint[@lang='en']")?.InnerText ?? "There is no available information!";

                var regionTrafficStatus = new RegionTrafficStatus(
                    regionCode,
                    title,
                    dateTimeNow,
                    level,
                    icon,
                    text);

                return regionTrafficStatus;
            }
            catch(Exception exception)
            {
                if(exception.Message != "Root element is missing.")
                    throw new TrafficException("Could not read results of request to yandex", exception);

                return null;
            }
        }
    }
}
