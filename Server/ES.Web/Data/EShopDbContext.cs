using Microsoft.EntityFrameworkCore;

namespace PL.Web.Data;

public class EShopDbContext : DbContext
{

    public EShopDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

    }
}
