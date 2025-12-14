using ToDo.Application.Interfaces;
using ToDo.DataAccess.Repositories.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Application.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
    }

    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.ToList();
        }
    }
}
