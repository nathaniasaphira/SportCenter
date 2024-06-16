using SportCenter.Models.Entities;

namespace SportCenter.Services.Users
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
