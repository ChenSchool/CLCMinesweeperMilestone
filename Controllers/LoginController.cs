using Microsoft.AspNetCore.Mvc;

namespace CLCMinesweeperMilestone.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
