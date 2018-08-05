using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace UklonTestApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger logger;

        public BaseController(ILogger logger)
        {
            this.logger = logger;
        }
    }
}