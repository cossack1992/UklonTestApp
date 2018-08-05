using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UklonTestApp.Exceptions;
using UklonTestApp.Models;

namespace UklonTestApp.Helpers
{
    public static class HelperMethods
    {
        /// <summary>
        /// Method helper. Help to get <see cref="RegionModel"/>s from html document  
        /// </summary>
        /// <param name="document">HtmlDocument to read.</param>
        /// <returns>Collection of all <see cref="RegionModel"/></returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="document"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ReadingHTMLDocumentException">Thrown if it could not find required node in <paramref name="document"/></exception>
        /// <exception cref="ReadingHTMLDocumentException">Thrown if it could not find required attribute in node.</exception>
        public static IEnumerable<Region> GetRegionsFromHTMLDocument(HtmlDocument document)
        {
            if(document == null)
            {
                throw new ArgumentException("Argument is not valid!", nameof(document));
            }

            var table = document.DocumentNode.SelectSingleNode("//meta[@property='og:description']");

            if(table == null)
            {
                throw new ReadingHTMLDocumentException("Document does not contain meta data with property 'og:description'");
            }

            var content = table.Attributes["content"];

            if(content == null)
            {
                throw new ReadingHTMLDocumentException("Meta data 'og:description' does not contain attribute content");
            }

            var regions = content.Value.Replace("&nbsp;", "")
            .Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
            .Skip(1)
            .Select(region =>
            {
                var values = region.Replace(" ", "").Split(",");

                return new Region(values.First(), values.Last());
            })
            .ToList();

            return regions;
        }
    }
}
