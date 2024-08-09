using EverythingSucks.Helpers;
using EverythingSucks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region FluentAPI

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

            #endregion

            base.OnModelCreating(modelBuilder);

            #region Sample Data
            // Khởi tạo dữ liệu cho IdentityRole
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            // Khởi tạo dữ liệu cho Admin
            var hasher = new PasswordHasher<User>();

            var adminUserId = Guid.NewGuid().ToString();

            var adminUser = new User
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "EC",
                Address = "123 Admin St",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // Thêm người dùng quản trị viên vào bảng AspNetUsers
            modelBuilder.Entity<User>().HasData(adminUser);

            // Gán vai trò quản trị viên cho người dùng quản trị viên
            var adminUserRole = new IdentityUserRole<string>
            {
                UserId = adminUserId,
                RoleId = adminRoleId
            };

            // Thêm vai trò người dùng vào bảng AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);

            var adminCart = Guid.NewGuid();
            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = adminCart, UserId = adminUserId , CartStatusId = null}    
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

            // Define static GUIDs for ProductType(aoThun)
            var aoThunId = Guid.NewGuid();

            // Khởi tạo dữ liệu cho Type
            modelBuilder.Entity<ProductType>().HasData(
                // Áo
                new ProductType { Id = aoThunId, Name = "Áo thun", CategoryId = topsCategoryId },
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

            // Define static GUIDs for Color
            var whiteId = Guid.NewGuid();
            var greyId = Guid.NewGuid();
            var blackId = Guid.NewGuid();
            var pinkId = Guid.NewGuid();
            var redId = Guid.NewGuid();
            var orangeId = Guid.NewGuid();
            var beigeId = Guid.NewGuid();
            var brownId = Guid.NewGuid();
            var yellowId = Guid.NewGuid();
            var greenId = Guid.NewGuid();
            var blueId = Guid.NewGuid();
            var purpleId = Guid.NewGuid();

            // Khởi tạo dữ liệu cho Color
            modelBuilder.Entity<Color>().HasData(
                new Color { Id = whiteId, Name = "White", ColorCode = "#FFFFFF" },
                new Color { Id = greyId, Name = "Grey", ColorCode = "#DEDEDE" },
                new Color { Id = blackId, Name = "Black", ColorCode = "#3D3D3D" },
                new Color { Id = pinkId, Name = "Pink", ColorCode = "#F5C0C9" },
                new Color { Id = redId, Name = "Red", ColorCode = "#EB3417" },
                new Color { Id = orangeId, Name = "Orange", ColorCode = "#F3A72C" },
                new Color { Id = beigeId, Name = "Beige", ColorCode = "#EFEBD4" },
                new Color { Id = brownId, Name = "Brown", ColorCode = "#714E36" },
                new Color { Id = yellowId, Name = "Yellow", ColorCode = "#FFFF3F" },
                new Color { Id = greenId, Name = "Green", ColorCode = "#387D1F" },
                new Color { Id = blueId, Name = "Blue", ColorCode = "#0003F9" },
                new Color { Id = purpleId, Name = "Purple", ColorCode = "#741A7C" }
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

            // Define static GUIDs for Product
            var product_1_Id = Guid.NewGuid();
            var product_2_Id = Guid.NewGuid();
            var product_3_Id = Guid.NewGuid();
            var product_4_Id = Guid.NewGuid();
            var product_5_Id = Guid.NewGuid();
            var product_6_Id = Guid.NewGuid();
            var product_7_Id = Guid.NewGuid();
            var product_8_Id = Guid.NewGuid();
            var product_9_Id = Guid.NewGuid();
            var product_10_Id = Guid.NewGuid();
            var product_11_Id = Guid.NewGuid();
            var product_12_Id = Guid.NewGuid();

            // Khởi tạo dữ liệu cho Product
            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    Id = product_1_Id,
                    Name = "AIRism Cotton Áo Thun Dáng Rộng Tay Lỡ",
                    Description = "Bộ sưu tập U từ thương hiệu Uniqlo là kết tinh sáng tạo của đội ngũ thiết kế quốc tế tận tâm và tài năng đến từ Trung tâm Nghiên cứu và Phát triển Paris, dưới sự dẫn dắt của Giám đốc Nghệ thuật Christophe Lemaire.",
                    Price = 391000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "airism-cotton-ao-thun-dang-rong-tay-lo",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_2_Id,
                    Name = "DRY-EX Áo Thun Cổ Tròn",
                    Description = "Vải 'DRY-EX' nhanh chóng hấp thụ và hút ẩm để giữ cho làn da của bạn cảm giác tươi mát.",
                    Price = 391000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "dry-ex-ao-thun-co-tron",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_3_Id,
                    Name = "Áo Thun Vải Cotton Cổ Henley Ngắn Tay",
                    Description = "Chất liệu 100% cotton bền chắc, cổ áo được làm bằng vải mềm, thiết kế giản dị lấy cảm hứng từ áo bóng bầu dục cổ điển.",
                    Price = 489000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-vai-cotton-co-henley-ngan-tay",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_4_Id,
                    Name = "Áo Thun Dáng Rộng Kẻ Sọc Cổ Tròn Tay Lỡ",
                    Description = "Chất liệu 100% cotton cực dày dặn, cảm giác sắc nét, mịn màng, được giặt trước một lần nước để có phong cách hoàn toàn giản dị.",
                    Price = 391000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-dang-rong-ke-soc-co-tron-tay-lo",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_5_Id,
                    Name = "Áo Thun Supima Cotton Cổ Tròn Ngắn Tay",
                    Description = "100% cotton SUPIMA® cao cấp, mịn màng, thiết kế cơ bản phù hợp tạo kiểu với nhiều phong cách khác nhau từ đơn giản đến layer, được thiết kế tỉ mỉ đến từng chi tiết, từ chiều rộng cổ áo đến đường may.",
                    Price = 391000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-supima-cotton-co-tron-ngan-tay",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_6_Id,
                    Name = "Áo Thun Dry Cổ Tròn Nhiều Màu",
                    Description = "Cảm giác giản dị của cotton, lớp lót polyester Với công nghệ DRY khô nhanh, thiết kế cơ bản mà bạn có thể tự tạo phong cách riêng hoặc theo Kiểu layer.",
                    Price = 146000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-dry-co-tron-nhieu-mau",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_7_Id,
                    Name = "Áo Thun Cổ Tròn Ngắn Tay",
                    Description = "Chất liệu vải jersey 100% cotton dày dặn, mang lại cảm giác tươi mát, vải jersey khô được dệt nhỏ gọn có độ bền cao và có đặc tính sau mỗi lần giặt, buộc dây ở cổ áo giúp giữ nguyên kiểu dáng đường viền cổ áo.",
                    Price = 293000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-co-tron-ngan-tay",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_8_Id,
                    Name = "Áo Thun Vải Waffle Dài Tay",
                    Description = "Một phiên bản mới của chiếc áo thun cổ tròn vải waffle nay đã có mặt, được thiết kế với kiểu dáng đơn giản, không có túi ở phần trước ngực, cải tiến với đường may thẳng cùng kiểu dáng xẻ tà, dễ dàng mặc cho mọi dịp.",
                    Price = 391000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-vai-waffle-dai-tay",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_9_Id,
                    Name = "AIRism Cotton Áo Thun Dáng Rộng",
                    Description = "Vải 'AIRism' mịn màng trông như cotton, cổ tròn hẹp mang lại vẻ ngoài bóng bẩy, vai trễ và tay áo dài đến một nửa rộng rãi, chất liệu vải tạo dáng tôn dáng.",
                    Price = 391000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "airism-cotton-ao-thun-dang-rong",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_10_Id,
                    Name = "AIRism Cotton Áo Thun Không Tay",
                    Description = "Chất vải 'AIRism' mịn màng trông như cotton, Với công nghệ DRY khô nhanh, chất liệu vải sắc nét tạo nên kiểu dáng đẹp mắt.",
                    Price = 293000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "airism-cotton-ao-thun-khong-tay",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_11_Id,
                    Name = "Áo Thun Dáng Rộng Tay Lỡ (Ringer)",
                    Description = "Phần thân được làm từ chất liệu 100% cotton cực dày dặn, cảm giác khá sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.",
                    Price = 293000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-dang-rong-tay-lo-ringer",
                    ProductTypeId = aoThunId
                },
                new Product
                {
                    Id = product_12_Id,
                    Name = "Áo Thun Dáng Rộng Tay Lỡ (Raglan)",
                    Description = "Chất liệu 100% cotton dày dặn hoàn hảo, cảm giác sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.",
                    Price = 293000,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false,
                    Slug = "ao-thun-dang-rong-tay-lo-raglan",
                    ProductTypeId = aoThunId
                }
            );

            // Define static GUIDs for ProductColor
            var product_1_color_1_Id = Guid.NewGuid();
            var product_1_color_2_Id = Guid.NewGuid();
            var product_2_color_1_Id = Guid.NewGuid();
            var product_2_color_2_Id = Guid.NewGuid();
            var product_3_color_1_Id = Guid.NewGuid();
            var product_3_color_2_Id = Guid.NewGuid();
            var product_4_color_1_Id = Guid.NewGuid();
            var product_4_color_2_Id = Guid.NewGuid();
            var product_5_color_1_Id = Guid.NewGuid();
            var product_5_color_2_Id = Guid.NewGuid();
            var product_6_color_1_Id = Guid.NewGuid();
            var product_6_color_2_Id = Guid.NewGuid();
            var product_7_color_1_Id = Guid.NewGuid();
            var product_7_color_2_Id = Guid.NewGuid();
            var product_8_color_1_Id = Guid.NewGuid();
            var product_8_color_2_Id = Guid.NewGuid();
            var product_9_color_1_Id = Guid.NewGuid();
            var product_9_color_2_Id = Guid.NewGuid();
            var product_10_color_1_Id = Guid.NewGuid();
            var product_10_color_2_Id = Guid.NewGuid();
            var product_11_color_1_Id = Guid.NewGuid();
            var product_11_color_2_Id = Guid.NewGuid();
            var product_12_color_1_Id = Guid.NewGuid();
            var product_12_color_2_Id = Guid.NewGuid();


            // Khởi tạo dữ liệu cho ProductColor
            modelBuilder.Entity<ProductColor>().HasData(
                // product_1
                new ProductColor { Id = product_1_color_1_Id, ProductId = product_1_Id, ColorId = blackId },
                new ProductColor { Id = product_1_color_2_Id, ProductId = product_1_Id, ColorId = whiteId },
                // product_2
                new ProductColor { Id = product_2_color_1_Id, ProductId = product_2_Id, ColorId = blueId },
                new ProductColor { Id = product_2_color_2_Id, ProductId = product_2_Id, ColorId = blackId },
                // product_3
                new ProductColor { Id = product_3_color_1_Id, ProductId = product_3_Id, ColorId = blackId },
                new ProductColor { Id = product_3_color_2_Id, ProductId = product_3_Id, ColorId = greenId },
                // product_4
                new ProductColor { Id = product_4_color_1_Id, ProductId = product_4_Id, ColorId = pinkId },
                new ProductColor { Id = product_4_color_2_Id, ProductId = product_4_Id, ColorId = blueId },
                // product_5
                new ProductColor { Id = product_5_color_1_Id, ProductId = product_5_Id, ColorId = greyId },
                new ProductColor { Id = product_5_color_2_Id, ProductId = product_5_Id, ColorId = redId },
                // product_6
                new ProductColor { Id = product_6_color_1_Id, ProductId = product_6_Id, ColorId = brownId },
                new ProductColor { Id = product_6_color_2_Id, ProductId = product_6_Id, ColorId = greenId },
                // product_7
                new ProductColor { Id = product_7_color_1_Id, ProductId = product_7_Id, ColorId = greyId },
                new ProductColor { Id = product_7_color_2_Id, ProductId = product_7_Id, ColorId = beigeId },
                // product_8
                new ProductColor { Id = product_8_color_1_Id, ProductId = product_8_Id, ColorId = blueId },
                new ProductColor { Id = product_8_color_2_Id, ProductId = product_8_Id, ColorId = blackId },
                // product_9
                new ProductColor { Id = product_9_color_1_Id, ProductId = product_9_Id, ColorId = brownId },
                new ProductColor { Id = product_9_color_2_Id, ProductId = product_9_Id, ColorId = blueId },
                // product_10
                new ProductColor { Id = product_10_color_1_Id, ProductId = product_10_Id, ColorId = whiteId },
                new ProductColor { Id = product_10_color_2_Id, ProductId = product_10_Id, ColorId = greyId },
                // product_11
                new ProductColor { Id = product_11_color_1_Id, ProductId = product_11_Id, ColorId = whiteId },
                new ProductColor { Id = product_11_color_2_Id, ProductId = product_11_Id, ColorId = blueId },
                // product_12
                new ProductColor { Id = product_12_color_1_Id, ProductId = product_12_Id, ColorId = greenId },
                new ProductColor { Id = product_12_color_2_Id, ProductId = product_12_Id, ColorId = beigeId }
            );

            // Khởi tạo dữ liệu cho ProductImages
            modelBuilder.Entity<ProductImage>().HasData(
                // product_1
                new ProductImage { 
                    Id = Guid.NewGuid(), 
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_1_gkqv9b.jpg",
                    IsPrimary = true,
                    ProductColorId = product_1_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_2_sx4vva.jpg",
                    IsPrimary = false,
                    ProductColorId = product_1_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438076/white_1_t5dag8.jpg",
                    IsPrimary = true,
                    ProductColorId = product_1_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438077/white_2_oicoau.jpg",
                    IsPrimary = false,
                    ProductColorId = product_1_color_2_Id,
                },
                // product_2
                new ProductImage
                 {
                     Id = Guid.NewGuid(),
                     Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_1_eaakhx.jpg",
                     IsPrimary = true,
                     ProductColorId = product_2_color_1_Id,
                 },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_2_pvuo6o.jpg",
                    IsPrimary = false,
                    ProductColorId = product_2_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_1_r9smbn.jpg",
                    IsPrimary = true,
                    ProductColorId = product_2_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_2_rhdpsy.jpg",
                    IsPrimary = false,
                    ProductColorId = product_2_color_2_Id,
                },
                // product_3
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439223/black_1_lbqxjg.jpg",
                    IsPrimary = true,
                    ProductColorId = product_3_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/black_2_xkoq54.jpg",
                    IsPrimary = false,
                    ProductColorId = product_3_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/olive_1_faf0az.jpg",
                    IsPrimary = true,
                    ProductColorId = product_3_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439222/olive_2_ovtcev.jpg",
                    IsPrimary = false,
                    ProductColorId = product_3_color_2_Id,
                },
                // product_4
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_1_v0tcxo.jpg",
                    IsPrimary = true,
                    ProductColorId = product_4_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_2_ygzmiw.jpg",
                    IsPrimary = false,
                    ProductColorId = product_4_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439524/navy_1_kkhyqa.jpg",
                    IsPrimary = true,
                    ProductColorId = product_4_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/navy_2_kolxaz.jpg",
                    IsPrimary = false,
                    ProductColorId = product_4_color_2_Id,
                },
                // product_5
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439897/gray_1_lzp7go.jpg",
                    IsPrimary = true,
                    ProductColorId = product_5_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/gray_2_bzzytk.jpg",
                    IsPrimary = false,
                    ProductColorId = product_5_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_1_zht2re.jpg",
                    IsPrimary = true,
                    ProductColorId = product_5_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_2_oohqly.jpg",
                    IsPrimary = false,
                    ProductColorId = product_5_color_2_Id,
                },
                 // product_6
                 new ProductImage
                 {
                     Id = Guid.NewGuid(),
                     Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_1_h2bauh.jpg",
                     IsPrimary = true,
                     ProductColorId = product_6_color_1_Id,
                 },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_2_vj3zqp.jpg",
                    IsPrimary = false,
                    ProductColorId = product_6_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/green_1_pwrndf.jpg",
                    IsPrimary = true,
                    ProductColorId = product_6_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439960/green_2_kf2ll7.jpg",
                    IsPrimary = false,
                    ProductColorId = product_6_color_2_Id,
                },
                // product_7
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/light-gray_1_uyfeh7.jpg",
                    IsPrimary = true,
                    ProductColorId = product_7_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440135/light-gray_2_hpszuf.jpg",
                    IsPrimary = false,
                    ProductColorId = product_7_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_1_zmlakv.jpg",
                    IsPrimary = true,
                    ProductColorId = product_7_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_2_dfpfn5.jpg",
                    IsPrimary = false,
                    ProductColorId = product_7_color_2_Id,
                },
                // product_8
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440195/blue_1_hwruph.jpg",
                    IsPrimary = true,
                    ProductColorId = product_8_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440196/blue_2_ioyjmd.jpg",
                    IsPrimary = false,
                    ProductColorId = product_8_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440193/black_1_mvzenu.jpg",
                    IsPrimary = true,
                    ProductColorId = product_8_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440194/black_2_yptvzh.jpg",
                    IsPrimary = false,
                    ProductColorId = product_8_color_2_Id,
                },
                // product_9
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/brown_1_gmlsf5.jpg",
                    IsPrimary = true,
                    ProductColorId = product_9_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440251/brown_2_zqkgfj.jpg",
                    IsPrimary = false,
                    ProductColorId = product_9_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440252/navy_1_aewdgs.jpg",
                    IsPrimary = true,
                    ProductColorId = product_9_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/navy_2_sybyag.jpg",
                    IsPrimary = false,
                    ProductColorId = product_9_color_2_Id,
                },
                // product_10
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440317/white_1_lppqmp.jpg",
                    IsPrimary = true,
                    ProductColorId = product_10_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440318/white_2_opuzjo.jpg",
                    IsPrimary = false,
                    ProductColorId = product_10_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440315/gray_1_eurozu.jpg",
                    IsPrimary = true,
                    ProductColorId = product_10_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440316/gray_2_gif5gy.jpg",
                    IsPrimary = false,
                    ProductColorId = product_10_color_2_Id,
                },
                // product_11
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440403/white_1_nd5suu.jpg",
                    IsPrimary = true,
                    ProductColorId = product_11_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440406/white_2_mbd76x.jpg",
                    IsPrimary = false,
                    ProductColorId = product_11_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440401/navy_1_vrk8fm.jpg",
                    IsPrimary = true,
                    ProductColorId = product_11_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440402/navy_2_vgb74l.jpg",
                    IsPrimary = false,
                    ProductColorId = product_11_color_2_Id,
                },
                // product_12
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440458/olive_1_uixjsd.jpg",
                    IsPrimary = true,
                    ProductColorId = product_12_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440460/olive_2_q9e65h.jpg",
                    IsPrimary = false,
                    ProductColorId = product_12_color_1_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_1_xaltqx.jpg",
                    IsPrimary = true,
                    ProductColorId = product_12_color_2_Id,
                },
                new ProductImage
                {
                    Id = Guid.NewGuid(),
                    Url = "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_2_xf8iqr.jpg",
                    IsPrimary = false,
                    ProductColorId = product_12_color_2_Id,
                }
            );

            #endregion
        }
    }
}
