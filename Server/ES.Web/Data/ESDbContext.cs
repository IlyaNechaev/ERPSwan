using ES.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ES.Web.Data;

public class ESDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ESDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var userEntity = builder.Entity<User>();

        userEntity.HasKey(nameof(User.ObjectID));
    }
}
