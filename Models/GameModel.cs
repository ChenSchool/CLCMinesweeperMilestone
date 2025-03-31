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

        public GameModel() { }

        public static List<ButtonModel> stringToButtons(string buttons)
        {
            List<ButtonModel> buttonList = new List<ButtonModel>();
            string[] buttonArray = buttons.Split(',');
            foreach (string button in buttonArray)
            {
                if (button == "") continue;
                string[] buttonInfo = button.Split(':');
                buttonList.Add(new ButtonModel(int.Parse(buttonInfo[0]), int.Parse(buttonInfo[1]), buttonInfo[2]));
            }
            return buttonList;
        }

        public static string buttonsToString(List<ButtonModel> buttons)
        {
            string buttonString = "";
            foreach (ButtonModel button in buttons)
            {
                buttonString += button.Id + ":" + button.ButtonState + ":" + button.ButtonImage + ",";
            }
            return buttonString;
        }
    }
}
