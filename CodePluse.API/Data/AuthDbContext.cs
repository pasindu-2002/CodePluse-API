using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePluse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "398c3ede-5ce2-421d-be27-4f339c7576b7";
            var writerRoleId = "3ee26313-3500-4f3b-8f3e-2581e14d9570";

            // Create Reader and Writer Role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id =  readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },

                new IdentityRole() 
                {
                    Id =  writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin user
            var adminUserId = "be21fce7-832e-42de-94c5-2899f3626c6c";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepluse.com",
                Email = "admin@codepluse.com",
                NormalizedEmail = "admin@codepluse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin@123");

            // Seed the admin
            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles To Admin
            var adminRole = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },

                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRole);
        }
    }
}
