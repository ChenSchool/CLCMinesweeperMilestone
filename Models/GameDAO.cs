
using System.Data.SqlClient;

namespace CLCMinesweeperMilestone.Models
{
    public class GameDAO : IGameManager
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Milestone;Integrated Security=True;Connect Timeout=30;Encrypt=False";

        public int AddGame(GameModel game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Game (userId, GameId, DateSaved, Buttons) VALUES (@userId, @gameId, @DateSaved, @Buttons) SELECT SCOPE_IDENTITY()";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GameId", game.gameId);
                    command.Parameters.AddWithValue("@userId", game.userId);
                    command.Parameters.AddWithValue("@DateSaved", game.DateSaved);
                    command.Parameters.AddWithValue("@Buttons", GameModel.buttonsToString(game.buttons));
                    command.ExecuteScalar();
                    return (game.gameId);

                }
            }
        }

        public void DeleteGame(GameModel game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Game WHERE GameId = @gameId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GameId", game.gameId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<GameModel> GetAllGames()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Game";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<GameModel> games = new List<GameModel>();
                    while (reader.Read())
                    {
                        GameModel game = new GameModel
                        {
                            gameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                            userId = reader.GetInt32(reader.GetOrdinal("userId")),
                            DateSaved = reader.GetDateTime(reader.GetOrdinal("DateSaved")),
                            buttons = GameModel.stringToButtons(reader.GetString(reader.GetOrdinal("Buttons")))
                        };

                        games.Add(game);
                    }
                    return games;
                }
            }
        }

        public GameModel GetGameById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Game WHERE gameId = @GameId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GameId", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        GameModel game = new GameModel
                        {
                            gameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                            userId = reader.GetInt32(reader.GetOrdinal("userId")),
                            DateSaved = reader.GetDateTime(reader.GetOrdinal("DateSaved")),
                            buttons = GameModel.stringToButtons(reader.GetString(reader.GetOrdinal("Buttons")))
                        };
                        return game;
                    }
                    return null;
                }
            }
        }

        public void UpdateGame(GameModel game)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Game SET userId = @userId, DateSaved = @DateSaved, Buttons = @Buttons WHERE gameId = @GameId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GameId", game.gameId);
                    command.Parameters.AddWithValue("@userId", game.userId);
                    command.Parameters.AddWithValue("@DateSaved", game.DateSaved);
                    command.Parameters.AddWithValue("@Buttons", GameModel.buttonsToString(game.buttons));
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
