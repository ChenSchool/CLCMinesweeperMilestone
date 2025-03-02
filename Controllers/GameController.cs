using Microsoft.AspNetCore.Mvc;

namespace CLCMinesweeperMilestone.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
