using Microsoft.EntityFrameworkCore;
using uBlog.API.Data;
using uBlog.API.Models.Domain;
using uBlog.API.Repositories.Interface;

namespace uBlog.API.Repositories.Implementation 
{
    public class CategoryRepository : ICategoryRepository 
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Category> CreateAsync(Category category) 
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() 
        {
           return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id) 
        {
           return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category) 
        {
           var existingCategory =  await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

           if (existingCategory != null) {
                _dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
                await _dbContext.SaveChangesAsync();
                return category;
           }

            return null;
        }

        public async Task<Category?> DeleteAsnyc(Guid id) 
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null) return null;

            _dbContext.Categories.Remove(existingCategory);
            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
