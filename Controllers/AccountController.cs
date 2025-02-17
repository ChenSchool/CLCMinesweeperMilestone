using Microsoft.AspNetCore.Mvc;
using CLCMinesweeperMilestone.Models;
using CLCMinesweeperMilestone.Database;

namespace CLCMinesweeperMilestone.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor injection for the database context.
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(UserModel model)
        {


            if (ModelState.IsValid)
            {
                
                _context.Users.Add(model);
                _context.SaveChanges();         //BUG Currently hanging from a missing SQL server - Might be an issue with me not having one running?

                // Redirect to a success page.
                return RedirectToAction("RegisterSuccess");
            }
            else
            {
                // Log errors or inspect them in the debugger:
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            }
            // If there are validation errors, re-display the form.
            return View(model);
        }

        // GET: /Account/RegisterSuccess
        public IActionResult RegisterSuccess()
        {
            return View();
        }
    }
}
