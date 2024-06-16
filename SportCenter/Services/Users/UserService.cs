using SportCenter.Models.Entities;
using SportCenter.Repositories;

namespace SportCenter.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return userRepository.GetById(id);
        }

        public void AddUser(User user)
        {
            userRepository.Add(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        public void DeleteUser(User user)
        {
            userRepository.Delete(user);
        }
    }
}
