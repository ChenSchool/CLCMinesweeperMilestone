namespace CLCMinesweeperMilestone.Models
{
    public class ButtonModel
    {
        //This class to be used for the button logic in the game, there are a few states that the buttons can be those being Covered, Gold, Monster, Numbered, or Empty
        public int Id { get; set; }
        public int ButtonState { get; set; }
        public string ButtonImage { get; set; }

        public ButtonModel(int id, int buttonState, string buttonImg)
        {
            Id = id;
            ButtonState = buttonState;
            ButtonImage = buttonImg;
        }

        public ButtonModel()
        {
        }

    }
}
