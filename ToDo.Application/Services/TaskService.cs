using ToDo.Application.Interfaces;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Domain.Enums;

namespace ToDo.Application.Services;

public class TaskService : ITaskService
{
    private readonly IGenericRepository<TaskItem> _taskRepository;

    public TaskService(IGenericRepository<TaskItem> taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(int userId)
    {
        var tasks = await _taskRepository.GetAllAsync();
        return tasks.Where(t => t.UserId == userId);
    }

    public async Task<IEnumerable<TaskItem>> GetFilteredAsync(
        int userId,
        TaskState? state,
        int? categoryId)
    {
        var tasks = await _taskRepository.GetAllAsync();

        var query = tasks.Where(t => t.UserId == userId);

        if (state.HasValue)
            query = query.Where(t => t.State == state.Value);

        if (categoryId.HasValue)
            query = query.Where(t => t.CategoryId == categoryId.Value);

        return query;
    }

    public async Task<TaskItem?> GetByIdAsync(int id, int userId)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        return task?.UserId == userId ? task : null;
    }

    public async Task AddAsync(TaskItem task)
    {
        await _taskRepository.AddAsync(task);
    }

    public async Task UpdateAsync(TaskItem task, int userId)
    {
        if (task.UserId != userId)
            throw new Exception("Yetkisiz işlem");

        await _taskRepository.UpdateAsync(task);
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var task = await GetByIdAsync(id, userId);
        if (task == null)
            throw new Exception("Task bulunamadı");

        await _taskRepository.DeleteAsync(task);
    }
}
