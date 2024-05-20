using Microsoft.EntityFrameworkCore;
using uBlog.API.Models.Domain;

namespace uBlog.API.Data 
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<BlogImage> BlogImages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {


        }
    }
}
