using LogicalSimulator.Comm.Handlers;
using LogicalSimulator.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LogicalSimulator.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void SetUp(string isPeriod, int? minutes)
        {
            if (!string.IsNullOrWhiteSpace(isPeriod) && string.Equals(isPeriod, "Infinity", StringComparison.OrdinalIgnoreCase))
                MseHandler.Instance(false, minutes ?? 0).OnStart();
            else
                MseHandler.Instance(true, minutes ?? 0).OnStart();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}