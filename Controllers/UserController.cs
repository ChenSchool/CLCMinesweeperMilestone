using CLCMinesweeperMilestone.Filters;
using CLCMinesweeperMilestone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace CLCMinesweeperMilestone.Controllers
{
    public class UserController : Controller
    {
        static UserCollection users = new UserCollection();
        /*static UserDAO users = new UserDAO();*/

        // List to store button models
        static List<ButtonModel> buttons = new List<ButtonModel>();

        // Array of button images
        string[] buttonImages = { "Tile 1.png", "Tile 2.png", "Tile Flat.png", "Skull.png", "Gold.png", "Number 1.png", "Number 2.png", "Number 3.png", "Number 4.png", "Number 5.png", "Number 6.png", "Number 7.png", "Number 8.png" };

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddTestUser()
        {
            UserModel user1 = new UserModel();
            user1.Username = "test1";
            user1.SetPassword("test1");

            users.AddUser(user1); // Ensure UserDAO has an AddUser method

            return View("Index");
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
            string difficulty = HttpContext.Session.GetString("Difficulty") ?? "Easy";
            int boardSize = HttpContext.Session.GetInt32("BoardSize") ?? 6;
            ViewBag.Difficulty = difficulty;
            ViewBag.BoardSize = boardSize;

            double skullProbability = difficulty switch
            {
                "Easy" => 0.10,
                "Medium" => 0.25,
                "Hard" => 0.40,
                _ => 0.10
            };

            // ✅ Reset the button list for a fresh game
            buttons = new List<ButtonModel>();
            Random rng = new Random();

            for (int i = 0; i < boardSize * boardSize; i++)
            {
                int row = i / boardSize;
                int col = i % boardSize;

                // Assign skulls based on probability
                int tileType = rng.NextDouble() < skullProbability ? 3 : 0; // 3 = Skull, 0 = Empty
                buttons.Add(new ButtonModel(i, tileType, buttonImages[tileType]));
            }

            ComputeTileNumbers(boardSize);

            return View("StartGame", buttons);
        }
        private void ComputeTileNumbers(int boardSize)
        {
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].ButtonState == 3) continue; // Skip skulls

                int row = i / boardSize;
                int col = i % boardSize;
                int skullCount = 0;

                for (int j = 0; j < 8; j++)
                {
                    int newRow = row + dx[j];
                    int newCol = col + dy[j];
                    int newIndex = newRow * boardSize + newCol;

                    if (newRow >= 0 && newRow < boardSize && newCol >= 0 && newCol < boardSize)
                    {
                        if (buttons[newIndex].ButtonState == 3)
                        {
                            skullCount++;
                        }
                    }
                }

                if (skullCount > 0)
                {
                    buttons[i].ButtonState = skullCount + 4; // Assign numbered state
                    buttons[i].ButtonImage = buttonImages[buttons[i].ButtonState];
                }
            }
        }



        public IActionResult ButtonClick(int id)
        {
            // Find the button with the specified id  e
            ButtonModel button = buttons.ElementAt(id);
            button.IsRevealed = true;

            if (button.ButtonState == 3) // Skull (Game Over)
            {
                return RedirectToAction("Lose");
            }

            if (button.ButtonState > 0 && button.ButtonState != 3) // Numbered tile
            {
                button.IsRevealed = true;
            }

            if (button.ButtonState == 0) // Empty tile (Reveal adjacent)
            {
                RevealAdjacentTiles(id);
            }

            if (CheckWinCondition()) // Check win condition
            {
                return RedirectToAction("Win");
            }

            return View("StartGame", buttons); // ✅ Send updated board back to vie

        }

        private void RevealAdjacentTiles(int id)
        {

            int boardSize = (int)Math.Sqrt(buttons.Count);

            int row = id / boardSize;
            int col = id % boardSize;
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];
                int newIndex = newRow * boardSize + newCol;

                if (newRow >= 0 && newRow < boardSize && newCol >= 0 && newCol < boardSize)
                {
                    if (buttons[newIndex].IsRevealed) continue; // Skip if already revealed
                    ButtonClick(newIndex);
                }
            }           
        }
        private bool CheckWinCondition()
        {
            int totalSafeTiles = buttons.Count(b => b.ButtonState != 3);
            int revealedCount = buttons.Count(b => b.IsRevealed && b.ButtonState != 3);

            return revealedCount == totalSafeTiles;
        }

        // win or lose logic here
        public IActionResult Lose()
        {
            ViewBag.Message = "You hit a Skull! Game Over!";
            return View();
        }

        public IActionResult Win()
        {
            ViewBag.Message = "Congratulations! You cleared the dungeon!";
            return View();
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
