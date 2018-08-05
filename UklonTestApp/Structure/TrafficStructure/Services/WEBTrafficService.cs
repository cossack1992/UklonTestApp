using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using UklonTestApp.Exceptions;
using UklonTestApp.Helpers;
using UklonTestApp.Models;
using UklonTestApp.Structure.TrafficStructure.Interfaces;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    /// <summary>
    /// Tool to connect yandex traffic service 
    /// </summary>
    public class WEBTrafficService : ITrafficSevice
    {
        public WEBTrafficService()
        {
            Client = new HttpClient();
        }

        public HttpClient Client { get; }

        public Task<IEnumerable<Region>> GetRegionsAsync()
        {
            try
            {
                string url = @"https://goo.gl/EKCY6i";

                return Task.Run(() =>
                {
                    var htmlWeb = new HtmlWeb();
                    var document = htmlWeb.Load(url);
                    return HelperMethods.GetRegionsFromHTMLDocument(document);
                });
            }
            catch (Exception exception)
            {
                throw new TrafficException("Could not read results of request to 'https://goo.gl/EKCY6i'", exception);
            }
        }

        /// <summary>
        /// Get traffic status for region by regionCode.
        /// </summary>
        /// <param name="regionCode"></param>
        /// <param name="dateTimeNow"></param>
        /// <returns></returns>
        public async Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            string url = $@"https://export.yandex.com/bar/reginfo.xml?region={regionCode}&bustCache={dateTimeNow}";
            var xmlDoc = new XmlDocument();
            try
            {
                var response = await Client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                xmlDoc.Load(stream);

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
            catch (Exception exception) when (exception.Message == "Root element is missing.")
            {
                return null;
            }
            catch (Exception exception)
            {
                throw new TrafficException("Could not read results of request to yandex", exception);
            }
        }
    }
}
