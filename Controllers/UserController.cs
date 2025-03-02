using CLCMinesweeperMilestone.Filters;
using CLCMinesweeperMilestone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace CLCMinesweeperMilestone.Controllers
{
    public class UserController : Controller
    {
        static UserCollection users = new UserCollection();

        // List to store button models
        static List<ButtonModel> buttons = new List<ButtonModel>();

        // Array of button images
        string[] buttonImages = ["Tile 1.png", "Tile 2.png", "Tile Flat.png", "Skull.png", "Gold.png", "Number 1.png", "Number 2.png", "Number 3.png", "Number 4.png", "Number 5.png", "Number 6.png", "Number 7.png", "Number 8.png"];

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

        // Constructor to initialize buttons
        public UserController()
        {
            if (buttons.Count == 0)
            {
                // Implement logic to select the difficulty of the game, Easy = 10% Skulls, Medium = 25% Skulls, Hard = 40% Skulls and tile amount (6x6, 7x7, 8x8) or however you want the logic to work
                for (int i = 0; i < 25; i++)
                {
                    int number = RandomNumberGenerator.GetInt32(0, 12);
                    buttons.Add(new ButtonModel(i, number, buttonImages[number]));
                }
            }
        }

        [SessionCheckFilter]
        public IActionResult DungeonDelver()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SelectGame(string difficulty, int boardSize)
        {
            HttpContext.Session.SetString("Difficulty", difficulty);
            HttpContext.Session.SetInt32("BoardSize", boardSize);

            return RedirectToAction("StartGame");
        }

        // Action to start the game        
        public IActionResult StartGame()
        {
            // Include logic to check the status of each button, Skull = Game Over, Gold = Good, Numbered = Number of Skulls touching the tile, Empty = No Skulls around (Fantasy Style Minesweeper because the original is a little boring IMHO)

            string difficulty = HttpContext.Session.GetString("Difficulty") ?? "Easy"; // Default to Easy
            int boardSize = HttpContext.Session.GetInt32("BoardSize") ?? 6; // Default to 6x6
            ViewBag.Difficulty = difficulty;
            ViewBag.BoardSize = boardSize;

            // Determine skull probability based on difficulty
            double skullProbability = difficulty switch
            {
                "Easy" => 0.10,   // 10% Skulls
                "Medium" => 0.25, // 25% Skulls
                "Hard" => 0.40,   // 40% Skulls
                _ => 0.10
            };

            // Reset the button list for a fresh game
            buttons.Clear();
            int totalTiles = boardSize * boardSize;

            for (int i = 0; i < totalTiles; i++)
            {
                int number = RandomNumberGenerator.GetInt32(0, 12);

                // Assign skulls based on probability
                if (RandomNumberGenerator.GetInt32(0, 100) < (skullProbability * 100))
                {
                    number = 3; // Skull index
                }

                buttons.Add(new ButtonModel(i, number, buttonImages[number]));
            }

            return View(/*Implement Game Board Logic first*/ buttons);
        }

        public IActionResult ButtonClick(int id)
        {
            // Find the button with the specified id  
            ButtonModel button = buttons.FirstOrDefault(b => b.Id == id);
            if (button != null)
            {
                // Update the button state and image  
                button.ButtonState = (button.ButtonState + 1) % 13;
                button.ButtonImage = buttonImages[button.ButtonState];
            }

            // Redirect to the Index action  
            return RedirectToAction("StartGame");
        }

        // Game Logic Here?

        [SessionCheckFilter]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return View("Index");
        }
    }
}
