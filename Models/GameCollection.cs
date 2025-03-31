
namespace CLCMinesweeperMilestone.Models
{
    public class GameCollection : IGameManager
    {
        private List<GameModel> _games;

        public GameCollection()
        {
            _games = new List<GameModel>();
        }

        public int AddGame(GameModel game)
        {
            _games.Add(game);
            return game.gameId;
        }

        public void DeleteGame(GameModel game)
        {
            _games.Remove(game);
        }

        public List<GameModel> GetAllGames()
        {
            return _games;
        }

        public GameModel GetGameById(int id)
        {
            return _games.Find(x => x.userId == id);
        }

        public void UpdateGame(GameModel game)
        {
            var index = _games.FindIndex(x => x.userId == game.userId);
            _games[index] = game;
        }
    }
}
