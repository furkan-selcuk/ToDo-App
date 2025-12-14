using ToDo.Application.Interfaces;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Services;

public class UserService : IUserService
{
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        var users = await _userRepository.GetAllAsync();
        return users.FirstOrDefault(x => x.UserName == userName);
    }

    public async Task AddAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }
}
