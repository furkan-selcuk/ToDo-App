using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetByUserNameAsync(string userName);
    Task AddAsync(User user);
}
