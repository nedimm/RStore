using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RStore.Api.Data;

public class RStoreDbContext : IdentityDbContext<ApiUser>
{
    public RStoreDbContext()
    {
    }

    public RStoreDbContext(DbContextOptions<RStoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "F96BE8D1-4D0B-4377-88C7-14FEAC63A9AB"
            },
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "969C3A74-2EC8-4E6F-AA55-BF701ED43BCD"
            });

        var hasher = new PasswordHasher<ApiUser>();

        builder.Entity<ApiUser>().HasData(
            new ApiUser
            {
                Id = "07928237-EBFE-41EE-9017-8C3B691DCA08",
                Email = "admin@rstore.com",
                NormalizedEmail = "ADMIN@RSTORE.COM",
                UserName = "admin@rstore.com",
                NormalizedUserName = "ADMIN@RSTORE.COM",
                FirstName = "Admin",
                LastName = "System",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new ApiUser
            {
                Id = "197A6B96-01D9-4CA8-AF39-77B65DE60F90",
                Email = "user@rstore.com",
                NormalizedEmail = "USER@RSTORE.COM",
                UserName = "user@rstore.com",
                NormalizedUserName = "USER@RSTORE.COM",
                FirstName = "User",
                LastName = "System",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            }
            );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>()
            {
                RoleId = "F96BE8D1-4D0B-4377-88C7-14FEAC63A9AB",
                UserId = "197A6B96-01D9-4CA8-AF39-77B65DE60F90"
            },
            new IdentityUserRole<string>()
            {
                RoleId = "969C3A74-2EC8-4E6F-AA55-BF701ED43BCD",
                UserId = "07928237-EBFE-41EE-9017-8C3B691DCA08"
            }
        );
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}
