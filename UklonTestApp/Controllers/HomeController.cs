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
using UklonTestApp.Structure;
using UklonTestApp.Structure.Service;

namespace UklonTestApp.Controllers
{
    /// <summary>
    /// Controller for region API
    /// </summary>
    public class HomeController : Controller
    {
        public IService Service { get; }

        public HomeController(IService service)
        {
            Service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get json collection of all available <see cref="RegionModel"/>s.
        /// </summary>
        /// <returns>Json collection <see cref="RegionModel"/>s.</returns>
        public async Task<JsonResult> GetRegions()
        {
            var results = await Service.GetRegions();
            return Json(JsonConvert.SerializeObject(results));
        }

        /// <summary>
        /// Get traffic status of <see cref="RegionModel"/> by regionCode.
        /// </summary>
        /// <param name="regionCode">The identifier of <see cref="RegionModel"/>.</param>
        /// <returns>Json traffic status for <see cref="RegionModel"/>.</returns>
        public async Task<JsonResult> GetRegion(string regionCode)
        {
            var result = await Service.GetRegionTrafficStatusAsync(regionCode, DateTimeOffset.Now);
            return Json(result is RegionTrafficStatus ? JsonConvert.SerializeObject(result) : "SORRY, Service is not available!");
        }

        /// <summary>
        /// Get traffic status of <see cref="RegionModel"/> by regionCode.
        /// </summary>
        /// <param name="regionCode">The identifier of <see cref="RegionModel"/>.</param>
        /// <returns>Json traffic status for <see cref="RegionModel"/>.</returns>
        public async Task<JsonResult> GetRegionStatuses()
        {
            var regions = await Service.GetRegions();

            var statuses = await Service.GetRegionTrafficStatuses(regions.Select(region => region.RegionCode), DateTimeOffset.Now);
            return Json(JsonConvert.SerializeObject(statuses.Where(status => status is RegionTrafficStatus)));
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
