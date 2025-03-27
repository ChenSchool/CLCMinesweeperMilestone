namespace CLCMinesweeperMilestone.Models
{
    public interface IGameManager
    {
        public List<GameModel> GetAllGames();
        public GameModel GetGameById(int id);
        public void AddGame(GameModel game);
        public void DeleteGame(GameModel game);
        public void UpdateGame(GameModel game);

    }
}
