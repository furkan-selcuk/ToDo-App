using ToDo.Domain.Enums;
using ToDo.Domain.Entities;

namespace ToDo.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllAsync(int userId);
    Task<IEnumerable<TaskItem>> GetFilteredAsync(
        int userId,
        TaskState? state,
        int? categoryId
    );
    Task<TaskItem?> GetByIdAsync(int id, int userId);
    Task AddAsync(TaskItem task);
    Task UpdateAsync(TaskItem task, int userId);
    Task DeleteAsync(int id, int userId);
}
