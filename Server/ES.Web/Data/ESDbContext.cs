using ES.Web.Models;
using ES.Web.Models.DAO;
using ES.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace ES.Web.Data;

public class ESDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderPart> OrderParts { get; set; }
    public DbSet<OrderMaterial> OrderMaterials { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookEntry> BookEntries { get; set; }

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

        orderEntity
            .HasOne(nameof(Order.Foreman))
            .WithMany()
            .HasForeignKey(nameof(Order.ForemanID));

        orderEntity
            .HasMany(nameof(Order.Books))
            .WithOne(nameof(Book.Order))
            .HasPrincipalKey(nameof(Order.ObjectID))
            .HasForeignKey(nameof(Book.OrderID));

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

        #region BOOK

        var bookEntity = builder.Entity<Book>();

        bookEntity.HasKey(nameof(Book.ObjectID));

        // FOREIGN KEYS
        bookEntity
            .HasOne(nameof(Book.DebetEntry))
            .WithMany()
            .HasForeignKey(nameof(Book.DebetID))
            .HasPrincipalKey(nameof(BookEntry.ObjectID));

        bookEntity
            .HasOne(nameof(Book.CreditEntry))
            .WithMany()
            .HasForeignKey(nameof(Book.CreditID))
            .HasPrincipalKey(nameof(BookEntry.ObjectID));

        bookEntity
            .HasOne(nameof(Book.Order))
            .WithMany(nameof(Order.Books))
            .HasForeignKey(nameof(Book.OrderID))
            .HasPrincipalKey(nameof(Order.ObjectID));

        #endregion

        #region BOOK_ENTRY

        var bookEntryEntity = builder.Entity<BookEntry>();

        bookEntryEntity.HasKey(nameof(BookEntry.ObjectID));

        var entries = new BookEntry[]
        {
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "01",
                Name = "Основные средства"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "02",
                Name = "Амортизация основных средств"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "10",
                Name = "Материалы"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "20",
                Name = "Основное производство"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "43",
                Name = "Готовая продукция"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "69",
                Name = "Расчеты по социальному страхованию и обеспечению"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "70",
                Name = "Расчеты с персоналом по оплате труда"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "80",
                Name = "Уставный капитал"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "84",
                Name = "Нераспределенная прибыль"
            },
            new()
            {
                ObjectID = Guid.NewGuid(),
                Code = "99",
                Name = "Прибыли и убытки"
            }
        };

        bookEntryEntity.HasData(entries);

        #endregion


        #region DEFAULT_ENTITIES

        // Admin
        var adminUser = new User
        {
            ObjectID = new Guid("f4f6c79f-b232-4fdf-b666-40ea657fe07a"),
            FirstName = "Админ",
            LastName = "Админ",
            BirthDay = new DateTime(1997, 9, 4),
            Login = "admin",
            PasswordHash = new SecurityService().Encrypt("admin"),
            Role = UserRole.ADMIN
        };

        userEntity.HasData(adminUser);

        var defaultUsers = new List<User>
        {
            new User
            {
                ObjectID = new Guid("ad845b18-0b55-4d42-aab1-ae14c1e99b92"),
                FirstName = "Анна",
                LastName = "Малинова",
                BirthDay = new DateTime(1991, 5, 17),
                Login = "anna",
                PasswordHash = new SecurityService().Encrypt("123"),
                Role = UserRole.BOOKER
            },
            new User
            {
                ObjectID = new Guid("8ac4d193-acb3-448e-a6f6-c3c77092c12f"),
                FirstName = "Сергей",
                LastName = "Васильев",
                BirthDay = new DateTime(1985, 12, 4),
                Login = "sergey",
                PasswordHash = new SecurityService().Encrypt("123"),
                Role = UserRole.FOREMAN
            },
            new User
            {
                ObjectID = new Guid("cdef1e1a-6af8-4754-a5a7-a308ab4b19a8"),
                FirstName = "Иван",
                LastName = "Петров",
                BirthDay = new DateTime(1990, 6, 23),
                Login = "ivan",
                PasswordHash = new SecurityService().Encrypt("123"),
                Role = UserRole.WORKER
            },
        };

        userEntity.HasData(defaultUsers);

        var random = new Random(12);
        // Materials
        var materials = new Material[]
        {
            new Material
            {
                ObjectID = new Guid("aa628991-1113-486a-b705-3d54f1019c0d"),
                Name = "Материал 1",
                CountStored = 150,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("509b21ba-1331-4c06-afd8-5781161e2296"),
                Name = "Материал 2",
                CountStored = 100,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("2f43fb0b-af30-4805-bf78-3d92e9bec871"),
                Name = "Материал 3",
                CountStored = 200,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("92118cdc-1275-45d9-9774-5c3a27ac6db8"),
                Name = "Материал 4",
                CountStored = 500,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("c8b54ef6-45fb-4865-9042-6cd34aaabf54"),
                Name = "Материал 5",
                CountStored = 500,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("853a3600-df09-4709-85e5-42332adb21a9"),
                Name = "Материал 6",
                CountStored = 450,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("e7e87fcb-e048-403b-830d-487f0f4c0471"),
                Name = "Материал 7",
                CountStored = 3100,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("c17570a6-b141-4507-aea3-ce16acf5ce70"),
                Name = "Материал 8",
                CountStored = 25,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("4cfda7ac-5daf-45b8-9a2b-238d6319ecf4"),
                Name = "Материал 9",
                CountStored = 2000,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("b55319e7-afb4-4235-97df-a00cd0f391e3"),
                Name = "Материал 10",
                CountStored = 200,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
            },
            new Material
            {
                ObjectID = new Guid("ade2a3bc-2cf0-4902-8367-fef07f31e147"),
                Name = "Материал 11",
                CountStored = 100,
                CountReserved = 0,
                Code = random.Next(),
                Price = 20,
                Units = MeasureUnits.PIECE
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
            Number = 1,
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
