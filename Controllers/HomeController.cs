using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KoroneLibrary.Models;
using NLog;

namespace KoroneLibrary.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public HomeController()
        {
        }

        public IActionResult Index(string message)
        {
            Logger.Info("index visited");
            ViewData["message"] = message;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult Login()
        {
            Logger.Info("user logined.");
            return Redirect("../Article/Search");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
