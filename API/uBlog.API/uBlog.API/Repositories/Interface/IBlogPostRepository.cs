using uBlog.API.Models.Domain;

namespace uBlog.API.Repositories.Interface
{
    public interface IBlogPostRepository 
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);

        Task<IEnumerable<BlogPost>> GetAllAsync();

        Task<BlogPost?> GetByIdAsync(Guid id);

        Task<BlogPost?> UpdateAsync(BlogPost blogPost);

        Task<BlogPost?> DeleteAsnyc(Guid id);

        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

    }
}
