using uBlog.API.Models.Domain;

namespace uBlog.API.Repositories.Interface {
    public interface ICategoryRepository {

        Task<Category> CreateAsync(Category category);
    }
}
