using Microsoft.AspNetCore.Mvc;

namespace Advisor.API.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
