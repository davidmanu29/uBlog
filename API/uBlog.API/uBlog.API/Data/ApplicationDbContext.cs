using Microsoft.EntityFrameworkCore;
using uBlog.API.Models.Domain;

namespace uBlog.API.Data {
    public class ApplicationDbContext : DbContext {

        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) {


        }

        
    }
}
