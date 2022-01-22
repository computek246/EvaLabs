using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EvaLabs.Areas.Admin.Controllers
{
    [Authorize]
    [Area("admin")]
    public class AdminBaseController : Controller
    {

    }
}
