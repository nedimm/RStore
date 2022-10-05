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

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}
