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
                    ProductCount = table.Column<int>(type: "int", nullable: false),
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
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OrderStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_CartItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
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
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
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
                    { "b3980b2e-fc1d-41ed-997f-a6407920f048", "41f7081c-639d-4728-addc-d8d281432ea8", "User", "USER" },
                    { "fe7283b4-661a-4230-8a12-eb06cc16ba3c", "f45bc4bc-f869-4706-85b6-bacf60a7e646", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "CartStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b10a4294-645b-462d-bb4f-198b72cf9904"), "Có hàng" },
                    { new Guid("cd666bd1-f7a9-4355-a6f4-b395f40de032"), "Trống" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4de85204-0560-405f-9a9b-a6f4d7ea1487"), "Áo" },
                    { new Guid("f9ac455f-41da-4983-8891-aae2b7058424"), "Phụ kiện" },
                    { new Guid("fc9ffec4-b595-4740-acd9-76cd598f56b9"), "Quần" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "ColorCode", "Name" },
                values: new object[,]
                {
                    { new Guid("00e23392-e844-4c50-b20b-252e65b05c54"), "#DEDEDE", "Grey" },
                    { new Guid("0a3e66f0-0ac5-4075-9916-78217c4c4c93"), "#EB3417", "Red" },
                    { new Guid("1cb1c8b3-e020-49d3-8251-dfd26eba8ef9"), "#714E36", "Brown" },
                    { new Guid("28fa8ebf-6946-4b74-a552-77a4f6a956ea"), "#387D1F", "Green" },
                    { new Guid("3535b3df-3d13-4c8b-af77-171cd5e342c5"), "#FFFF3F", "Yellow" },
                    { new Guid("68c07f92-fa29-4296-8bb3-600a8e7750ae"), "#3D3D3D", "Black" },
                    { new Guid("757b58f5-54a8-4fce-ad27-79b2cd5fbc84"), "#F5C0C9", "Pink" },
                    { new Guid("cfd6a0e9-37ff-46c6-826c-43ef9d53e6cb"), "#0003F9", "Blue" },
                    { new Guid("e6ec535c-29b5-4491-ba0f-ace916ce6f8d"), "#FFFFFF", "White" },
                    { new Guid("eb0a1ce8-f538-475b-9dfc-b674a4182def"), "#EFEBD4", "Beige" },
                    { new Guid("f69ce8c3-2546-4a35-bac0-ea9dfb8a7207"), "#F3A72C", "Orange" },
                    { new Guid("fb9fa8b3-3d12-4524-b4d0-27a4425e2a82"), "#741A7C", "Purple" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2f8cc928-03b4-4a30-81cb-3b2c9f821751"), "Xác nhận" },
                    { new Guid("a5cf6b86-d897-4e40-a6bd-f778107ab374"), "Đã hủy" },
                    { new Guid("b1ca1b6d-be04-4b89-84e3-5aae0ceec124"), "Đang chờ xác nhận" },
                    { new Guid("b8431a34-b0b2-43e7-9ae5-447335e7ec19"), "Đã giao" },
                    { new Guid("b96b185a-8f06-4674-bcd5-aed0ad300acd"), "Chờ giao hàng" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("32bc7f03-6493-411f-9ac1-7061a42f3c0f"), "3XL" },
                    { new Guid("4f9c5a76-f7f8-4737-b2df-0c974561a0f7"), "2XL" },
                    { new Guid("b3a63acf-eda9-48b3-814c-f24d72c27be8"), "XL" },
                    { new Guid("b8ae4678-5dbd-4192-a151-81b3bd130243"), "L" },
                    { new Guid("bc732479-4839-4d06-830c-7a74bb95f6db"), "M" }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("189a0409-4e45-4e86-9551-76c83483de86"), new Guid("4de85204-0560-405f-9a9b-a6f4d7ea1487"), "Áo sơ mi" },
                    { new Guid("49881797-7198-4998-973f-d069659357cd"), new Guid("f9ac455f-41da-4983-8891-aae2b7058424"), "Kính mát" },
                    { new Guid("5278fcb4-2857-4397-8e68-7a95731f29c1"), new Guid("4de85204-0560-405f-9a9b-a6f4d7ea1487"), "Áo polo" },
                    { new Guid("72c922f9-7196-4ec1-b2df-feca550d985b"), new Guid("fc9ffec4-b595-4740-acd9-76cd598f56b9"), "Quần Jeans" },
                    { new Guid("8d725127-621e-48bd-9f08-08278a163afe"), new Guid("fc9ffec4-b595-4740-acd9-76cd598f56b9"), "Quần Short" },
                    { new Guid("bea03f3c-0eed-4665-bc50-ca73398848cd"), new Guid("fc9ffec4-b595-4740-acd9-76cd598f56b9"), "Quần Tây" },
                    { new Guid("c82ee7c2-deb0-4c9a-8abe-f0d27e156d0a"), new Guid("f9ac455f-41da-4983-8891-aae2b7058424"), "Mũ & Mũ lưỡi trai" },
                    { new Guid("ccde8b8c-2279-443e-9dc9-8447eb880ca5"), new Guid("f9ac455f-41da-4983-8891-aae2b7058424"), "Túi" },
                    { new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new Guid("4de85204-0560-405f-9a9b-a6f4d7ea1487"), "Áo thun" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Price", "ProductTypeId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("334f7595-c66a-4cfb-aa5f-5575a5ed44f3"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(821), "Chất liệu 100% cotton bền chắc, cổ áo được làm bằng vải mềm, thiết kế giản dị lấy cảm hứng từ áo bóng bầu dục cổ điển.", false, "Áo Thun Vải Cotton Cổ Henley Ngắn Tay", 489000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(821) },
                    { new Guid("3e7fac1f-f0da-4fbb-9725-2b2a4b6a0715"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(840), "Phần thân được làm từ chất liệu 100% cotton cực dày dặn, cảm giác khá sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Ringer)", 293000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(840) },
                    { new Guid("3ee84e29-6ca8-4686-9b26-946926f60002"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(825), "100% cotton SUPIMA® cao cấp, mịn màng, thiết kế cơ bản phù hợp tạo kiểu với nhiều phong cách khác nhau từ đơn giản đến layer, được thiết kế tỉ mỉ đến từng chi tiết, từ chiều rộng cổ áo đến đường may.", false, "Áo Thun Supima Cotton Cổ Tròn Ngắn Tay", 391000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(826) },
                    { new Guid("558c3fce-6292-4bc3-999e-f81ca3d4916e"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(823), "Chất liệu 100% cotton cực dày dặn, cảm giác sắc nét, mịn màng, được giặt trước một lần nước để có phong cách hoàn toàn giản dị.", false, "Áo Thun Dáng Rộng Kẻ Sọc Cổ Tròn Tay Lỡ", 391000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(824) },
                    { new Guid("7ba4e307-667c-4683-9fd8-58334be00eec"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(830), "Chất liệu vải jersey 100% cotton dày dặn, mang lại cảm giác tươi mát, vải jersey khô được dệt nhỏ gọn có độ bền cao và có đặc tính sau mỗi lần giặt, buộc dây ở cổ áo giúp giữ nguyên kiểu dáng đường viền cổ áo.", false, "Áo Thun Cổ Tròn Ngắn Tay", 293000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(830) },
                    { new Guid("92e69b3d-36d4-49a7-93cc-ad3c397e2a70"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(827), "Cảm giác giản dị của cotton, lớp lót polyester Với công nghệ DRY khô nhanh, thiết kế cơ bản mà bạn có thể tự tạo phong cách riêng hoặc theo Kiểu layer.", false, "Áo Thun Dry Cổ Tròn Nhiều Màu", 146000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(828) },
                    { new Guid("b68880b2-2ed9-4039-94a7-3cdcff86f5f4"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(837), "Chất vải 'AIRism' mịn màng trông như cotton, Với công nghệ DRY khô nhanh, chất liệu vải sắc nét tạo nên kiểu dáng đẹp mắt.", false, "AIRism Cotton Áo Thun Không Tay", 293000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(838) },
                    { new Guid("c8d25d58-1140-4a6a-8f18-2f4faec2bdd0"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(805), "Bộ sưu tập U từ thương hiệu Uniqlo là kết tinh sáng tạo của đội ngũ thiết kế quốc tế tận tâm và tài năng đến từ Trung tâm Nghiên cứu và Phát triển Paris, dưới sự dẫn dắt của Giám đốc Nghệ thuật Christophe Lemaire.", false, "AIRism Cotton Áo Thun Dáng Rộng Tay Lỡ", 391000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(814) },
                    { new Guid("dcd470e6-9e95-4296-9b5c-e949639e8aaf"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(834), "Vải 'AIRism' mịn màng trông như cotton, cổ tròn hẹp mang lại vẻ ngoài bóng bẩy, vai trễ và tay áo dài đến một nửa rộng rãi, chất liệu vải tạo dáng tôn dáng.", false, "AIRism Cotton Áo Thun Dáng Rộng", 391000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(834) },
                    { new Guid("e347acab-4b00-4fb3-bb8f-e81c4629db3d"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(832), "Một phiên bản mới của chiếc áo thun cổ tròn vải waffle nay đã có mặt, được thiết kế với kiểu dáng đơn giản, không có túi ở phần trước ngực, cải tiến với đường may thẳng cùng kiểu dáng xẻ tà, dễ dàng mặc cho mọi dịp.", false, "Áo Thun Vải Waffle Dài Tay", 391000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(832) },
                    { new Guid("e620841b-871e-49d6-b4dc-7890d00ae6b5"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(819), "Vải 'DRY-EX' nhanh chóng hấp thụ và hút ẩm để giữ cho làn da của bạn cảm giác tươi mát.", false, "DRY-EX Áo Thun Cổ Tròn", 391000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(819) },
                    { new Guid("e7900470-56f9-423d-9ddb-bfc2d28973ac"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(842), "Chất liệu 100% cotton dày dặn hoàn hảo, cảm giác sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Raglan)", 293000m, new Guid("f35b93ca-b8d8-4a21-b1af-6ff7ac96f9a7"), new DateTime(2024, 8, 2, 17, 35, 14, 495, DateTimeKind.Local).AddTicks(842) }
                });

            migrationBuilder.InsertData(
                table: "ProdductColor",
                columns: new[] { "Id", "ColorId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("04d12b74-a0e9-4251-8dbb-9f6d2cfa55ec"), new Guid("cfd6a0e9-37ff-46c6-826c-43ef9d53e6cb"), new Guid("558c3fce-6292-4bc3-999e-f81ca3d4916e") },
                    { new Guid("093240e3-c0b4-462a-b3ab-a776b56defee"), new Guid("1cb1c8b3-e020-49d3-8251-dfd26eba8ef9"), new Guid("92e69b3d-36d4-49a7-93cc-ad3c397e2a70") },
                    { new Guid("0a00fddd-b9ba-4ec4-a186-95d6cc537e58"), new Guid("cfd6a0e9-37ff-46c6-826c-43ef9d53e6cb"), new Guid("dcd470e6-9e95-4296-9b5c-e949639e8aaf") },
                    { new Guid("18f29a07-6b4e-4fc5-aa5c-535d4ffcbcad"), new Guid("eb0a1ce8-f538-475b-9dfc-b674a4182def"), new Guid("7ba4e307-667c-4683-9fd8-58334be00eec") },
                    { new Guid("1ec6f99f-58ee-45dc-88b8-2ae84d424c43"), new Guid("0a3e66f0-0ac5-4075-9916-78217c4c4c93"), new Guid("3ee84e29-6ca8-4686-9b26-946926f60002") },
                    { new Guid("36947dbc-0517-4751-9841-601d9c79104d"), new Guid("28fa8ebf-6946-4b74-a552-77a4f6a956ea"), new Guid("92e69b3d-36d4-49a7-93cc-ad3c397e2a70") },
                    { new Guid("45b187d9-2f06-434d-a932-85efd4d77b5f"), new Guid("1cb1c8b3-e020-49d3-8251-dfd26eba8ef9"), new Guid("dcd470e6-9e95-4296-9b5c-e949639e8aaf") },
                    { new Guid("4971ab26-e279-48b6-bb5f-8f279a050bdb"), new Guid("e6ec535c-29b5-4491-ba0f-ace916ce6f8d"), new Guid("3e7fac1f-f0da-4fbb-9725-2b2a4b6a0715") },
                    { new Guid("4e8127fe-87ab-4bde-ac56-ae7e966cf44f"), new Guid("e6ec535c-29b5-4491-ba0f-ace916ce6f8d"), new Guid("c8d25d58-1140-4a6a-8f18-2f4faec2bdd0") },
                    { new Guid("62720260-acd0-4fed-8790-06702233f6cc"), new Guid("e6ec535c-29b5-4491-ba0f-ace916ce6f8d"), new Guid("b68880b2-2ed9-4039-94a7-3cdcff86f5f4") },
                    { new Guid("7c65956f-f339-4e5b-b638-b2a36b2414d3"), new Guid("68c07f92-fa29-4296-8bb3-600a8e7750ae"), new Guid("e347acab-4b00-4fb3-bb8f-e81c4629db3d") },
                    { new Guid("7d7baab9-ec37-4484-8ada-65ffd9575b17"), new Guid("68c07f92-fa29-4296-8bb3-600a8e7750ae"), new Guid("e620841b-871e-49d6-b4dc-7890d00ae6b5") },
                    { new Guid("92a6068e-ced7-48a2-9d69-0dd977162b39"), new Guid("00e23392-e844-4c50-b20b-252e65b05c54"), new Guid("b68880b2-2ed9-4039-94a7-3cdcff86f5f4") },
                    { new Guid("9a04284e-a517-4e6b-b9c0-0267b003c401"), new Guid("28fa8ebf-6946-4b74-a552-77a4f6a956ea"), new Guid("334f7595-c66a-4cfb-aa5f-5575a5ed44f3") },
                    { new Guid("9cb06349-4e63-4ecd-aa28-696f9278ef6e"), new Guid("28fa8ebf-6946-4b74-a552-77a4f6a956ea"), new Guid("e7900470-56f9-423d-9ddb-bfc2d28973ac") },
                    { new Guid("ac592a1a-7873-4cff-8cbe-ca31d28d1e56"), new Guid("68c07f92-fa29-4296-8bb3-600a8e7750ae"), new Guid("334f7595-c66a-4cfb-aa5f-5575a5ed44f3") },
                    { new Guid("ae7605fe-cbaa-48ad-a46e-4d667aea3a65"), new Guid("eb0a1ce8-f538-475b-9dfc-b674a4182def"), new Guid("e7900470-56f9-423d-9ddb-bfc2d28973ac") },
                    { new Guid("b4c4c78c-d475-4ec9-8154-77b140080c9e"), new Guid("68c07f92-fa29-4296-8bb3-600a8e7750ae"), new Guid("c8d25d58-1140-4a6a-8f18-2f4faec2bdd0") },
                    { new Guid("bedf5b56-b580-4f91-9d52-3c50f6cb162e"), new Guid("00e23392-e844-4c50-b20b-252e65b05c54"), new Guid("3ee84e29-6ca8-4686-9b26-946926f60002") },
                    { new Guid("cc710d02-65f7-4b27-a6f6-e538c5a603e4"), new Guid("00e23392-e844-4c50-b20b-252e65b05c54"), new Guid("7ba4e307-667c-4683-9fd8-58334be00eec") },
                    { new Guid("ce8c600f-0e60-420d-8dd7-48c9a52b4a57"), new Guid("cfd6a0e9-37ff-46c6-826c-43ef9d53e6cb"), new Guid("3e7fac1f-f0da-4fbb-9725-2b2a4b6a0715") },
                    { new Guid("e0ea5ba2-8c2c-492c-aacd-dc86f2b6baa3"), new Guid("757b58f5-54a8-4fce-ad27-79b2cd5fbc84"), new Guid("558c3fce-6292-4bc3-999e-f81ca3d4916e") },
                    { new Guid("e4999fd3-c9f4-4b58-9c82-5c71d421910d"), new Guid("cfd6a0e9-37ff-46c6-826c-43ef9d53e6cb"), new Guid("e347acab-4b00-4fb3-bb8f-e81c4629db3d") },
                    { new Guid("f8d1337c-df9c-471a-aff8-a84dfee9af37"), new Guid("cfd6a0e9-37ff-46c6-826c-43ef9d53e6cb"), new Guid("e620841b-871e-49d6-b4dc-7890d00ae6b5") }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "IsPrimary", "ProductColorId", "Url" },
                values: new object[,]
                {
                    { new Guid("007ec052-5824-48bf-95ce-3fff18bfe63f"), false, new Guid("e4999fd3-c9f4-4b58-9c82-5c71d421910d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440196/blue_2_ioyjmd.jpg" },
                    { new Guid("0425635a-99e7-4465-b7d4-777fa7998d08"), false, new Guid("62720260-acd0-4fed-8790-06702233f6cc"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440318/white_2_opuzjo.jpg" },
                    { new Guid("0666852e-b824-4550-b716-226cdf46fb2a"), true, new Guid("b4c4c78c-d475-4ec9-8154-77b140080c9e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_1_gkqv9b.jpg" },
                    { new Guid("06ea1484-c162-4715-bfbf-af58c336b03f"), false, new Guid("ac592a1a-7873-4cff-8cbe-ca31d28d1e56"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/black_2_xkoq54.jpg" },
                    { new Guid("071b5451-7017-4dda-9167-67b72d2431f4"), true, new Guid("e4999fd3-c9f4-4b58-9c82-5c71d421910d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440195/blue_1_hwruph.jpg" },
                    { new Guid("08a1ecd6-c35f-43ab-9e56-f033cf5b0a7d"), true, new Guid("ac592a1a-7873-4cff-8cbe-ca31d28d1e56"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439223/black_1_lbqxjg.jpg" },
                    { new Guid("0e3b7490-ba97-47de-a544-452892d330f0"), true, new Guid("e0ea5ba2-8c2c-492c-aacd-dc86f2b6baa3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_1_v0tcxo.jpg" },
                    { new Guid("1a14825a-a674-4834-bb12-65a70b339003"), true, new Guid("1ec6f99f-58ee-45dc-88b8-2ae84d424c43"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_1_zht2re.jpg" },
                    { new Guid("20084984-043e-451f-8709-4049a7eaeb8b"), false, new Guid("0a00fddd-b9ba-4ec4-a186-95d6cc537e58"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/navy_2_sybyag.jpg" },
                    { new Guid("21b1ae7d-7eb5-42a2-acba-a5d13b72868e"), false, new Guid("e0ea5ba2-8c2c-492c-aacd-dc86f2b6baa3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_2_ygzmiw.jpg" },
                    { new Guid("28449abf-6edd-4fc8-8394-fd74d186e828"), true, new Guid("0a00fddd-b9ba-4ec4-a186-95d6cc537e58"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440252/navy_1_aewdgs.jpg" },
                    { new Guid("2f675fa2-1da7-4e2b-9b0d-302001bffbd1"), false, new Guid("4971ab26-e279-48b6-bb5f-8f279a050bdb"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440406/white_2_mbd76x.jpg" },
                    { new Guid("305aaaa7-a4f2-4e96-8ffd-94a551f2514d"), false, new Guid("7d7baab9-ec37-4484-8ada-65ffd9575b17"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_2_rhdpsy.jpg" },
                    { new Guid("31cc6857-1915-410a-9e88-6761a4f66af1"), true, new Guid("18f29a07-6b4e-4fc5-aa5c-535d4ffcbcad"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_1_zmlakv.jpg" },
                    { new Guid("341946ee-d0d4-4249-8209-6edbee2d1150"), false, new Guid("1ec6f99f-58ee-45dc-88b8-2ae84d424c43"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_2_oohqly.jpg" },
                    { new Guid("35f732bf-6d75-4aba-aa25-750693420f92"), false, new Guid("92a6068e-ced7-48a2-9d69-0dd977162b39"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440316/gray_2_gif5gy.jpg" },
                    { new Guid("38986c7c-4705-4d89-9802-441404a1b441"), true, new Guid("04d12b74-a0e9-4251-8dbb-9f6d2cfa55ec"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439524/navy_1_kkhyqa.jpg" },
                    { new Guid("3ed27361-589f-4a73-a689-dc678c8acb8a"), false, new Guid("093240e3-c0b4-462a-b3ab-a776b56defee"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_2_vj3zqp.jpg" },
                    { new Guid("3fd593df-4c1c-4943-a0cd-190f578f320e"), true, new Guid("7d7baab9-ec37-4484-8ada-65ffd9575b17"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_1_r9smbn.jpg" },
                    { new Guid("51f1e78a-c51a-4357-97f4-2f274ebdafca"), false, new Guid("ce8c600f-0e60-420d-8dd7-48c9a52b4a57"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440402/navy_2_vgb74l.jpg" },
                    { new Guid("68ed9dfc-dce3-4962-a8d4-f1450c306c22"), false, new Guid("36947dbc-0517-4751-9841-601d9c79104d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439960/green_2_kf2ll7.jpg" },
                    { new Guid("71ab1755-3c54-4c6c-a1fd-271682484c8a"), false, new Guid("18f29a07-6b4e-4fc5-aa5c-535d4ffcbcad"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_2_dfpfn5.jpg" },
                    { new Guid("77336544-da04-4dcf-a26a-4b02c803d304"), false, new Guid("cc710d02-65f7-4b27-a6f6-e538c5a603e4"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440135/light-gray_2_hpszuf.jpg" },
                    { new Guid("8730995d-5a67-47ae-bb46-650cef10213d"), true, new Guid("f8d1337c-df9c-471a-aff8-a84dfee9af37"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_1_eaakhx.jpg" },
                    { new Guid("873ecbbf-54c2-4f8a-9e09-7cc9a1cc800c"), true, new Guid("ce8c600f-0e60-420d-8dd7-48c9a52b4a57"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440401/navy_1_vrk8fm.jpg" },
                    { new Guid("96aa75d9-c888-49dc-903c-55af6b188ce8"), false, new Guid("9cb06349-4e63-4ecd-aa28-696f9278ef6e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440460/olive_2_q9e65h.jpg" },
                    { new Guid("9f32d698-ca02-4204-8ec0-18bcfb6b12f6"), false, new Guid("b4c4c78c-d475-4ec9-8154-77b140080c9e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_2_sx4vva.jpg" },
                    { new Guid("ae4b3729-6360-4049-bcb1-1270273c6736"), true, new Guid("4971ab26-e279-48b6-bb5f-8f279a050bdb"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440403/white_1_nd5suu.jpg" },
                    { new Guid("b0b62756-d9d6-4245-958e-69c7910b4d7a"), true, new Guid("45b187d9-2f06-434d-a932-85efd4d77b5f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/brown_1_gmlsf5.jpg" },
                    { new Guid("b1151b1c-3c74-4594-a109-3c0e7c402cd1"), true, new Guid("7c65956f-f339-4e5b-b638-b2a36b2414d3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440193/black_1_mvzenu.jpg" },
                    { new Guid("ba140479-b68e-435b-a031-e5fce4e5ada5"), true, new Guid("92a6068e-ced7-48a2-9d69-0dd977162b39"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440315/gray_1_eurozu.jpg" },
                    { new Guid("be6bbf10-65c6-49d2-81ca-5c83944aed58"), true, new Guid("ae7605fe-cbaa-48ad-a46e-4d667aea3a65"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_1_xaltqx.jpg" },
                    { new Guid("c0f52c9b-075b-443c-98bb-223176ff9d18"), true, new Guid("9a04284e-a517-4e6b-b9c0-0267b003c401"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/olive_1_faf0az.jpg" },
                    { new Guid("c1085d1d-7389-453f-a521-d20e439d9fa8"), false, new Guid("bedf5b56-b580-4f91-9d52-3c50f6cb162e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/gray_2_bzzytk.jpg" },
                    { new Guid("c5d29f9c-0981-466c-84df-8156d996b9c7"), false, new Guid("ae7605fe-cbaa-48ad-a46e-4d667aea3a65"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_2_xf8iqr.jpg" },
                    { new Guid("c885f72a-0ea1-4c06-89a9-f675bab40c6f"), false, new Guid("4e8127fe-87ab-4bde-ac56-ae7e966cf44f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438077/white_2_oicoau.jpg" },
                    { new Guid("cdccbcd9-d578-47ac-9104-5fef26736015"), false, new Guid("04d12b74-a0e9-4251-8dbb-9f6d2cfa55ec"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/navy_2_kolxaz.jpg" },
                    { new Guid("cea2af4a-d60a-4c84-adb3-f81aaea9aee4"), true, new Guid("9cb06349-4e63-4ecd-aa28-696f9278ef6e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440458/olive_1_uixjsd.jpg" },
                    { new Guid("d2fdb252-13ae-436a-be74-8b6cdebf4e97"), true, new Guid("4e8127fe-87ab-4bde-ac56-ae7e966cf44f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438076/white_1_t5dag8.jpg" },
                    { new Guid("d3635f3a-ab12-4c4c-834c-e083e5e40f4e"), true, new Guid("093240e3-c0b4-462a-b3ab-a776b56defee"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_1_h2bauh.jpg" },
                    { new Guid("d3af0431-22b5-4ea1-925f-ba4d40959142"), false, new Guid("f8d1337c-df9c-471a-aff8-a84dfee9af37"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_2_pvuo6o.jpg" },
                    { new Guid("e1da8949-7961-42fa-bf94-e0942521a539"), true, new Guid("cc710d02-65f7-4b27-a6f6-e538c5a603e4"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/light-gray_1_uyfeh7.jpg" },
                    { new Guid("e768e1c6-d061-4c19-a4f0-4ecfa8de0054"), false, new Guid("45b187d9-2f06-434d-a932-85efd4d77b5f"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440251/brown_2_zqkgfj.jpg" },
                    { new Guid("ee15e5d0-0048-47fb-91f0-335a4c5c79e5"), false, new Guid("7c65956f-f339-4e5b-b638-b2a36b2414d3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440194/black_2_yptvzh.jpg" },
                    { new Guid("f01d03c1-66ab-4acd-bac8-505e5b379013"), true, new Guid("bedf5b56-b580-4f91-9d52-3c50f6cb162e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439897/gray_1_lzp7go.jpg" },
                    { new Guid("f55f2d2c-37be-4a64-bd0d-cb6f7317fd4b"), false, new Guid("9a04284e-a517-4e6b-b9c0-0267b003c401"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439222/olive_2_ovtcev.jpg" },
                    { new Guid("fcbdbdb7-9e54-4733-8985-69859300d867"), true, new Guid("62720260-acd0-4fed-8790-06702233f6cc"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440317/white_1_lppqmp.jpg" },
                    { new Guid("ff389c2d-ad96-4938-9dfd-1ac0bfbf4d26"), true, new Guid("36947dbc-0517-4751-9841-601d9c79104d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/green_1_pwrndf.jpg" }
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
                name: "IX_CartItem_ProductId",
                table: "CartItem",
                column: "ProductId");

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
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

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
