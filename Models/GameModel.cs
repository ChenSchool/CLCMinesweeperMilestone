namespace CLCMinesweeperMilestone.Models
{
    public class GameModel
    {

        public int userId { get; set; }
        public int gameId { get; set; }
        public DateTime DateSaved { get; set; }
        public List<ButtonModel> buttons { get; set; }


        public GameModel(int userId, int gameId, DateTime dateSaved, List<ButtonModel> buttons)
        {
            this.userId = userId;
            this.gameId = gameId;
            this.DateSaved = dateSaved;
            this.buttons = buttons;
        }
    }
}
