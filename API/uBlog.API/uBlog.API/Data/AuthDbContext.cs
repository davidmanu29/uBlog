using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace uBlog.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "48cda5f9-31c9-4275-929e-f89ec9a2cfb2";
            var writerRoleId = "6497aecd-f54f-4c9f-83a7-937e55c5f1eb";

            //create reader/writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },

                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles); // seeds data when migration runs inside the table

            //create admin user
            var adminUserId = "9b9407d5-0554-4539-81eb-28eb19ea4527";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@ublog.com",
                Email = "admin@ublog.com",
                NormalizedEmail = "admin@ublog.com".ToUpper(),
                NormalizedUserName = "admin@ublog.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@10");

            builder.Entity<IdentityUser>().HasData(admin);

            // give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
