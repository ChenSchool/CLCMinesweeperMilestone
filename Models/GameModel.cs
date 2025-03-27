namespace CLCMinesweeperMilestone.Models
{
    public class GameModel
    {

        public int userId { get; set; }

        public DateTime DateSaved { get; set; }

        public List<ButtonModel> buttons { get; set; }


        public GameModel(int userId, DateTime dateSaved, List<ButtonModel> buttons)
        {
            this.userId = userId;
            this.DateSaved = dateSaved;
            this.buttons = buttons;
        }
    }
}
