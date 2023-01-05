using ES.Web.Models;
using ES.Web.Models.DAO;
using Microsoft.EntityFrameworkCore;

namespace ES.Web.Data;

public class ESDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderPart> OrderParts { get; set; }
    public DbSet<OrderMaterial> OrderMaterials { get; set; }

    public ESDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region USER

        var userEntity = builder.Entity<User>();

        // PRIMARY KEY
        userEntity.HasKey(nameof(User.ObjectID));

        // FirstName
        userEntity.Property(nameof(User.FirstName))
            .IsRequired(true)
            .HasMaxLength(127);
        // LastName
        userEntity.Property(nameof(User.LastName))
            .IsRequired(true)
            .HasMaxLength(127);
        // Age
        userEntity.Property(nameof(User.Age))
            .IsRequired(true)
            .HasMaxLength(100);

        #endregion USER

        #region MATERIAL

        var materialEntity = builder.Entity<Material>();

        // PRIMARY KEY
        materialEntity.HasKey(nameof(Material.ObjectID));

        materialEntity.Property(nameof(Material.ObjectID))
            .ValueGeneratedOnAdd();

        // Name
        materialEntity.Property(nameof(Material.Name))
            .IsRequired(true)
            .HasMaxLength(255);

        // CountStored
        materialEntity.Property(nameof(Material.CountStored))
            .IsRequired(true);

        // CountReserved
        materialEntity.Property(nameof(Material.CountReserved))
            .IsRequired(true)
            .HasDefaultValue(0);

        #endregion

        #region ORDER

        var orderEntity = builder.Entity<Order>();

        // PRIMARY KEY
        orderEntity.HasKey(nameof(Order.ObjectID));

        // Name
        orderEntity.Property(nameof(Order.Name))
            .IsRequired(true)
            .HasMaxLength(255);

        // RegDate
        orderEntity.Property(nameof(Order.RegDate))
            .IsRequired(true);

        // IsApproved
        orderEntity.Property(nameof(Order.IsApproved))
            .HasDefaultValue(false);
        // IsCompleted
        orderEntity.Property(nameof(Order.IsCompleted))
            .HasDefaultValue(false);
        // IsChecked
        orderEntity.Property(nameof(Order.IsChecked))
            .HasDefaultValue(false);

        // FOREIGN KEYS
        orderEntity
            .HasMany(nameof(Order.Parts))
            .WithOne(nameof(OrderPart.Order))
            .HasPrincipalKey(nameof(Order.ObjectID))
            .HasForeignKey(nameof(OrderPart.OrderID));

        #endregion ORDER

        #region ORDER_PART

        var orderPartEntity = builder.Entity<OrderPart>();

        // PRIMARY KEY
        orderPartEntity.HasKey(nameof(OrderPart.ObjectID));

        // EndDate
        orderPartEntity.Property(nameof(OrderPart.EndDate))
            .IsRequired(false);

        // IsCompleted
        //orderPartEntity.Property(nameof(OrderPart.IsCompleted))
        //    .HasComputedColumnSql("[EndDate] IS NULL");

        // FOREIGN KEYS
        orderPartEntity
            .HasOne(nameof(OrderPart.Order))
            .WithMany(nameof(Order.Parts))
            .HasForeignKey(nameof(OrderPart.OrderID))
            .HasPrincipalKey(nameof(Order.ObjectID));

        orderPartEntity
            .HasMany(nameof(OrderPart.Materials))
            .WithOne(nameof(OrderMaterial.Part))
            .HasPrincipalKey(nameof(OrderPart.ObjectID))
            .HasForeignKey(nameof(OrderMaterial.PartID));

        #endregion ORDER_PART

        #region ORDER_MATERIAL

        var orderMaterial = builder.Entity<OrderMaterial>();

        // PRIMARY KEY
        orderMaterial.HasKey(nameof(OrderMaterial.MaterialID), nameof(OrderMaterial.PartID));

        // Count
        orderMaterial.Property(nameof(OrderMaterial.Count))
            .HasDefaultValue(0);

        // FOREIGN KEYS
        orderMaterial
            .HasOne(nameof(OrderMaterial.Material));

        orderMaterial
            .HasOne(nameof(OrderMaterial.Part))
            .WithMany(nameof(OrderPart.Materials))
            .HasForeignKey(nameof(OrderMaterial.PartID))
            .HasPrincipalKey(nameof(OrderPart.ObjectID));

        #endregion


        #region DEFAULT_ENTITIES

        // Admin
        var adminUser = new User
        {
            ObjectID = Guid.NewGuid(),
            FirstName = "Админ",
            LastName = "",
            BirthDay = new DateTime(2000, 1, 1),
            Login = "admin",
            PasswordHash = "admin",
            Role = UserRole.ADMIN
        };

        userEntity.HasData(adminUser);

        // Materials
        var materials = new Material[]
        {
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 1",
                CountStored = 150,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 2",
                CountStored = 100,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 3",
                CountStored = 200,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 4",
                CountStored = 500,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 5",
                CountStored = 500,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 6",
                CountStored = 450,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 7",
                CountStored = 3100,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 8",
                CountStored = 25,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 9",
                CountStored = 2000,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 10",
                CountStored = 200,
                CountReserved = 0
            },
            new Material
            {
                ObjectID = Guid.NewGuid(),
                Name = "Материал 11",
                CountStored = 100,
                CountReserved = 0
            }
        };

        materialEntity.HasData(materials);

        #endregion DEFAULT_ENTITIES
    }

    private Order[] GenerateOrdersData(Material[] materials)
    {
        var order = new Order
        {
            ObjectID = Guid.NewGuid(),
            Name = "Производственный заказ 1",
            RegDate = DateTime.UtcNow,
            IsApproved = false,
            IsCompleted = false,
            IsChecked = false
        };

        var orderParts = new List<OrderPart>
        {
            new OrderPart
            {
                ObjectID = Guid.NewGuid(),
                OrderNum = 1,
                EndDate = null,
                IsCompleted = false,
                OrderID = order.ObjectID,
                Materials = new List<OrderMaterial>
                {
                    new OrderMaterial
                    {
                        MaterialID = materials[0].ObjectID,
                        Count = 10
                    },
                    new OrderMaterial
                    {
                        MaterialID = materials[1].ObjectID,
                        Count = 8
                    }
                }
            }
        };

        order.Parts = orderParts;
        return new Order[] { order };
    }
}
