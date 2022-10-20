using ES.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace PL.Web.Data;

public class EShopDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public EShopDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var userEntity = builder.Entity<User>();

        userEntity.HasKey(nameof(User.ObjectID));
    }
}
