using SportCenter.Models.Entities;
using SportCenter.Repositories;

namespace SportCenter.Services.Auth;

public class UserService(IRepository<User> userRepository) : IUserService
{
    private readonly IRepository<User> _userRepository = userRepository;

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAll();
    }

    public User GetUserById(int id)
    {
        return _userRepository.GetById(id);
    }

    public void AddUser(User user)
    {
        _userRepository.Add(user);
    }

    public void UpdateUser(User user)
    {
        _userRepository.Update(user);
    }

    public void DeleteUser(User user)
    {
        _userRepository.Delete(user);
    }
}
