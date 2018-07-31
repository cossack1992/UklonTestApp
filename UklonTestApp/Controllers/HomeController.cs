using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UklonTestApp.Models;

namespace UklonTestApp.Controllers
{
    /// <summary>
    /// Controller for region API
    /// </summary>
    public class RegionController : Controller
    {
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
