using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}