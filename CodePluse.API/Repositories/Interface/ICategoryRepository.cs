using CodePluse.API.Models.Domain;

namespace CodePluse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync(string? query = null);
        Task<Category?> GetById(Guid id);
        Task<Category?> UpadateAsync(Category category);
        Task<Category?>DeleteAsync(Guid id);
    }
}
