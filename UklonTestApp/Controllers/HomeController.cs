using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UklonTestApp.Helpers;
using UklonTestApp.Models;

namespace UklonTestApp.Controllers
{
    /// <summary>
    /// Controller for region API
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception();
        }

        /// <summary>
        /// Get json collection of all available <see cref="Region"/>s.
        /// </summary>
        /// <returns>Json collection <see cref="Region"/>s.</returns>
        public JsonResult GetRegions()
        {
            string url = @"https://goo.gl/EKCY6i";
            var htmlWeb = new HtmlWeb();
            var document = htmlWeb.Load(url);

            List<Region> regions = HelperMethods.GetRegionsFromHTMLDocument(document);

            return Json(JsonConvert.SerializeObject(regions));
        }

        /// <summary>
        /// Get traffic status of <see cref="Region"/> by regionCode.
        /// </summary>
        /// <param name="regionCode">The identifier of <see cref="Region"/>.</param>
        /// <returns>Json traffic status for <see cref="Region"/>.</returns>
        public JsonResult GetRegion(int regionCode)
        {            
            string level, icon, text;
            HelperMethods.GetRegionTrafficStatusByRegionCode(regionCode, out level, out icon, out text);

            return Json(JsonConvert.SerializeObject(new { Level = level, Icon = icon, Text = text }));
        }

        /// <summary>
        /// Get error page.
        /// </summary>
        /// <returns>Error View.</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
