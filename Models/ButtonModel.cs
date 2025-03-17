namespace CLCMinesweeperMilestone.Models
{
    public class ButtonModel
    {
        // This class is used for button logic in the game
        public int Id { get; set; }
        public int ButtonState { get; set; } // 0 = Empty, 1 = Revealed, 2-10 = Numbers, 3 = Skull
        public string ButtonImage { get; set; }
        public bool IsRevealed { get; set; } = false; // New property to track if the tile is revealed

        public bool IsFlagged { get; set; } = false;

        public ButtonModel(int id, int buttonState, string buttonImg)
        {
            Id = id;
            ButtonState = buttonState;
            ButtonImage = buttonImg;
        }

        public ButtonModel() { }
    }
}
