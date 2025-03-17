namespace CLCMinesweeperMilestone.Models
{
    public class ButtonModel
    {
        public int Id { get; set; }
        public int ButtonState { get; set; } // 0 = Empty, 1 = Revealed, 2-10 = Numbers, 3 = Skull
        public string ButtonImage { get; set; }
        public bool IsRevealed { get; set; } = false; // Property to track if the tile is revealed
        public bool IsFlagged { get; set; } = false; // New property to track if the tile is flagged

        public ButtonModel(int id, int buttonState, string buttonImg)
        {
            Id = id;
            ButtonState = buttonState;
            ButtonImage = buttonImg;
        }

        public ButtonModel() { }
    }
}
