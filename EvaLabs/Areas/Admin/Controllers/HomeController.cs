using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using EvaLabs.Areas.Admin.Models;

namespace EvaLabs.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {

            var apiHelpViewModel =
                Assembly.GetExecutingAssembly().GetAssemblyMethodInfo<AdminBaseController>()
                    .Where(x => x.Name == nameof(Index))
                    .Select(x => x.ToMethodInfo())
                    .OrderBy(x => x.Area)
                    .ThenBy(x => x.Controller)
                    .ThenBy(x => x.Action)
                    .ToList();

            return View(apiHelpViewModel);
        }
    }
}
