using EvaLabs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaLabs.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private readonly ICurrentUserService<int> _currentUserService;

        public BaseController() : base()
        {

        }

        public BaseController(ICurrentUserService<int> currentUserService)
        {
            _currentUserService = currentUserService;
        }

        
    }
}
