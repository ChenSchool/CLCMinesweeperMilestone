using CLCMinesweeperMilestone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CLCMinesweeperMilestone.Controllers
{
    [ApiController]
    [Route("api/DungeonDelver")]
    public class GamesApiController : ControllerBase
    {
        // For demonstration purposes, we use the same static collection.
        // In a real app, consider dependency injection for shared data.
        private static GameCollection games = UserController.GetGameCollection();

        // GET /api/showSavedGames
        [HttpGet("showSavedGames")]
        public IActionResult ShowSavedGames()
        {
            var savedGames = games.GetAllGames().OrderBy(g => g.gameId).ToList();
            return Ok(savedGames);
        }

        // GET /api/showSavedGames/{id}
        [HttpGet("showSavedGames/{id}")]
        public IActionResult ShowSavedGame(int id)
        {
            var game = games.GetAllGames().FirstOrDefault(g => g.gameId == id);
            if (game != null)
            {
                return Ok(game);
            }
            return NotFound(new { message = "Game not found" });
        }

        // DELETE /api/deleteOneGame/{id}
        [HttpDelete("deleteOneGame/{id}")]
        public IActionResult DeleteOneGame(int id)
        {
            var game = games.GetAllGames().FirstOrDefault(g => g.gameId == id);
            if (game != null)
            {
                games.DeleteGame(game);
                return Ok(new { success = true, message = "Game deleted" });
            }
            return NotFound(new { success = false, message = "Game not found" });
        }
    }
}
