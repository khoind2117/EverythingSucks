using EverythingSucks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace EverythingSucks.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartStatus> CartStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrdersItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        #endregion

        #region FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>(entity =>
            {
                // Liên kết đến Cart 1-1
                entity.HasOne(u => u.Cart)
                    .WithOne(c => c.User)
                    .HasForeignKey<User>(u => u.CartId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);

                // Liên kết đến Order 
                entity.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Favorite (User - Product n-n)
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("Favorite").HasKey(f => f.Id);

                // Liên kết đến User
                entity.HasOne(f => f.User)
                    .WithMany(u => u.Favorites)
                    .HasForeignKey(f => f.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Liên kết đến Product
                entity.HasOne(f => f.Product)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(f => f.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product").HasKey(p => p.Id);

                entity.Property(p => p.Price)
                  .HasColumnType("decimal(18,2)");
            });

            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category").HasKey(c => c.Id);

                // Liên kết đến ProductType
                entity.HasMany(c => c.ProductTypes)
                    .WithOne(pt => pt.Category)
                    .HasForeignKey(pt => pt.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // ProductType
            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType").HasKey(pt => pt.Id);

                // Liên kết đến Product    
                entity.HasMany(pt => pt.Products)
                    .WithOne(p => p.ProductType)
                    .HasForeignKey(p => p.ProductTypeId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Color
            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color").HasKey (c => c.Id);
            });

            // ProductColor (Product - Color n-n)
            modelBuilder.Entity<ProductColor>(entity =>
            {
                entity.ToTable("ProdductColor").HasKey(pc => pc.Id);

                // Liên kết đến Product
                entity.HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductColors)
                    .HasForeignKey(pc => pc.ProductId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Liên kết đến Color
                entity.HasOne(pc => pc.Color)
                    .WithMany(c => c.ProductColors)
                    .HasForeignKey(pc => pc.ColorId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Liên kết đến CartItem
                entity.HasMany(pc => pc.CartItems)
                    .WithOne(ci => ci.ProductColor)
                    .HasForeignKey(pc => pc.ProductColorId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Liên kết đến OrderItem
                entity.HasMany(pc => pc.OrderItems)
                    .WithOne(oi => oi.ProductColor)
                    .HasForeignKey(pc => pc.ProductColorId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Liên kết đến ProductImage
                entity.HasMany(pc => pc.ProductImages)
                    .WithOne(pi => pi.ProductColor)
                    .HasForeignKey(pc => pc.ProductColorId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
            // ProductImage
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage").HasKey(pi => pi.Id);

            });

            // Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart").HasKey(c => c.Id);

                // Liên kết đến CartStatus
                entity.HasOne(c => c.CartStatus)
                    .WithMany(cs => cs.Carts)
                    .HasForeignKey(c => c.CartStatusId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // CartItem
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.ToTable("CartItem").HasKey(ci => ci.Id);

                // Liên kết đến Cart
                entity.HasOne(ci => ci.Cart)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(ci => ci.CartId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // CartStatus
            modelBuilder.Entity<CartStatus>(entity =>
            {
                entity.ToTable("CartStatus").HasKey(cs => cs.Id);
            });

            // Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order").HasKey(o => o.Id);

                entity.Property(o => o.TotalAmount)
                    .HasColumnType("decimal(18,2)");

                // Liên kết đến OrderStatus
                entity.HasOne(o => o.OrderStatus)
                    .WithMany(os => os.Orders)
                    .HasForeignKey(o => o.OrderStatusId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem").HasKey(oi => oi.Id);

                // Liên kết đến Order
                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // OrderStatus
            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus").HasKey(os => os.Id);
            });

            base.OnModelCreating(modelBuilder);

            // Khởi tạo dữ liệu cho IdentityRole
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            // Define static GUIDs for Categories
            var topsCategoryId = Guid.NewGuid();
            var bottomsCategoryId = Guid.NewGuid();
            var accessoriesCategoryId = Guid.NewGuid();

            // Khởi tạo dữ liệu cho Category
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = topsCategoryId, Name = "Áo" },
                new Category { Id = bottomsCategoryId, Name = "Quần" },
                new Category { Id = accessoriesCategoryId, Name = "Phụ kiện" }
            );

            // Khởi tạo dữ liệu cho Type
            modelBuilder.Entity<ProductType>().HasData(
                // Áo
                new ProductType { Id = Guid.NewGuid(), Name = "Áo thun", CategoryId = topsCategoryId },
                new ProductType { Id = Guid.NewGuid(), Name = "Áo polo", CategoryId = topsCategoryId },
                new ProductType { Id = Guid.NewGuid(), Name = "Áo sơ mi", CategoryId = topsCategoryId },
                // Quần
                new ProductType { Id = Guid.NewGuid(), Name = "Quần Short", CategoryId = bottomsCategoryId },
                new ProductType { Id = Guid.NewGuid(), Name = "Quần Jeans", CategoryId = bottomsCategoryId },
                new ProductType { Id = Guid.NewGuid(), Name = "Quần Tây", CategoryId = bottomsCategoryId },
                // Phụ kiện
                new ProductType { Id = Guid.NewGuid(), Name = "Mũ & Mũ lưỡi trai", CategoryId = accessoriesCategoryId },
                new ProductType { Id = Guid.NewGuid(), Name = "Kính mát", CategoryId = accessoriesCategoryId },
                new ProductType { Id = Guid.NewGuid(), Name = "Túi", CategoryId = accessoriesCategoryId }
            );

            //Khởi tạo dữ liệu cho Size
            modelBuilder.Entity<Size>().HasData(
                new Size { Id = Guid.NewGuid(), Name = "M" },
                new Size { Id = Guid.NewGuid(), Name = "L" },
                new Size { Id = Guid.NewGuid(), Name = "XL" },
                new Size { Id = Guid.NewGuid(), Name = "2XL" },
                new Size { Id = Guid.NewGuid(), Name = "3XL" }
            );

            // Khởi tạo dữ liệu cho Color
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = Guid.NewGuid(), Name = "White", ColorCode = "#FFFFFF" },
                new Color { Id = Guid.NewGuid(), Name = "Grey", ColorCode = "#DEDEDE" },
                new Color { Id = Guid.NewGuid(), Name = "Black", ColorCode = "#3D3D3D" },
                new Color { Id = Guid.NewGuid(), Name = "Pink", ColorCode = "#F5C0C9" },
                new Color { Id = Guid.NewGuid(), Name = "Red", ColorCode = "#EB3417" },
                new Color { Id = Guid.NewGuid(), Name = "Orange", ColorCode = "#F3A72C" },
                new Color { Id = Guid.NewGuid(), Name = "Beige", ColorCode = "#EFEBD4" },
                new Color { Id = Guid.NewGuid(), Name = "Brown", ColorCode = "#714E36" },
                new Color { Id = Guid.NewGuid(), Name = "Yellow", ColorCode = "#FFFF3F" },
                new Color { Id = Guid.NewGuid(), Name = "Green", ColorCode = "#387D1F" },
                new Color { Id = Guid.NewGuid(), Name = "Blue", ColorCode = "#0003F9" },
                new Color { Id = Guid.NewGuid(), Name = "Purple", ColorCode = "#741A7C" }
            );

            // Khởi tạo dữ liệu cho CartStatus
            modelBuilder.Entity<CartStatus>().HasData(
                new CartStatus { Id = Guid.NewGuid(), Name = "Có hàng" },
                new CartStatus { Id = Guid.NewGuid(), Name = "Trống" }
            );

            // Khởi tạo dữ liệu cho OrderStatus
            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus { Id = Guid.NewGuid(), Name = "Đang chờ xác nhận" },
                new OrderStatus { Id = Guid.NewGuid(), Name = "Xác nhận" },
                new OrderStatus { Id = Guid.NewGuid(), Name = "Chờ giao hàng" },
                new OrderStatus { Id = Guid.NewGuid(), Name = "Đã giao" },
                new OrderStatus { Id = Guid.NewGuid(), Name = "Đã hủy" }
            );
        }
        #endregion
    }
}
