using CLCMinesweeperMilestone.Filters;
using CLCMinesweeperMilestone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLCMinesweeperMilestone.Controllers
{
    public class UserController : Controller
    {
        static UserCollection users = new UserCollection();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(string UserName, string Password)
        {

            int userId = users.CheckCredentials(UserName, Password);
            UserModel userData = users.GetUserById(userId);

            if (userId > 0)
            {
                string userJson = ServiceStack.Text.JsonSerializer.SerializeToString(userData);
                HttpContext.Session.SetString("User", userJson);
                return View("LoginSuccess", userData);
            }
            else
            {
                return View("LoginFailure", userData);
            }
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        public IActionResult ProcessRegister(RegisterViewModel registerViewModel)
        {
            UserModel user = new UserModel();
            user.FirstName = registerViewModel.FirstName;
            user.LastName = registerViewModel.LastName;
            user.Sex = registerViewModel.Sex;
            user.Age = registerViewModel.Age;
            user.State = registerViewModel.State;
            user.Email = registerViewModel.Email;
            user.Username = registerViewModel.Username;
            user.SetPassword(registerViewModel.Password);
            users.AddUser(user);

            return View("Index");
        }

        [SessionCheckFilter]
        public IActionResult StartGame()
        {
            return View();
        }

        [SessionCheckFilter]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return View("Index");
        }
    }
}
