namespace CLCMinesweeperMilestone.Models
{
    public class GameDifficulty
    {
        public enum DifficultyLevel
        {
            Easy,
            Medium,
            Hard
        }

        public DifficultyLevel SelectedDifficulty { get; set; }
    }
}
