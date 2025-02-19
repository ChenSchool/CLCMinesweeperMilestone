
namespace CLCMinesweeperMilestone.Models
{
    public class UserCollection : IUserManager
    {
        private List<UserModel> _users;

        public UserCollection()
        {
            _users = new List<UserModel>();
            GenerateUserData();
        }

        public void GenerateUserData()
        {
            UserModel user1 = new UserModel();
            user1.Username = "test1";
            user1.SetPassword("test1");
            AddUser(user1);

            UserModel user2 = new UserModel();
            user2.Username = "Megan";
            user2.SetPassword("princess");
            AddUser(user2);
        }

        public int AddUser(UserModel user)
        {
            user.Id = _users.Count + 1;
            _users.Add(user);
            return user.Id;
        }

        public int CheckCredentials(string username, string password)
        {
            foreach (UserModel user in _users)
            {
                if (user.Username == username && user.VerifyPassword(password))
                {
                    return user.Id;
                }
            }
            return 0;
        }

        public void DeleteUser(UserModel user)
        {
            _users.Remove(user);
        }

        public List<UserModel> GetAllUsers()
        {
            return _users;
        }

        public UserModel GetUserById(int id)
        {
            foreach(UserModel user in _users)
            {
                if(user.Id == id)
                    return user;
            }
            return null;
        }

        public void UpdateUser(UserModel user)
        {
            UserModel findUser = GetUserById(user.Id);
            if (findUser != null)
            {
                int index = _users.IndexOf(findUser);
                _users[index] = user;
            }
        }
    }
}
