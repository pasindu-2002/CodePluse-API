using CodePluse.API.Models.Domain;

namespace CodePluse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
       Task<BlogPost> CreateAsync(BlogPost blogPost);

       Task<IEnumerable<BlogPost>> GetAllAsync();

       Task<BlogPost> GetByIdAsync(Guid id);

       Task<BlogPost?> UpdateAsync(BlogPost blogPost);

       Task<BlogPost?> DeleteAsync(Guid id);

        Task<BlogPost?> GetByUrlHandleAync(string urlHandle);
    }
}
