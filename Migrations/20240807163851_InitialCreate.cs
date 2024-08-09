using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EverythingSucks.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_CartStatus_CartStatusId",
                        column: x => x.CartStatusId,
                        principalTable: "CartStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductType_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ProductTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_OrderStatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavoriteAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorite_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdductColor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdductColor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdductColor_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ProdductColor_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_CartId",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_ProdductColor_ProductColorId",
                        column: x => x.ProductColorId,
                        principalTable: "ProdductColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CartItem_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_ProdductColor_ProductColorId",
                        column: x => x.ProductColorId,
                        principalTable: "ProdductColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OrderItem_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    ProductColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImage_ProdductColor_ProductColorId",
                        column: x => x.ProductColorId,
                        principalTable: "ProdductColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5aaeea95-2f4f-4173-9549-5e290b8ec87b", "a3201d1b-56e4-4cc9-82a3-a6145418a7f2", "User", "USER" },
                    { "c733d5c0-70be-43cc-b029-0d0b3abd5fc5", "78eec985-6927-4d18-9d66-f601e1c73d21", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "32c944b9-669b-4bd7-ab4b-7a06829d9d84", 0, "123 Admin St", null, "1054e68f-0730-4300-a7f1-b5e29fafa0b2", "admin@gmail.com", true, "Admin", "EC", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEC65jIFVdnsAwyW+rRk80hpu5z+aPLWSovJ8maFatXXf8XyndiM+cdUvEub/ao5PQg==", null, false, "3efcb03d-7624-47c6-8b51-c759db125256", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CartStatusId", "UserId" },
                values: new object[] { new Guid("3a88704d-1fb1-41e9-b24b-0bb1ee011326"), null, "32c944b9-669b-4bd7-ab4b-7a06829d9d84" });

            migrationBuilder.InsertData(
                table: "CartStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("03ff6de0-5b56-4983-bc77-d6ad9aa3f754"), "Có hàng" },
                    { new Guid("2ad7231a-e37c-4c60-a6ff-11b4fac52394"), "Trống" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01af2e2f-f779-46ae-af72-64717834d068"), "Quần" },
                    { new Guid("3bf2fb8d-f7b5-409a-9eb8-1ae92a2e644d"), "Phụ kiện" },
                    { new Guid("8488ca15-ef5c-4e33-bffe-a5d1cb5b3946"), "Áo" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "ColorCode", "Name" },
                values: new object[,]
                {
                    { new Guid("055c1aa9-77ab-485c-9560-fc4860049b28"), "#3D3D3D", "Black" },
                    { new Guid("1e6bcca3-4c9a-4b98-99c1-58bceecfe0cc"), "#FFFFFF", "White" },
                    { new Guid("250afb4b-b7a7-4506-9285-c1c53fa3ddab"), "#DEDEDE", "Grey" },
                    { new Guid("37f88607-bd03-4ce7-9e07-b2a056d6a5be"), "#741A7C", "Purple" },
                    { new Guid("3df410ec-626c-4ffa-862b-4cc7046b944a"), "#F5C0C9", "Pink" },
                    { new Guid("4819410f-6be8-4275-a6d7-b59afbd0cd50"), "#387D1F", "Green" },
                    { new Guid("5c9896d1-4166-4b78-a624-d8dbd045b0e7"), "#EFEBD4", "Beige" },
                    { new Guid("78eff946-06d0-4a8e-bcbf-d710ddc44dad"), "#0003F9", "Blue" },
                    { new Guid("944f67e8-09d1-4e1a-ade1-e9956766e319"), "#EB3417", "Red" },
                    { new Guid("a4f85071-938b-42de-b93a-f5bb744e7932"), "#FFFF3F", "Yellow" },
                    { new Guid("b1d159f9-76f1-4dc9-9ff2-be47b7c45278"), "#714E36", "Brown" },
                    { new Guid("b53ebd36-d127-4758-b3ea-00296ee34f22"), "#F3A72C", "Orange" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("18238cc0-2df7-42ff-8e5f-3fb33505f08b"), "Đã hủy" },
                    { new Guid("4b70283d-5ce2-4c1f-b093-18e7122de806"), "Đã giao" },
                    { new Guid("4dcd64cc-8379-46e0-a32d-fd0e74a895ce"), "Đang chờ xác nhận" },
                    { new Guid("60fde9c1-2f33-4271-a2ba-88206ea622aa"), "Xác nhận" },
                    { new Guid("df387df4-52f9-45ea-a8e2-26764bb09fbf"), "Chờ giao hàng" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("13cf56e1-f54d-43ae-8a59-b41dfd710270"), "2XL" },
                    { new Guid("69522bcb-5754-46b8-b7ff-2d9a5997b7c6"), "3XL" },
                    { new Guid("a063ae38-bb8b-4187-96fc-6e988d9a4e1c"), "XL" },
                    { new Guid("c150a438-6f06-4617-aa0d-d91b1d6fc78e"), "L" },
                    { new Guid("fcf87b14-38e8-4440-b788-2fafbcccd215"), "M" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c733d5c0-70be-43cc-b029-0d0b3abd5fc5", "32c944b9-669b-4bd7-ab4b-7a06829d9d84" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("061a9d19-ed1b-42eb-aacb-7d8ca99466d7"), new Guid("01af2e2f-f779-46ae-af72-64717834d068"), "Quần Short" },
                    { new Guid("063e74e1-9654-4fbb-adc9-69371e7b611d"), new Guid("8488ca15-ef5c-4e33-bffe-a5d1cb5b3946"), "Áo sơ mi" },
                    { new Guid("440f4086-1e5f-4285-84b9-5d1e6e53db82"), new Guid("01af2e2f-f779-46ae-af72-64717834d068"), "Quần Tây" },
                    { new Guid("56a78937-cf4a-479c-b39e-3721696d6148"), new Guid("3bf2fb8d-f7b5-409a-9eb8-1ae92a2e644d"), "Mũ & Mũ lưỡi trai" },
                    { new Guid("9a8ff57c-59fc-4f23-838a-0c6a93e55c7a"), new Guid("01af2e2f-f779-46ae-af72-64717834d068"), "Quần Jeans" },
                    { new Guid("c162dcc8-8e6d-4cf4-b339-dbc33bcc8e0e"), new Guid("3bf2fb8d-f7b5-409a-9eb8-1ae92a2e644d"), "Kính mát" },
                    { new Guid("d21197a5-f6ba-40b7-980f-91a958844382"), new Guid("3bf2fb8d-f7b5-409a-9eb8-1ae92a2e644d"), "Túi" },
                    { new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new Guid("8488ca15-ef5c-4e33-bffe-a5d1cb5b3946"), "Áo thun" },
                    { new Guid("e64cd78d-a9f7-4acd-b8eb-e125b800989e"), new Guid("8488ca15-ef5c-4e33-bffe-a5d1cb5b3946"), "Áo polo" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Price", "ProductTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("08039caf-5a2d-43ec-8ad1-d66388053434"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4877), "100% cotton SUPIMA® cao cấp, mịn màng, thiết kế cơ bản phù hợp tạo kiểu với nhiều phong cách khác nhau từ đơn giản đến layer, được thiết kế tỉ mỉ đến từng chi tiết, từ chiều rộng cổ áo đến đường may.", false, "Áo Thun Supima Cotton Cổ Tròn Ngắn Tay", 391000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4878) },
                    { new Guid("12439ae3-a55e-4050-bf5d-efe7dbb4be6c"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4872), "Chất liệu 100% cotton bền chắc, cổ áo được làm bằng vải mềm, thiết kế giản dị lấy cảm hứng từ áo bóng bầu dục cổ điển.", false, "Áo Thun Vải Cotton Cổ Henley Ngắn Tay", 489000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4872) },
                    { new Guid("33daddd3-18a8-4189-9bd6-1c8c630549ae"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4884), "Một phiên bản mới của chiếc áo thun cổ tròn vải waffle nay đã có mặt, được thiết kế với kiểu dáng đơn giản, không có túi ở phần trước ngực, cải tiến với đường may thẳng cùng kiểu dáng xẻ tà, dễ dàng mặc cho mọi dịp.", false, "Áo Thun Vải Waffle Dài Tay", 391000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4884) },
                    { new Guid("45ac6e73-c401-4b7c-bf24-3a53388ee3f8"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4887), "Chất vải 'AIRism' mịn màng trông như cotton, Với công nghệ DRY khô nhanh, chất liệu vải sắc nét tạo nên kiểu dáng đẹp mắt.", false, "AIRism Cotton Áo Thun Không Tay", 293000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4888) },
                    { new Guid("76884c0d-4eee-4afd-a1e5-c6310dfcfb40"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4855), "Bộ sưu tập U từ thương hiệu Uniqlo là kết tinh sáng tạo của đội ngũ thiết kế quốc tế tận tâm và tài năng đến từ Trung tâm Nghiên cứu và Phát triển Paris, dưới sự dẫn dắt của Giám đốc Nghệ thuật Christophe Lemaire.", false, "AIRism Cotton Áo Thun Dáng Rộng Tay Lỡ", 391000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4867) },
                    { new Guid("83005e52-71cd-422e-ab5b-5129b30d16cd"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4889), "Phần thân được làm từ chất liệu 100% cotton cực dày dặn, cảm giác khá sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Ringer)", 293000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4890) },
                    { new Guid("877d7d6e-e9b8-4eea-99fd-85c63ff0d2ca"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4886), "Vải 'AIRism' mịn màng trông như cotton, cổ tròn hẹp mang lại vẻ ngoài bóng bẩy, vai trễ và tay áo dài đến một nửa rộng rãi, chất liệu vải tạo dáng tôn dáng.", false, "AIRism Cotton Áo Thun Dáng Rộng", 391000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4886) },
                    { new Guid("916fca40-92eb-4e8e-b377-f1170316b32e"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4880), "Cảm giác giản dị của cotton, lớp lót polyester Với công nghệ DRY khô nhanh, thiết kế cơ bản mà bạn có thể tự tạo phong cách riêng hoặc theo Kiểu layer.", false, "Áo Thun Dry Cổ Tròn Nhiều Màu", 146000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4880) },
                    { new Guid("93d3bb8f-befc-4ffa-a324-80e2a15e45ea"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4870), "Vải 'DRY-EX' nhanh chóng hấp thụ và hút ẩm để giữ cho làn da của bạn cảm giác tươi mát.", false, "DRY-EX Áo Thun Cổ Tròn", 391000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4870) },
                    { new Guid("ad86c66a-93e0-432a-9a18-12b1491af89b"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4891), "Chất liệu 100% cotton dày dặn hoàn hảo, cảm giác sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Raglan)", 293000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4891) },
                    { new Guid("bb59c0b0-ed31-42d9-a40c-0f95e08efb8f"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4874), "Chất liệu 100% cotton cực dày dặn, cảm giác sắc nét, mịn màng, được giặt trước một lần nước để có phong cách hoàn toàn giản dị.", false, "Áo Thun Dáng Rộng Kẻ Sọc Cổ Tròn Tay Lỡ", 391000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4874) },
                    { new Guid("daab0e88-09c2-47fe-8c2b-8ffcfe6b7563"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4882), "Chất liệu vải jersey 100% cotton dày dặn, mang lại cảm giác tươi mát, vải jersey khô được dệt nhỏ gọn có độ bền cao và có đặc tính sau mỗi lần giặt, buộc dây ở cổ áo giúp giữ nguyên kiểu dáng đường viền cổ áo.", false, "Áo Thun Cổ Tròn Ngắn Tay", 293000m, new Guid("e15365db-2023-4745-9d7b-c91e40017a9a"), new DateTime(2024, 8, 7, 23, 38, 49, 765, DateTimeKind.Local).AddTicks(4882) }
                });

            migrationBuilder.InsertData(
                table: "ProdductColor",
                columns: new[] { "Id", "ColorId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("0d7bafff-9962-414e-85d8-9c80221d3e85"), new Guid("78eff946-06d0-4a8e-bcbf-d710ddc44dad"), new Guid("83005e52-71cd-422e-ab5b-5129b30d16cd") },
                    { new Guid("155fafd4-541b-4716-ac34-609553e1e07a"), new Guid("b1d159f9-76f1-4dc9-9ff2-be47b7c45278"), new Guid("916fca40-92eb-4e8e-b377-f1170316b32e") },
                    { new Guid("1db07e2d-dc3a-4b77-8625-9701d71d6b22"), new Guid("78eff946-06d0-4a8e-bcbf-d710ddc44dad"), new Guid("877d7d6e-e9b8-4eea-99fd-85c63ff0d2ca") },
                    { new Guid("27f6cbcc-b625-4de7-8b5a-1016551ecdb5"), new Guid("4819410f-6be8-4275-a6d7-b59afbd0cd50"), new Guid("12439ae3-a55e-4050-bf5d-efe7dbb4be6c") },
                    { new Guid("3e50ab98-3dbf-4fcd-9963-c1fa59a8d782"), new Guid("944f67e8-09d1-4e1a-ade1-e9956766e319"), new Guid("08039caf-5a2d-43ec-8ad1-d66388053434") },
                    { new Guid("52afd94c-134f-40eb-b199-b8be04285dc1"), new Guid("b1d159f9-76f1-4dc9-9ff2-be47b7c45278"), new Guid("877d7d6e-e9b8-4eea-99fd-85c63ff0d2ca") },
                    { new Guid("5300edec-f24d-43c9-98ac-ece6f8162972"), new Guid("055c1aa9-77ab-485c-9560-fc4860049b28"), new Guid("33daddd3-18a8-4189-9bd6-1c8c630549ae") },
                    { new Guid("61203d6a-654f-4f84-a193-8ac16fc14d46"), new Guid("250afb4b-b7a7-4506-9285-c1c53fa3ddab"), new Guid("08039caf-5a2d-43ec-8ad1-d66388053434") },
                    { new Guid("619021b8-532e-4a1b-87f7-38571fcd269a"), new Guid("4819410f-6be8-4275-a6d7-b59afbd0cd50"), new Guid("ad86c66a-93e0-432a-9a18-12b1491af89b") },
                    { new Guid("6b82f7f9-0614-4352-a900-41887b213bc3"), new Guid("1e6bcca3-4c9a-4b98-99c1-58bceecfe0cc"), new Guid("83005e52-71cd-422e-ab5b-5129b30d16cd") },
                    { new Guid("6ef5bc54-127b-4c64-9eeb-bda6e98efee0"), new Guid("5c9896d1-4166-4b78-a624-d8dbd045b0e7"), new Guid("daab0e88-09c2-47fe-8c2b-8ffcfe6b7563") },
                    { new Guid("813ed004-2277-4c73-8bd5-ea1e273cee62"), new Guid("1e6bcca3-4c9a-4b98-99c1-58bceecfe0cc"), new Guid("76884c0d-4eee-4afd-a1e5-c6310dfcfb40") },
                    { new Guid("88cf17db-ff65-4ca1-9aff-edc7943abddf"), new Guid("250afb4b-b7a7-4506-9285-c1c53fa3ddab"), new Guid("daab0e88-09c2-47fe-8c2b-8ffcfe6b7563") },
                    { new Guid("8aa01376-6057-4ecb-ae2a-5be214ddcae6"), new Guid("4819410f-6be8-4275-a6d7-b59afbd0cd50"), new Guid("916fca40-92eb-4e8e-b377-f1170316b32e") },
                    { new Guid("91427255-9f79-4eb9-b77b-eefdcebfff77"), new Guid("3df410ec-626c-4ffa-862b-4cc7046b944a"), new Guid("bb59c0b0-ed31-42d9-a40c-0f95e08efb8f") },
                    { new Guid("98b00074-701a-4e71-9a9f-f5350e9edaea"), new Guid("78eff946-06d0-4a8e-bcbf-d710ddc44dad"), new Guid("93d3bb8f-befc-4ffa-a324-80e2a15e45ea") },
                    { new Guid("99b85730-83bd-4700-8b7b-0f7cb010cbf0"), new Guid("055c1aa9-77ab-485c-9560-fc4860049b28"), new Guid("76884c0d-4eee-4afd-a1e5-c6310dfcfb40") },
                    { new Guid("9fcd53b2-464d-4614-be17-c8191337f54f"), new Guid("78eff946-06d0-4a8e-bcbf-d710ddc44dad"), new Guid("bb59c0b0-ed31-42d9-a40c-0f95e08efb8f") },
                    { new Guid("ac4b09da-bf4c-42f4-824f-bc16e81aa413"), new Guid("5c9896d1-4166-4b78-a624-d8dbd045b0e7"), new Guid("ad86c66a-93e0-432a-9a18-12b1491af89b") },
                    { new Guid("b116a223-432a-4aff-bd15-9390abb7bfbc"), new Guid("250afb4b-b7a7-4506-9285-c1c53fa3ddab"), new Guid("45ac6e73-c401-4b7c-bf24-3a53388ee3f8") },
                    { new Guid("c391e26b-aaee-4038-9aa4-358c27ffcd8a"), new Guid("78eff946-06d0-4a8e-bcbf-d710ddc44dad"), new Guid("33daddd3-18a8-4189-9bd6-1c8c630549ae") },
                    { new Guid("ce36bfdc-94d3-4464-9235-a232311cccfc"), new Guid("055c1aa9-77ab-485c-9560-fc4860049b28"), new Guid("93d3bb8f-befc-4ffa-a324-80e2a15e45ea") },
                    { new Guid("d0c3fa8a-8c14-4e81-ad69-5273d8dc57bb"), new Guid("1e6bcca3-4c9a-4b98-99c1-58bceecfe0cc"), new Guid("45ac6e73-c401-4b7c-bf24-3a53388ee3f8") },
                    { new Guid("d29b9213-3169-4b2d-8bbf-52834e4f3ca4"), new Guid("055c1aa9-77ab-485c-9560-fc4860049b28"), new Guid("12439ae3-a55e-4050-bf5d-efe7dbb4be6c") }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "IsPrimary", "ProductColorId", "Url" },
                values: new object[,]
                {
                    { new Guid("0105a9c6-f7db-46ce-a643-1c5da8990a81"), false, new Guid("619021b8-532e-4a1b-87f7-38571fcd269a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440460/olive_2_q9e65h.jpg" },
                    { new Guid("04fee30f-f915-4556-9ec7-603fc9165ed3"), false, new Guid("98b00074-701a-4e71-9a9f-f5350e9edaea"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_2_pvuo6o.jpg" },
                    { new Guid("0502fca7-b25b-4844-a10d-df7d9a7d19f5"), true, new Guid("c391e26b-aaee-4038-9aa4-358c27ffcd8a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440195/blue_1_hwruph.jpg" },
                    { new Guid("05635881-8cab-4281-a848-66d76adb4cdc"), false, new Guid("1db07e2d-dc3a-4b77-8625-9701d71d6b22"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/navy_2_sybyag.jpg" },
                    { new Guid("09792728-30fc-43de-8589-87948bdf0dc1"), false, new Guid("813ed004-2277-4c73-8bd5-ea1e273cee62"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438077/white_2_oicoau.jpg" },
                    { new Guid("0ee22d01-e51a-4b1f-a39b-5cdfb83ef504"), true, new Guid("6ef5bc54-127b-4c64-9eeb-bda6e98efee0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_1_zmlakv.jpg" },
                    { new Guid("10f2a7b1-d360-4762-bf6c-2fd68896b754"), false, new Guid("6b82f7f9-0614-4352-a900-41887b213bc3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440406/white_2_mbd76x.jpg" },
                    { new Guid("19c3276c-f8fe-4a58-9e2d-f482fa4f4d4b"), true, new Guid("52afd94c-134f-40eb-b199-b8be04285dc1"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/brown_1_gmlsf5.jpg" },
                    { new Guid("1a6a7a18-1489-4865-8354-539c983876d7"), true, new Guid("5300edec-f24d-43c9-98ac-ece6f8162972"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440193/black_1_mvzenu.jpg" },
                    { new Guid("1af7ad50-76d2-4d18-b007-79316b4e1ba0"), true, new Guid("88cf17db-ff65-4ca1-9aff-edc7943abddf"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/light-gray_1_uyfeh7.jpg" },
                    { new Guid("1cf2f018-0a6f-4b3f-8abf-325261cb0413"), false, new Guid("88cf17db-ff65-4ca1-9aff-edc7943abddf"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440135/light-gray_2_hpszuf.jpg" },
                    { new Guid("1e58c4cc-a69a-4c8a-a7aa-0effc9db9ebd"), true, new Guid("3e50ab98-3dbf-4fcd-9963-c1fa59a8d782"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_1_zht2re.jpg" },
                    { new Guid("1eb36129-fada-498b-9c0a-14fa1864a24b"), false, new Guid("155fafd4-541b-4716-ac34-609553e1e07a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_2_vj3zqp.jpg" },
                    { new Guid("1f2b380c-b7ca-4841-96d7-e73aef6786bd"), true, new Guid("99b85730-83bd-4700-8b7b-0f7cb010cbf0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_1_gkqv9b.jpg" },
                    { new Guid("239aa394-f40e-45d0-bad3-bcc0526ff0de"), true, new Guid("b116a223-432a-4aff-bd15-9390abb7bfbc"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440315/gray_1_eurozu.jpg" },
                    { new Guid("2929f829-a674-4e6b-a0ee-1ed7acdb24fd"), false, new Guid("d29b9213-3169-4b2d-8bbf-52834e4f3ca4"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/black_2_xkoq54.jpg" },
                    { new Guid("2b3d60e9-5c29-4e94-b0df-e403c58e78e5"), true, new Guid("ce36bfdc-94d3-4464-9235-a232311cccfc"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_1_r9smbn.jpg" },
                    { new Guid("38bdf142-0f63-4c9c-87e7-28c4881fb775"), true, new Guid("155fafd4-541b-4716-ac34-609553e1e07a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_1_h2bauh.jpg" },
                    { new Guid("3a20cab8-866d-4538-86f1-84eb4e395ebd"), true, new Guid("8aa01376-6057-4ecb-ae2a-5be214ddcae6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/green_1_pwrndf.jpg" },
                    { new Guid("3d7b1f3f-6c50-44df-9232-dee9d90663d8"), true, new Guid("91427255-9f79-4eb9-b77b-eefdcebfff77"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_1_v0tcxo.jpg" },
                    { new Guid("42aa4729-64c2-4511-b4e1-53edf8924983"), true, new Guid("6b82f7f9-0614-4352-a900-41887b213bc3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440403/white_1_nd5suu.jpg" },
                    { new Guid("42b4d277-f7c5-4152-a568-79ec56fed1af"), false, new Guid("99b85730-83bd-4700-8b7b-0f7cb010cbf0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_2_sx4vva.jpg" },
                    { new Guid("49ac97f5-295b-43dd-94e1-f735f0c658e8"), true, new Guid("d0c3fa8a-8c14-4e81-ad69-5273d8dc57bb"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440317/white_1_lppqmp.jpg" },
                    { new Guid("49ee70b6-a427-40dd-bcaf-eee51ba5d4a3"), true, new Guid("98b00074-701a-4e71-9a9f-f5350e9edaea"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_1_eaakhx.jpg" },
                    { new Guid("644fe614-8318-44b3-912f-17dcb2445bdb"), true, new Guid("27f6cbcc-b625-4de7-8b5a-1016551ecdb5"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/olive_1_faf0az.jpg" },
                    { new Guid("670cca2e-7644-46d7-b70f-6df80a07b7cf"), false, new Guid("0d7bafff-9962-414e-85d8-9c80221d3e85"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440402/navy_2_vgb74l.jpg" },
                    { new Guid("6d68fd7b-d97f-46c2-8919-3712cf9c68b0"), false, new Guid("6ef5bc54-127b-4c64-9eeb-bda6e98efee0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_2_dfpfn5.jpg" },
                    { new Guid("6dc50e23-b0f3-4a11-a8cb-383cf0c2e278"), false, new Guid("9fcd53b2-464d-4614-be17-c8191337f54f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/navy_2_kolxaz.jpg" },
                    { new Guid("6ed86342-6c6e-4ce9-93d6-4d25f22da2d2"), true, new Guid("1db07e2d-dc3a-4b77-8625-9701d71d6b22"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440252/navy_1_aewdgs.jpg" },
                    { new Guid("841e635d-cbb8-4a75-98e5-c6ac25b1959c"), true, new Guid("619021b8-532e-4a1b-87f7-38571fcd269a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440458/olive_1_uixjsd.jpg" },
                    { new Guid("8a660c39-a005-4fa5-8038-f4efbe816c5a"), false, new Guid("8aa01376-6057-4ecb-ae2a-5be214ddcae6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439960/green_2_kf2ll7.jpg" },
                    { new Guid("8d33de52-09d0-46d2-9ec1-1c465f3b674a"), false, new Guid("ac4b09da-bf4c-42f4-824f-bc16e81aa413"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_2_xf8iqr.jpg" },
                    { new Guid("911dce3e-dd79-4693-b22e-6fa98eac2ff0"), false, new Guid("52afd94c-134f-40eb-b199-b8be04285dc1"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440251/brown_2_zqkgfj.jpg" },
                    { new Guid("91adb552-3850-4bab-94d1-fd193d48edc3"), false, new Guid("3e50ab98-3dbf-4fcd-9963-c1fa59a8d782"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_2_oohqly.jpg" },
                    { new Guid("a3494c7c-57ef-4d1b-a13a-687379d8c3f4"), false, new Guid("d0c3fa8a-8c14-4e81-ad69-5273d8dc57bb"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440318/white_2_opuzjo.jpg" },
                    { new Guid("a574ed50-0219-4318-a235-15f1f5eabcbf"), true, new Guid("813ed004-2277-4c73-8bd5-ea1e273cee62"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438076/white_1_t5dag8.jpg" },
                    { new Guid("bc684a31-a317-407a-9234-df1ae3f1a0db"), false, new Guid("ce36bfdc-94d3-4464-9235-a232311cccfc"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_2_rhdpsy.jpg" },
                    { new Guid("cae8ba9f-2517-425d-92ec-f2acdd9e7297"), false, new Guid("61203d6a-654f-4f84-a193-8ac16fc14d46"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/gray_2_bzzytk.jpg" },
                    { new Guid("cc6baa04-80de-4544-8299-bdd86936b559"), true, new Guid("ac4b09da-bf4c-42f4-824f-bc16e81aa413"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_1_xaltqx.jpg" },
                    { new Guid("d12325cf-55ce-4421-8331-92b6be644860"), true, new Guid("9fcd53b2-464d-4614-be17-c8191337f54f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439524/navy_1_kkhyqa.jpg" },
                    { new Guid("d79051b7-f46f-4908-8c5d-7f916e18f0a5"), false, new Guid("5300edec-f24d-43c9-98ac-ece6f8162972"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440194/black_2_yptvzh.jpg" },
                    { new Guid("d9efba2f-bad6-42ba-8720-ff4c7b8effc8"), true, new Guid("61203d6a-654f-4f84-a193-8ac16fc14d46"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439897/gray_1_lzp7go.jpg" },
                    { new Guid("db9f6ead-3892-414c-8636-789f05515301"), true, new Guid("0d7bafff-9962-414e-85d8-9c80221d3e85"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440401/navy_1_vrk8fm.jpg" },
                    { new Guid("e33ba492-8488-4426-9261-5ee01e8f875b"), false, new Guid("27f6cbcc-b625-4de7-8b5a-1016551ecdb5"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439222/olive_2_ovtcev.jpg" },
                    { new Guid("e35f770e-67d2-4cc2-964d-44b05a54194f"), false, new Guid("c391e26b-aaee-4038-9aa4-358c27ffcd8a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440196/blue_2_ioyjmd.jpg" },
                    { new Guid("ec0df6e8-33cd-4586-bf76-397acb8b7c17"), true, new Guid("d29b9213-3169-4b2d-8bbf-52834e4f3ca4"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439223/black_1_lbqxjg.jpg" },
                    { new Guid("fd3db68e-1372-4222-896f-fca78e5d4058"), false, new Guid("b116a223-432a-4aff-bd15-9390abb7bfbc"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440316/gray_2_gif5gy.jpg" },
                    { new Guid("fdfb14bb-9825-46cb-a711-5a72f30394f4"), false, new Guid("91427255-9f79-4eb9-b77b-eefdcebfff77"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_2_ygzmiw.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers",
                column: "CartId",
                unique: true,
                filter: "[CartId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_CartStatusId",
                table: "Cart",
                column: "CartStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ProductColorId",
                table: "CartItem",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_SizeId",
                table: "CartItem",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ProductId",
                table: "Favorite",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId",
                table: "Favorite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatusId",
                table: "Order",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductColorId",
                table: "OrderItem",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_SizeId",
                table: "OrderItem",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdductColor_ColorId",
                table: "ProdductColor",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdductColor_ProductId",
                table: "ProdductColor",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductTypeId",
                table: "Product",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductColorId",
                table: "ProductImage",
                column: "ProductColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_CategoryId",
                table: "ProductType",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "ProdductColor");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "CartStatus");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
