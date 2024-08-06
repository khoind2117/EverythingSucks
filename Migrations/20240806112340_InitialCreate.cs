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
                    { "5899a36f-fcda-4d08-b066-a04886342c5f", "59677ee9-a88c-47be-98ec-9628baacd5eb", "User", "USER" },
                    { "5eb26716-96a4-4e9a-b650-7cb2e01970bc", "7c75ea8d-e299-45a6-a09a-2bf5dbb0abb9", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "CartStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("98200fc6-b563-48e9-af8c-c67b80e2367d"), "Có hàng" },
                    { new Guid("eef14b1c-2294-41bf-be59-fffb424c74eb"), "Trống" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("285369b6-5e13-4616-bb0d-56fb332983ad"), "Quần" },
                    { new Guid("9fab6c9b-3c48-48e1-8949-bd4292345a3a"), "Phụ kiện" },
                    { new Guid("d3260ef1-c6cf-48e0-8509-0b2727c087da"), "Áo" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "ColorCode", "Name" },
                values: new object[,]
                {
                    { new Guid("25448a6e-feb4-48d0-89d6-b502c5a178bb"), "#3D3D3D", "Black" },
                    { new Guid("3f0ef44b-52a8-4a9a-9386-f965deb167e7"), "#714E36", "Brown" },
                    { new Guid("46927bde-ec41-454a-9152-665d300763bd"), "#DEDEDE", "Grey" },
                    { new Guid("4e3980a0-0cb3-481b-96f2-aae4c510a02b"), "#0003F9", "Blue" },
                    { new Guid("544b59b8-87a6-4012-9e98-de73e00e9392"), "#EB3417", "Red" },
                    { new Guid("5c74e337-4a2b-4cc3-adc3-5c81f7318b36"), "#F3A72C", "Orange" },
                    { new Guid("7c42837d-8ad5-474d-868d-6f2a78044988"), "#EFEBD4", "Beige" },
                    { new Guid("977eab2d-607a-4997-9e5e-a10129a7eacd"), "#741A7C", "Purple" },
                    { new Guid("9c4f690c-7a1d-4708-b5ed-406fd8ab2e9e"), "#FFFFFF", "White" },
                    { new Guid("e022901e-e46e-4878-8f2b-056ded26a836"), "#FFFF3F", "Yellow" },
                    { new Guid("e141a55c-760a-4df9-be2c-1866f5b004b4"), "#387D1F", "Green" },
                    { new Guid("e25a0527-09ca-4fa8-920f-94024310e219"), "#F5C0C9", "Pink" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("1aed6d8a-1721-4e5b-8d09-053154c13650"), "Đang chờ xác nhận" },
                    { new Guid("51ebd24b-4d55-461f-8c92-b7fed9f5bcd7"), "Đã hủy" },
                    { new Guid("911272f3-18d8-4c17-ba66-20288c9fdf69"), "Đã giao" },
                    { new Guid("95ddaff4-9c55-447e-abe8-e6ea199aae26"), "Chờ giao hàng" },
                    { new Guid("c03f1386-51b6-44e5-b047-2588cb684e84"), "Xác nhận" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3bbaddb9-e9db-41a0-9aae-309e38c8d214"), "2XL" },
                    { new Guid("4d51a06f-7d4d-4bc1-bd98-cfedb606be87"), "XL" },
                    { new Guid("695e1398-1a3c-46e8-89ac-963795537954"), "M" },
                    { new Guid("b9d27ee1-9ce5-4c5a-b375-5d3cfad5850d"), "L" },
                    { new Guid("deee9f2f-3043-412c-b989-c5cdf5f02ef1"), "3XL" }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("3d5317bb-edb9-4736-940c-636812baa056"), new Guid("9fab6c9b-3c48-48e1-8949-bd4292345a3a"), "Túi" },
                    { new Guid("3fd2cd2b-4a5c-4c37-867d-563871dba2c7"), new Guid("285369b6-5e13-4616-bb0d-56fb332983ad"), "Quần Jeans" },
                    { new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new Guid("d3260ef1-c6cf-48e0-8509-0b2727c087da"), "Áo thun" },
                    { new Guid("64a31316-586a-4159-97c1-e08b26fe175c"), new Guid("9fab6c9b-3c48-48e1-8949-bd4292345a3a"), "Mũ & Mũ lưỡi trai" },
                    { new Guid("6580de69-ed75-4267-a9e7-d6a24a7f93f1"), new Guid("d3260ef1-c6cf-48e0-8509-0b2727c087da"), "Áo polo" },
                    { new Guid("db2bcdfe-2f99-4a08-9d1c-d70a1e865e78"), new Guid("285369b6-5e13-4616-bb0d-56fb332983ad"), "Quần Short" },
                    { new Guid("e19bd92c-eb3e-428b-a893-61b7b2eb75ab"), new Guid("d3260ef1-c6cf-48e0-8509-0b2727c087da"), "Áo sơ mi" },
                    { new Guid("f63a1f11-5309-4440-b4c4-34c5aa3741ae"), new Guid("9fab6c9b-3c48-48e1-8949-bd4292345a3a"), "Kính mát" },
                    { new Guid("f9fa04eb-012c-4053-b56c-f6af4f73faa6"), new Guid("285369b6-5e13-4616-bb0d-56fb332983ad"), "Quần Tây" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Price", "ProductTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0136a45a-768f-4546-a483-60d44b38b648"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(48), "Chất liệu 100% cotton dày dặn hoàn hảo, cảm giác sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Raglan)", 293000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(49) },
                    { new Guid("02205dcc-2c22-44eb-8ec7-1576103f6d78"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(46), "Phần thân được làm từ chất liệu 100% cotton cực dày dặn, cảm giác khá sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Ringer)", 293000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(47) },
                    { new Guid("2bfc2c03-5884-4e96-b09c-067941e18d81"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(38), "Chất liệu vải jersey 100% cotton dày dặn, mang lại cảm giác tươi mát, vải jersey khô được dệt nhỏ gọn có độ bền cao và có đặc tính sau mỗi lần giặt, buộc dây ở cổ áo giúp giữ nguyên kiểu dáng đường viền cổ áo.", false, "Áo Thun Cổ Tròn Ngắn Tay", 293000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(38) },
                    { new Guid("2dfc153d-5447-4fc7-ad0e-2a199a56ab08"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9995), "Vải 'DRY-EX' nhanh chóng hấp thụ và hút ẩm để giữ cho làn da của bạn cảm giác tươi mát.", false, "DRY-EX Áo Thun Cổ Tròn", 391000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9995) },
                    { new Guid("66316ad8-ed0b-4109-b139-f3f1da4301e1"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(1), "100% cotton SUPIMA® cao cấp, mịn màng, thiết kế cơ bản phù hợp tạo kiểu với nhiều phong cách khác nhau từ đơn giản đến layer, được thiết kế tỉ mỉ đến từng chi tiết, từ chiều rộng cổ áo đến đường may.", false, "Áo Thun Supima Cotton Cổ Tròn Ngắn Tay", 391000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(1) },
                    { new Guid("7e5c4190-b0b6-4264-b5c6-427c83ba4ff3"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(4), "Cảm giác giản dị của cotton, lớp lót polyester Với công nghệ DRY khô nhanh, thiết kế cơ bản mà bạn có thể tự tạo phong cách riêng hoặc theo Kiểu layer.", false, "Áo Thun Dry Cổ Tròn Nhiều Màu", 146000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(5) },
                    { new Guid("87df6479-5775-435b-864d-ad74e0e6d51a"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(40), "Một phiên bản mới của chiếc áo thun cổ tròn vải waffle nay đã có mặt, được thiết kế với kiểu dáng đơn giản, không có túi ở phần trước ngực, cải tiến với đường may thẳng cùng kiểu dáng xẻ tà, dễ dàng mặc cho mọi dịp.", false, "Áo Thun Vải Waffle Dài Tay", 391000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(40) },
                    { new Guid("929583cc-576b-4da9-b41a-01c7f1857c5d"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9983), "Bộ sưu tập U từ thương hiệu Uniqlo là kết tinh sáng tạo của đội ngũ thiết kế quốc tế tận tâm và tài năng đến từ Trung tâm Nghiên cứu và Phát triển Paris, dưới sự dẫn dắt của Giám đốc Nghệ thuật Christophe Lemaire.", false, "AIRism Cotton Áo Thun Dáng Rộng Tay Lỡ", 391000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9992) },
                    { new Guid("a697939c-867e-477a-bce1-692bbc60cfa8"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9999), "Chất liệu 100% cotton cực dày dặn, cảm giác sắc nét, mịn màng, được giặt trước một lần nước để có phong cách hoàn toàn giản dị.", false, "Áo Thun Dáng Rộng Kẻ Sọc Cổ Tròn Tay Lỡ", 391000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9999) },
                    { new Guid("b743125d-d91d-4f0e-a10d-9603939f7dea"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(42), "Vải 'AIRism' mịn màng trông như cotton, cổ tròn hẹp mang lại vẻ ngoài bóng bẩy, vai trễ và tay áo dài đến một nửa rộng rãi, chất liệu vải tạo dáng tôn dáng.", false, "AIRism Cotton Áo Thun Dáng Rộng", 391000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(42) },
                    { new Guid("ee3e8c93-3e09-4c3b-b992-3028446f4a96"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(44), "Chất vải 'AIRism' mịn màng trông như cotton, Với công nghệ DRY khô nhanh, chất liệu vải sắc nét tạo nên kiểu dáng đẹp mắt.", false, "AIRism Cotton Áo Thun Không Tay", 293000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 793, DateTimeKind.Local).AddTicks(45) },
                    { new Guid("fa4e89b0-b89a-4f8c-a2a4-c1a521341ce4"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9997), "Chất liệu 100% cotton bền chắc, cổ áo được làm bằng vải mềm, thiết kế giản dị lấy cảm hứng từ áo bóng bầu dục cổ điển.", false, "Áo Thun Vải Cotton Cổ Henley Ngắn Tay", 489000m, new Guid("4b07d583-b0bc-421b-a317-f6b7afdd16ec"), new DateTime(2024, 8, 6, 18, 23, 39, 792, DateTimeKind.Local).AddTicks(9997) }
                });

            migrationBuilder.InsertData(
                table: "ProdductColor",
                columns: new[] { "Id", "ColorId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("08f4b2b8-3892-459d-8b62-fc78201df9c6"), new Guid("9c4f690c-7a1d-4708-b5ed-406fd8ab2e9e"), new Guid("02205dcc-2c22-44eb-8ec7-1576103f6d78") },
                    { new Guid("156faef1-45cb-40bb-87d1-f0fb4a714fd3"), new Guid("46927bde-ec41-454a-9152-665d300763bd"), new Guid("2bfc2c03-5884-4e96-b09c-067941e18d81") },
                    { new Guid("236e1e24-61d6-497c-b8d3-9d345aff253a"), new Guid("7c42837d-8ad5-474d-868d-6f2a78044988"), new Guid("0136a45a-768f-4546-a483-60d44b38b648") },
                    { new Guid("42666ce5-a7cb-4cc4-b131-9e8d5d8d0108"), new Guid("7c42837d-8ad5-474d-868d-6f2a78044988"), new Guid("2bfc2c03-5884-4e96-b09c-067941e18d81") },
                    { new Guid("4e3f6713-8537-4901-9912-1b8e60370647"), new Guid("46927bde-ec41-454a-9152-665d300763bd"), new Guid("ee3e8c93-3e09-4c3b-b992-3028446f4a96") },
                    { new Guid("5467d69e-5137-4235-8c02-5909ed6ebdc3"), new Guid("25448a6e-feb4-48d0-89d6-b502c5a178bb"), new Guid("87df6479-5775-435b-864d-ad74e0e6d51a") },
                    { new Guid("5b142a1f-52e4-482b-b286-ace90b0e370e"), new Guid("544b59b8-87a6-4012-9e98-de73e00e9392"), new Guid("66316ad8-ed0b-4109-b139-f3f1da4301e1") },
                    { new Guid("72248adb-202b-4d42-970c-929012f09f92"), new Guid("25448a6e-feb4-48d0-89d6-b502c5a178bb"), new Guid("2dfc153d-5447-4fc7-ad0e-2a199a56ab08") },
                    { new Guid("74c30efb-2929-490c-adf6-db5ea0eb5724"), new Guid("4e3980a0-0cb3-481b-96f2-aae4c510a02b"), new Guid("b743125d-d91d-4f0e-a10d-9603939f7dea") },
                    { new Guid("74e406b8-92de-4ef5-9fce-c42806af61fe"), new Guid("4e3980a0-0cb3-481b-96f2-aae4c510a02b"), new Guid("02205dcc-2c22-44eb-8ec7-1576103f6d78") },
                    { new Guid("826c33f3-4392-42a5-b98b-6f7d85bcbc49"), new Guid("e141a55c-760a-4df9-be2c-1866f5b004b4"), new Guid("fa4e89b0-b89a-4f8c-a2a4-c1a521341ce4") },
                    { new Guid("8b2ddc46-d8b4-4f25-a081-4807204740d0"), new Guid("9c4f690c-7a1d-4708-b5ed-406fd8ab2e9e"), new Guid("ee3e8c93-3e09-4c3b-b992-3028446f4a96") },
                    { new Guid("8d3cc25f-0c8e-4e37-9570-2e3deb215d45"), new Guid("25448a6e-feb4-48d0-89d6-b502c5a178bb"), new Guid("fa4e89b0-b89a-4f8c-a2a4-c1a521341ce4") },
                    { new Guid("9667cdca-9f10-4b4a-adf2-b9fa91341bf6"), new Guid("4e3980a0-0cb3-481b-96f2-aae4c510a02b"), new Guid("87df6479-5775-435b-864d-ad74e0e6d51a") },
                    { new Guid("9f416d19-921e-4a5a-bbc3-c07a0fe622e8"), new Guid("e141a55c-760a-4df9-be2c-1866f5b004b4"), new Guid("0136a45a-768f-4546-a483-60d44b38b648") },
                    { new Guid("ab1782df-ea23-4f45-9d90-2b42c267a40d"), new Guid("3f0ef44b-52a8-4a9a-9386-f965deb167e7"), new Guid("7e5c4190-b0b6-4264-b5c6-427c83ba4ff3") },
                    { new Guid("b12213db-f580-422a-b301-e35ad569d07f"), new Guid("e25a0527-09ca-4fa8-920f-94024310e219"), new Guid("a697939c-867e-477a-bce1-692bbc60cfa8") },
                    { new Guid("bae4694d-1401-4ea4-a640-8004c5ffd7d5"), new Guid("e141a55c-760a-4df9-be2c-1866f5b004b4"), new Guid("7e5c4190-b0b6-4264-b5c6-427c83ba4ff3") },
                    { new Guid("bb98d5f4-ce56-4abd-b6d6-a70dec58f6e0"), new Guid("4e3980a0-0cb3-481b-96f2-aae4c510a02b"), new Guid("a697939c-867e-477a-bce1-692bbc60cfa8") },
                    { new Guid("c617ba9d-6b83-4ef6-b8c6-ccca5688aa6d"), new Guid("4e3980a0-0cb3-481b-96f2-aae4c510a02b"), new Guid("2dfc153d-5447-4fc7-ad0e-2a199a56ab08") },
                    { new Guid("c6d43721-cec3-4820-85ac-f6f8bd9e9fac"), new Guid("3f0ef44b-52a8-4a9a-9386-f965deb167e7"), new Guid("b743125d-d91d-4f0e-a10d-9603939f7dea") },
                    { new Guid("d9cbb083-26b5-496e-98a6-fce3be357293"), new Guid("9c4f690c-7a1d-4708-b5ed-406fd8ab2e9e"), new Guid("929583cc-576b-4da9-b41a-01c7f1857c5d") },
                    { new Guid("e0001701-6039-4297-bfc1-a0df8564f126"), new Guid("46927bde-ec41-454a-9152-665d300763bd"), new Guid("66316ad8-ed0b-4109-b139-f3f1da4301e1") },
                    { new Guid("e289fa09-3b4c-4191-9072-44821ba37049"), new Guid("25448a6e-feb4-48d0-89d6-b502c5a178bb"), new Guid("929583cc-576b-4da9-b41a-01c7f1857c5d") }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "IsPrimary", "ProductColorId", "Url" },
                values: new object[,]
                {
                    { new Guid("09151426-a6e7-46ce-8f23-5edd6dd32a79"), false, new Guid("e289fa09-3b4c-4191-9072-44821ba37049"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_2_sx4vva.jpg" },
                    { new Guid("09a2c3f7-5cb7-4626-b32a-0e4a6d830249"), false, new Guid("72248adb-202b-4d42-970c-929012f09f92"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_2_rhdpsy.jpg" },
                    { new Guid("11e7d2ef-6c41-4560-b67b-bbf1089db4d1"), true, new Guid("5467d69e-5137-4235-8c02-5909ed6ebdc3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440193/black_1_mvzenu.jpg" },
                    { new Guid("166ad3e4-2c12-420a-baad-861c0417fa15"), false, new Guid("9667cdca-9f10-4b4a-adf2-b9fa91341bf6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440196/blue_2_ioyjmd.jpg" },
                    { new Guid("2314c140-91c2-4bd2-bf6f-13e4eede93d6"), true, new Guid("bae4694d-1401-4ea4-a640-8004c5ffd7d5"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/green_1_pwrndf.jpg" },
                    { new Guid("231bbc81-b82f-42e8-a754-4f97aee13040"), false, new Guid("826c33f3-4392-42a5-b98b-6f7d85bcbc49"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439222/olive_2_ovtcev.jpg" },
                    { new Guid("3150494d-9dab-4def-af12-4f03fe1d10fa"), true, new Guid("74e406b8-92de-4ef5-9fce-c42806af61fe"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440401/navy_1_vrk8fm.jpg" },
                    { new Guid("3fab3e80-ea3e-40eb-bbdc-a02bc182de6a"), false, new Guid("08f4b2b8-3892-459d-8b62-fc78201df9c6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440406/white_2_mbd76x.jpg" },
                    { new Guid("41241bbd-9f7d-4f5e-8b50-a9e567e0cc4a"), false, new Guid("42666ce5-a7cb-4cc4-b131-9e8d5d8d0108"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_2_dfpfn5.jpg" },
                    { new Guid("46fd4b3d-6c69-4742-a535-b4dd52b00787"), false, new Guid("5b142a1f-52e4-482b-b286-ace90b0e370e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_2_oohqly.jpg" },
                    { new Guid("47e41546-a05b-4033-9de8-6b6993d12d45"), true, new Guid("bb98d5f4-ce56-4abd-b6d6-a70dec58f6e0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439524/navy_1_kkhyqa.jpg" },
                    { new Guid("494d2191-6685-4ceb-81b6-bb2e4ccb9c98"), true, new Guid("156faef1-45cb-40bb-87d1-f0fb4a714fd3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/light-gray_1_uyfeh7.jpg" },
                    { new Guid("4cbabba3-38b7-4bac-ae60-30e97b7ae6c9"), true, new Guid("74c30efb-2929-490c-adf6-db5ea0eb5724"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440252/navy_1_aewdgs.jpg" },
                    { new Guid("65600a82-78bb-4e51-9df8-4c93e0649ffb"), true, new Guid("72248adb-202b-4d42-970c-929012f09f92"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_1_r9smbn.jpg" },
                    { new Guid("662d545a-336a-4917-ae56-b7f3ebf00823"), true, new Guid("4e3f6713-8537-4901-9912-1b8e60370647"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440315/gray_1_eurozu.jpg" },
                    { new Guid("68cb87f4-fd4f-47e1-8986-0c436190ba55"), true, new Guid("e289fa09-3b4c-4191-9072-44821ba37049"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_1_gkqv9b.jpg" },
                    { new Guid("7217e0b6-8f27-404e-845e-0045171aa6a6"), true, new Guid("ab1782df-ea23-4f45-9d90-2b42c267a40d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_1_h2bauh.jpg" },
                    { new Guid("81443ec6-6f4f-4c7c-bc19-b70819d37178"), true, new Guid("e0001701-6039-4297-bfc1-a0df8564f126"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439897/gray_1_lzp7go.jpg" },
                    { new Guid("849d761d-84f9-4f38-9a54-67576a42731d"), false, new Guid("ab1782df-ea23-4f45-9d90-2b42c267a40d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_2_vj3zqp.jpg" },
                    { new Guid("86e2ccb8-3f48-436f-8402-cefaa47e4151"), false, new Guid("b12213db-f580-422a-b301-e35ad569d07f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_2_ygzmiw.jpg" },
                    { new Guid("874a2f41-c8ee-470a-b443-dabd8358c429"), true, new Guid("d9cbb083-26b5-496e-98a6-fce3be357293"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438076/white_1_t5dag8.jpg" },
                    { new Guid("9550b8ec-cc4f-40d9-953c-af1f831192ca"), false, new Guid("c617ba9d-6b83-4ef6-b8c6-ccca5688aa6d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_2_pvuo6o.jpg" },
                    { new Guid("a1ffaa6b-a72e-4a27-8b47-1fbfde0c3278"), true, new Guid("c617ba9d-6b83-4ef6-b8c6-ccca5688aa6d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_1_eaakhx.jpg" },
                    { new Guid("a2a5d019-2058-4159-b0db-6ff6fe46e4a5"), true, new Guid("08f4b2b8-3892-459d-8b62-fc78201df9c6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440403/white_1_nd5suu.jpg" },
                    { new Guid("a632467a-19cb-4200-940d-e3b0105ee55b"), false, new Guid("4e3f6713-8537-4901-9912-1b8e60370647"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440316/gray_2_gif5gy.jpg" },
                    { new Guid("aad3dcef-d62b-4459-87b1-e877a7038f3b"), false, new Guid("74c30efb-2929-490c-adf6-db5ea0eb5724"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/navy_2_sybyag.jpg" },
                    { new Guid("abbbcf69-d572-4d08-bbb7-24c469c0399b"), true, new Guid("9f416d19-921e-4a5a-bbc3-c07a0fe622e8"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440458/olive_1_uixjsd.jpg" },
                    { new Guid("b01fbc5b-55b4-43d8-9ee9-56b079eb12e2"), true, new Guid("b12213db-f580-422a-b301-e35ad569d07f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_1_v0tcxo.jpg" },
                    { new Guid("b0306f1f-5993-46f1-8d4c-e4b75fcd7506"), false, new Guid("156faef1-45cb-40bb-87d1-f0fb4a714fd3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440135/light-gray_2_hpszuf.jpg" },
                    { new Guid("b2d57dd8-df91-4feb-b661-9820efcc13b8"), false, new Guid("236e1e24-61d6-497c-b8d3-9d345aff253a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_2_xf8iqr.jpg" },
                    { new Guid("b60c2494-ff18-4717-99c0-7ca0a9a5cca2"), false, new Guid("8d3cc25f-0c8e-4e37-9570-2e3deb215d45"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/black_2_xkoq54.jpg" },
                    { new Guid("bc06c230-df3d-4d84-ab4a-889de917a202"), false, new Guid("8b2ddc46-d8b4-4f25-a081-4807204740d0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440318/white_2_opuzjo.jpg" },
                    { new Guid("bfa5fb97-1048-4bab-bcc8-d25dfe0b2e7b"), false, new Guid("d9cbb083-26b5-496e-98a6-fce3be357293"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438077/white_2_oicoau.jpg" },
                    { new Guid("c1c16734-de6b-4d70-b687-d257636e31aa"), true, new Guid("42666ce5-a7cb-4cc4-b131-9e8d5d8d0108"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_1_zmlakv.jpg" },
                    { new Guid("c7043e48-4780-4c37-af7b-da39de025a6e"), true, new Guid("5b142a1f-52e4-482b-b286-ace90b0e370e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_1_zht2re.jpg" },
                    { new Guid("ca2c5712-c096-4e80-a426-57609ba18b3b"), false, new Guid("bae4694d-1401-4ea4-a640-8004c5ffd7d5"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439960/green_2_kf2ll7.jpg" },
                    { new Guid("cb569db3-ea51-4f27-9e3b-89414cf3b7bf"), true, new Guid("8b2ddc46-d8b4-4f25-a081-4807204740d0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440317/white_1_lppqmp.jpg" },
                    { new Guid("cc3a9ca3-2b04-4a16-a6fa-4699379046f5"), false, new Guid("5467d69e-5137-4235-8c02-5909ed6ebdc3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440194/black_2_yptvzh.jpg" },
                    { new Guid("d55b86b8-3585-4019-8ffe-86d469377f0d"), false, new Guid("c6d43721-cec3-4820-85ac-f6f8bd9e9fac"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440251/brown_2_zqkgfj.jpg" },
                    { new Guid("d575e420-d17e-4542-9a22-c58d2bd92534"), true, new Guid("826c33f3-4392-42a5-b98b-6f7d85bcbc49"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/olive_1_faf0az.jpg" },
                    { new Guid("db6cbc35-75d5-4822-b9af-ce48a5a9a083"), false, new Guid("e0001701-6039-4297-bfc1-a0df8564f126"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/gray_2_bzzytk.jpg" },
                    { new Guid("e28e8092-dbd4-4472-8bf9-14f5949bc97e"), false, new Guid("bb98d5f4-ce56-4abd-b6d6-a70dec58f6e0"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/navy_2_kolxaz.jpg" },
                    { new Guid("f80497fa-1d5a-41c3-9e27-6abe34b4e16e"), false, new Guid("74e406b8-92de-4ef5-9fce-c42806af61fe"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440402/navy_2_vgb74l.jpg" },
                    { new Guid("fa3f4693-1596-4b9c-80b6-1e532d9727e8"), true, new Guid("8d3cc25f-0c8e-4e37-9570-2e3deb215d45"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439223/black_1_lbqxjg.jpg" },
                    { new Guid("fb1844ce-beb1-475a-9f04-08161b9d9dc0"), false, new Guid("9f416d19-921e-4a5a-bbc3-c07a0fe622e8"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440460/olive_2_q9e65h.jpg" },
                    { new Guid("fda00dde-43cb-4ab7-ae84-e2e59eb40c31"), true, new Guid("9667cdca-9f10-4b4a-adf2-b9fa91341bf6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440195/blue_1_hwruph.jpg" },
                    { new Guid("feec8d07-f126-4332-beaf-89b2019b8adb"), true, new Guid("236e1e24-61d6-497c-b8d3-9d345aff253a"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_1_xaltqx.jpg" },
                    { new Guid("ff3c99d1-c5e7-4854-85fc-d5164bd839c5"), true, new Guid("c6d43721-cec3-4820-85ac-f6f8bd9e9fac"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/brown_1_gmlsf5.jpg" }
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
