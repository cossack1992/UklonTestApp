using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using UklonTestApp.Controllers;

namespace UklonTestApp.Helpers
{
    public static class HelperMethods
    {
        /// <summary>
        /// Method helper. Help to get <see cref="Region"/>s from html document  
        /// </summary>
        /// <param name="document">HtmlDocument to read.</param>
        /// <returns>Collection of all <see cref="Region"/></returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="document"/> parameter is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Thrown if it could not find required node in <paramref name="document"/></exception>
        /// <exception cref="InvalidOperationException">Thrown if it could not find required attribute in node.</exception>
        public static List<Region> GetRegionsFromHTMLDocument(HtmlDocument document)
        {
            if(document == null)
            {
                throw new ArgumentException("Argument is required", nameof(document));
            }

            var table = document.DocumentNode.SelectSingleNode("//meta[@property='og:description']");

            if(table == null)
            {
                throw new InvalidOperationException("Document does not contain meta data with property 'og:description'");
            }

            var content = table.Attributes["content"];

            if(content == null)
            {
                throw new InvalidOperationException("Meta data 'og:description' does not contain attribute content");
            }

            var regions = content.Value.Replace("&nbsp;", "")
            .Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(region =>
            {
                var values = region.Split(", ");

                return new Region(values.First(), values.Last());
            })
            .ToList();

            return regions;
        }

        /// <summary>
        /// Get traffic status for region by regionCode.
        /// </summary>
        /// <param name="regionCode">The identifier of region.</param>
        /// <param name="level">The status level of traffic for region.</param>
        /// <param name="icon">The status icon of traffic for region.</param>
        /// <param name="text">The status text of traffic for region.</param>
        /// <exception cref="InvalidOperationException">Thrown if it could not find required node in document.</exception>

        public static void GetRegionTrafficStatusByRegionCode(int regionCode, out string level, out string icon, out string text)
        {
            level = null;
            icon = null;
            text = null;

            string url = $@"https://export.yandex.com/bar/reginfo.xml?region={regionCode}&bustCache={DateTimeOffset.Now}";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            var traffic = xmlDoc.SelectSingleNode("//traffic");
            if(traffic == null)
            {
                throw new InvalidOperationException("Document does not contain node 'traffic'");
            }

            level = xmlDoc.SelectSingleNode("//level")?.InnerText;
            icon = xmlDoc.SelectSingleNode("//icon")?.InnerText;
            text = xmlDoc.SelectSingleNode("//hint[@lang='en']")?.InnerText;
        }
    }
}
