using CLCMinesweeperMilestone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLCMinesweeperMilestone.Controllers
{
    public class GameController : Controller
    {
        private static List<ButtonModel> GameBoard = new List<ButtonModel>(); // Store game state

        public IActionResult Index()
        {
            if (GameBoard.Count == 0) // Initialize board only once
            {
                GenerateBoard();
            }
            return View(GameBoard);
        }

        private void GenerateBoard()
        {
            int boardSize = 6;
            int idCounter = 0;
            GameBoard.Clear();

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    GameBoard.Add(new ButtonModel(idCounter++, 0, "")); // Empty tiles
                }
            }
        }

        // ✅ Toggle flag when user right-clicks a tile
        [HttpPost]
        public IActionResult ToggleFlag([FromBody] FlagRequest request)
        {
            var button = GameBoard.FirstOrDefault(b => b.Id == request.ButtonId);
            if (button != null && !button.IsRevealed) // Prevent flagging revealed tiles
            {
                button.IsFlagged = !button.IsFlagged; // Toggle flag
                button.ButtonImage = button.IsFlagged ? "flag.png" : ""; // Update image
            }
            return Json(new { success = true, flagState = button?.IsFlagged });
        }

        // ✅ Return Partial View for AJAX updates
        public IActionResult UpdateBoard()
        {
            return PartialView("_GameBoardPartial", GameBoard);
        }
    }

    // Helper class for flag requests
    public class FlagRequest
    {
        public int ButtonId { get; set; }
    }
}