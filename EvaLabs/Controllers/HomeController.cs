using System.Diagnostics;
using System.Linq;
using System.Reflection;
using EvaLabs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
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


        public IActionResult Privacy()
        {
            var apiHelpViewModel =
                Assembly.GetExecutingAssembly().GetAssemblyMethodInfo<Controller>()
                    .Where(x => x.Name == nameof(Index))
                    .Select(x => x.ToMethodInfo())
                    .OrderBy(x => x.Area)
                    .ThenBy(x => x.Controller)
                    .ThenBy(x => x.Action)
                    .ToList();

            return View(apiHelpViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


        public IActionResult About()
        {
            return View();
        }
    }
}