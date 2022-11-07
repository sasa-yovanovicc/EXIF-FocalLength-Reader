using EXIFWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EXIFWeb.Controllers
{
    /// <summary>
    /// Show basic pages
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Home page controller.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Readme page controller.
        /// </summary>
        /// <returns></returns>
        public IActionResult Readme()
        {
            return View();
        }

        /// <summary>
        /// Error controller.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}