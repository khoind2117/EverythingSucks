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
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { "8d4d3064-ec2f-4a78-955b-135755d30e32", "01c36965-6bea-4ace-8aaa-9ee8c00ef6c0", "Admin", "ADMIN" },
                    { "b45b157f-5066-4893-a7c2-f8b16d3fa9f1", "c54ad79c-7c9f-4827-a570-718b373100da", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9b021dab-e578-4f1e-b950-3c879b260f59", 0, "123 Admin St", null, "e025364f-339a-42f4-be55-59faae26fa8f", "admin@gmail.com", true, "Admin", "EC", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEGTpN4CYajA0dYdgIMGSnBRBgqYX9RN1/BPAF1l08BmULgnnxYGkU4LzHnjHLZBx9A==", null, false, "926508d7-3d3c-464c-895c-cd2ee385ebf8", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CartStatusId", "UserId" },
                values: new object[] { new Guid("3381228b-d4b1-40ef-b7d3-d7b361b98424"), null, "9b021dab-e578-4f1e-b950-3c879b260f59" });

            migrationBuilder.InsertData(
                table: "CartStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("37a6cdc6-efb9-4373-9006-2867177ecb7a"), "Trống" },
                    { new Guid("c1f69a75-bf1c-4a53-a8a8-90b7b069f95f"), "Có hàng" }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("ab709163-8d52-4b0f-a057-756860ea59b0"), "Phụ kiện" },
                    { new Guid("e60de07f-6df6-4cde-b2ff-d5d0b3790e1e"), "Quần" },
                    { new Guid("f39163f3-fb6e-4efe-9c51-ab56525a57ad"), "Áo" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "ColorCode", "Name" },
                values: new object[,]
                {
                    { new Guid("1929ac40-c469-43b9-8415-b1e135870ede"), "#FFFFFF", "White" },
                    { new Guid("304d8429-e16f-421b-a5eb-813d8dbf00b6"), "#714E36", "Brown" },
                    { new Guid("4c702782-abd0-485a-a945-78ff78046618"), "#EB3417", "Red" },
                    { new Guid("722f201b-d3d2-4941-8c19-8e3bea681aaf"), "#FFFF3F", "Yellow" },
                    { new Guid("74737dac-7703-44b0-9621-4a77ca248c6e"), "#F5C0C9", "Pink" },
                    { new Guid("857ecdc3-f257-4f20-a03b-523fedf9056a"), "#741A7C", "Purple" },
                    { new Guid("8dac05f0-5f3d-4162-82ba-16156c991a4c"), "#387D1F", "Green" },
                    { new Guid("9d398d30-f4e6-4777-afa9-2f59f0192f2d"), "#DEDEDE", "Grey" },
                    { new Guid("a043c7b9-cf84-4fd1-afad-f83b3dfe4b34"), "#F3A72C", "Orange" },
                    { new Guid("dbfc57d3-e295-403a-b27d-a39c2974f55b"), "#3D3D3D", "Black" },
                    { new Guid("e233d819-ab20-43d9-a5bf-d2f40370e42c"), "#0003F9", "Blue" },
                    { new Guid("fc9f079b-e785-493b-b0ec-6bbdbf459e68"), "#EFEBD4", "Beige" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3341c0e8-81dd-4474-8ae5-886df544a63c"), "Chờ giao hàng" },
                    { new Guid("3e44c3f2-dc7a-4428-a101-1fc6381f8aaf"), "Đã hủy" },
                    { new Guid("93b236b4-46f6-43f5-a52c-446cfa0d799e"), "Xác nhận" },
                    { new Guid("95fe6d77-522e-48e7-9882-c3ca45925213"), "Đang chờ xác nhận" },
                    { new Guid("c1ae3cfe-e539-4157-bcd3-4c85f0a1bdd4"), "Đã giao" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8a867af7-9e6d-433e-bb40-c35cbe8b6658"), "3XL" },
                    { new Guid("dd82ed85-b897-4800-8fc0-decfbccd7572"), "M" },
                    { new Guid("ef132677-9929-4c30-bde5-afeb0339304a"), "L" },
                    { new Guid("f074ed7d-0916-4e34-abd9-efe5509887db"), "XL" },
                    { new Guid("ffe619d4-e784-4523-9a05-881fd91b1fe0"), "2XL" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "8d4d3064-ec2f-4a78-955b-135755d30e32", "9b021dab-e578-4f1e-b950-3c879b260f59" });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), new Guid("f39163f3-fb6e-4efe-9c51-ab56525a57ad"), "Áo thun" },
                    { new Guid("3b166fac-7774-40b4-b125-6d8ee1d6e377"), new Guid("ab709163-8d52-4b0f-a057-756860ea59b0"), "Mũ & Mũ lưỡi trai" },
                    { new Guid("7359214a-e494-4266-bcea-ae2bb615ccb0"), new Guid("ab709163-8d52-4b0f-a057-756860ea59b0"), "Kính mát" },
                    { new Guid("7486749d-38ab-42c6-ae7c-f516ae42df14"), new Guid("f39163f3-fb6e-4efe-9c51-ab56525a57ad"), "Áo polo" },
                    { new Guid("8c8fb1ca-c33f-491b-a1b1-3d7f0146b503"), new Guid("e60de07f-6df6-4cde-b2ff-d5d0b3790e1e"), "Quần Tây" },
                    { new Guid("a2b43af4-b402-428b-af2e-98402129708c"), new Guid("f39163f3-fb6e-4efe-9c51-ab56525a57ad"), "Áo sơ mi" },
                    { new Guid("e152445e-ef3c-46c3-b7fb-baaf525e1f54"), new Guid("e60de07f-6df6-4cde-b2ff-d5d0b3790e1e"), "Quần Jeans" },
                    { new Guid("fd2bb03f-63ca-4b47-aa8d-4d61d24922a7"), new Guid("ab709163-8d52-4b0f-a057-756860ea59b0"), "Túi" },
                    { new Guid("ffb58b18-e103-48df-b4c4-cc9fef026d05"), new Guid("e60de07f-6df6-4cde-b2ff-d5d0b3790e1e"), "Quần Short" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "Price", "ProductTypeId", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("09c61295-4094-47c2-b594-d0a689a7fc9f"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6504), "Vải 'AIRism' mịn màng trông như cotton, cổ tròn hẹp mang lại vẻ ngoài bóng bẩy, vai trễ và tay áo dài đến một nửa rộng rãi, chất liệu vải tạo dáng tôn dáng.", false, "AIRism Cotton Áo Thun Dáng Rộng", 391000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "airism-cotton-ao-thun-dang-rong", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6505) },
                    { new Guid("2043aadc-0040-4f3a-85fd-1a63acf6b25d"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6490), "Chất liệu 100% cotton bền chắc, cổ áo được làm bằng vải mềm, thiết kế giản dị lấy cảm hứng từ áo bóng bầu dục cổ điển.", false, "Áo Thun Vải Cotton Cổ Henley Ngắn Tay", 489000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-vai-cotton-co-henley-ngan-tay", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6491) },
                    { new Guid("2b54b3fd-b9e6-486b-8fe0-9c36f99d4ec1"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6475), "Bộ sưu tập U từ thương hiệu Uniqlo là kết tinh sáng tạo của đội ngũ thiết kế quốc tế tận tâm và tài năng đến từ Trung tâm Nghiên cứu và Phát triển Paris, dưới sự dẫn dắt của Giám đốc Nghệ thuật Christophe Lemaire.", false, "AIRism Cotton Áo Thun Dáng Rộng Tay Lỡ", 391000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "airism-cotton-ao-thun-dang-rong-tay-lo", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6485) },
                    { new Guid("602160e3-1d48-41ca-ad05-be16db6c17d0"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6502), "Một phiên bản mới của chiếc áo thun cổ tròn vải waffle nay đã có mặt, được thiết kế với kiểu dáng đơn giản, không có túi ở phần trước ngực, cải tiến với đường may thẳng cùng kiểu dáng xẻ tà, dễ dàng mặc cho mọi dịp.", false, "Áo Thun Vải Waffle Dài Tay", 391000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-vai-waffle-dai-tay", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6502) },
                    { new Guid("6d7ec1b2-272a-4ca5-8181-0d6c96c7a310"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6597), "Chất liệu 100% cotton dày dặn hoàn hảo, cảm giác sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Raglan)", 293000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-dang-rong-tay-lo-raglan", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6597) },
                    { new Guid("6ea3d056-9bd0-46ad-939a-03677d79282f"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6488), "Vải 'DRY-EX' nhanh chóng hấp thụ và hút ẩm để giữ cho làn da của bạn cảm giác tươi mát.", false, "DRY-EX Áo Thun Cổ Tròn", 391000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "dry-ex-ao-thun-co-tron", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6488) },
                    { new Guid("7ddf83f8-dc5f-4a4a-a61b-3932e4e0faa9"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6497), "Cảm giác giản dị của cotton, lớp lót polyester Với công nghệ DRY khô nhanh, thiết kế cơ bản mà bạn có thể tự tạo phong cách riêng hoặc theo Kiểu layer.", false, "Áo Thun Dry Cổ Tròn Nhiều Màu", 146000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-dry-co-tron-nhieu-mau", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6497) },
                    { new Guid("9b254a17-1c58-4510-92fb-47af6486e947"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6494), "100% cotton SUPIMA® cao cấp, mịn màng, thiết kế cơ bản phù hợp tạo kiểu với nhiều phong cách khác nhau từ đơn giản đến layer, được thiết kế tỉ mỉ đến từng chi tiết, từ chiều rộng cổ áo đến đường may.", false, "Áo Thun Supima Cotton Cổ Tròn Ngắn Tay", 391000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-supima-cotton-co-tron-ngan-tay", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6495) },
                    { new Guid("9b80d2ca-47df-4fab-8255-f29d155232a6"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6506), "Chất vải 'AIRism' mịn màng trông như cotton, Với công nghệ DRY khô nhanh, chất liệu vải sắc nét tạo nên kiểu dáng đẹp mắt.", false, "AIRism Cotton Áo Thun Không Tay", 293000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "airism-cotton-ao-thun-khong-tay", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6507) },
                    { new Guid("acc12af6-206f-4be9-9444-54204d5efc2d"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6499), "Chất liệu vải jersey 100% cotton dày dặn, mang lại cảm giác tươi mát, vải jersey khô được dệt nhỏ gọn có độ bền cao và có đặc tính sau mỗi lần giặt, buộc dây ở cổ áo giúp giữ nguyên kiểu dáng đường viền cổ áo.", false, "Áo Thun Cổ Tròn Ngắn Tay", 293000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-co-tron-ngan-tay", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6499) },
                    { new Guid("b63d6b79-26af-4ee6-9eb4-9d1c85a0eddf"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6508), "Phần thân được làm từ chất liệu 100% cotton cực dày dặn, cảm giác khá sắc nét, mịn màng, giữ nguyên hình dạng sau khi giặt.", false, "Áo Thun Dáng Rộng Tay Lỡ (Ringer)", 293000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-dang-rong-tay-lo-ringer", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6509) },
                    { new Guid("f8f0e63e-d665-452e-9214-a0ff58784d65"), new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6492), "Chất liệu 100% cotton cực dày dặn, cảm giác sắc nét, mịn màng, được giặt trước một lần nước để có phong cách hoàn toàn giản dị.", false, "Áo Thun Dáng Rộng Kẻ Sọc Cổ Tròn Tay Lỡ", 391000m, new Guid("18953569-5d03-47d8-b9ea-b12144f8ff65"), "ao-thun-dang-rong-ke-soc-co-tron-tay-lo", new DateTime(2024, 8, 9, 22, 12, 59, 909, DateTimeKind.Local).AddTicks(6493) }
                });

            migrationBuilder.InsertData(
                table: "ProdductColor",
                columns: new[] { "Id", "ColorId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("005f274f-57c3-4b79-802f-94fda79f5280"), new Guid("e233d819-ab20-43d9-a5bf-d2f40370e42c"), new Guid("602160e3-1d48-41ca-ad05-be16db6c17d0") },
                    { new Guid("096b0dda-8a37-48f9-8f53-da3dead40934"), new Guid("1929ac40-c469-43b9-8415-b1e135870ede"), new Guid("b63d6b79-26af-4ee6-9eb4-9d1c85a0eddf") },
                    { new Guid("12cb22c3-d943-4c95-926e-c3b3322a8801"), new Guid("fc9f079b-e785-493b-b0ec-6bbdbf459e68"), new Guid("6d7ec1b2-272a-4ca5-8181-0d6c96c7a310") },
                    { new Guid("1c60429a-3ab7-46c5-af10-cd1c85b68b71"), new Guid("e233d819-ab20-43d9-a5bf-d2f40370e42c"), new Guid("6ea3d056-9bd0-46ad-939a-03677d79282f") },
                    { new Guid("1e5104cd-dc22-4a91-b2ad-4dbc93f3ad76"), new Guid("e233d819-ab20-43d9-a5bf-d2f40370e42c"), new Guid("f8f0e63e-d665-452e-9214-a0ff58784d65") },
                    { new Guid("1e9bb361-47f1-4e8e-953a-85a59ceaf9d6"), new Guid("8dac05f0-5f3d-4162-82ba-16156c991a4c"), new Guid("7ddf83f8-dc5f-4a4a-a61b-3932e4e0faa9") },
                    { new Guid("1fe5e728-6d3d-4b5d-ad23-4ee59a364f96"), new Guid("e233d819-ab20-43d9-a5bf-d2f40370e42c"), new Guid("09c61295-4094-47c2-b594-d0a689a7fc9f") },
                    { new Guid("2a4d99d7-2d2a-4da5-aa1b-0b83db4e8830"), new Guid("e233d819-ab20-43d9-a5bf-d2f40370e42c"), new Guid("b63d6b79-26af-4ee6-9eb4-9d1c85a0eddf") },
                    { new Guid("43253cb8-a021-468d-a2b8-5d7f52f6c41e"), new Guid("dbfc57d3-e295-403a-b27d-a39c2974f55b"), new Guid("602160e3-1d48-41ca-ad05-be16db6c17d0") },
                    { new Guid("4d508f67-86ce-48c7-a0ed-c575b536ada3"), new Guid("dbfc57d3-e295-403a-b27d-a39c2974f55b"), new Guid("2b54b3fd-b9e6-486b-8fe0-9c36f99d4ec1") },
                    { new Guid("6b3ea4f4-f051-4948-9f3d-fec95c842415"), new Guid("dbfc57d3-e295-403a-b27d-a39c2974f55b"), new Guid("2043aadc-0040-4f3a-85fd-1a63acf6b25d") },
                    { new Guid("7271bb70-7c6c-41d4-ae93-0e5f588dceb2"), new Guid("9d398d30-f4e6-4777-afa9-2f59f0192f2d"), new Guid("acc12af6-206f-4be9-9444-54204d5efc2d") },
                    { new Guid("8525067e-beda-460e-bd31-943994af8675"), new Guid("9d398d30-f4e6-4777-afa9-2f59f0192f2d"), new Guid("9b80d2ca-47df-4fab-8255-f29d155232a6") },
                    { new Guid("8702532f-b7f6-42d6-867f-be4555adff33"), new Guid("1929ac40-c469-43b9-8415-b1e135870ede"), new Guid("9b80d2ca-47df-4fab-8255-f29d155232a6") },
                    { new Guid("8cae94a8-e4c7-4fe5-9979-3520057953be"), new Guid("74737dac-7703-44b0-9621-4a77ca248c6e"), new Guid("f8f0e63e-d665-452e-9214-a0ff58784d65") },
                    { new Guid("9095f5ba-9f85-4476-b3d9-38d98063aa46"), new Guid("dbfc57d3-e295-403a-b27d-a39c2974f55b"), new Guid("6ea3d056-9bd0-46ad-939a-03677d79282f") },
                    { new Guid("9cdeefb7-9730-488e-98f0-5bd53266be33"), new Guid("9d398d30-f4e6-4777-afa9-2f59f0192f2d"), new Guid("9b254a17-1c58-4510-92fb-47af6486e947") },
                    { new Guid("ad1750e3-0a68-4a89-9b14-8441088d277e"), new Guid("4c702782-abd0-485a-a945-78ff78046618"), new Guid("9b254a17-1c58-4510-92fb-47af6486e947") },
                    { new Guid("add54b59-9020-4dc8-93a1-de7e5fef1c8d"), new Guid("8dac05f0-5f3d-4162-82ba-16156c991a4c"), new Guid("2043aadc-0040-4f3a-85fd-1a63acf6b25d") },
                    { new Guid("af76e60a-8d08-431e-b31f-5e3a67019a03"), new Guid("fc9f079b-e785-493b-b0ec-6bbdbf459e68"), new Guid("acc12af6-206f-4be9-9444-54204d5efc2d") },
                    { new Guid("b3a2d4fc-332d-4bef-9388-880e8b891edd"), new Guid("1929ac40-c469-43b9-8415-b1e135870ede"), new Guid("2b54b3fd-b9e6-486b-8fe0-9c36f99d4ec1") },
                    { new Guid("b596820e-91c9-4306-bb20-67cf240e4e6e"), new Guid("304d8429-e16f-421b-a5eb-813d8dbf00b6"), new Guid("09c61295-4094-47c2-b594-d0a689a7fc9f") },
                    { new Guid("f787f0b3-7f2c-4ad5-a3d5-e36412c1cc8d"), new Guid("8dac05f0-5f3d-4162-82ba-16156c991a4c"), new Guid("6d7ec1b2-272a-4ca5-8181-0d6c96c7a310") },
                    { new Guid("fef783e2-8004-4644-b894-065480904eca"), new Guid("304d8429-e16f-421b-a5eb-813d8dbf00b6"), new Guid("7ddf83f8-dc5f-4a4a-a61b-3932e4e0faa9") }
                });

            migrationBuilder.InsertData(
                table: "ProductImage",
                columns: new[] { "Id", "IsPrimary", "ProductColorId", "Url" },
                values: new object[,]
                {
                    { new Guid("046bcc5c-7ed3-45d7-9f7d-ed38a3bec93b"), true, new Guid("fef783e2-8004-4644-b894-065480904eca"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_1_h2bauh.jpg" },
                    { new Guid("0df9d525-2504-465b-b0bb-ad49fdc7eb28"), true, new Guid("ad1750e3-0a68-4a89-9b14-8441088d277e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_1_zht2re.jpg" },
                    { new Guid("173a67cd-d8ee-4fef-af7d-7583bbf58765"), false, new Guid("4d508f67-86ce-48c7-a0ed-c575b536ada3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_2_sx4vva.jpg" },
                    { new Guid("263994ad-9f76-4d42-b3cf-116d888e410b"), false, new Guid("1c60429a-3ab7-46c5-af10-cd1c85b68b71"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_2_pvuo6o.jpg" },
                    { new Guid("3441e5fd-b661-4f6b-b64b-5dff271fa8e8"), true, new Guid("9095f5ba-9f85-4476-b3d9-38d98063aa46"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_1_r9smbn.jpg" },
                    { new Guid("35f91afd-f254-4100-b831-7bb58a0c3a9a"), true, new Guid("2a4d99d7-2d2a-4da5-aa1b-0b83db4e8830"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440401/navy_1_vrk8fm.jpg" },
                    { new Guid("378b2dd4-2dc4-4d5e-b7ad-573e39f72685"), true, new Guid("9cdeefb7-9730-488e-98f0-5bd53266be33"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439897/gray_1_lzp7go.jpg" },
                    { new Guid("3876b766-f575-4920-bcbb-9ede29e9eac4"), false, new Guid("005f274f-57c3-4b79-802f-94fda79f5280"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440196/blue_2_ioyjmd.jpg" },
                    { new Guid("3f9c32a0-eded-4b4d-8bbc-15e446e078c1"), false, new Guid("6b3ea4f4-f051-4948-9f3d-fec95c842415"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/black_2_xkoq54.jpg" },
                    { new Guid("3fc92cd3-72d3-40ef-aaf0-0e8cff404afe"), true, new Guid("43253cb8-a021-468d-a2b8-5d7f52f6c41e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440193/black_1_mvzenu.jpg" },
                    { new Guid("437f2ed8-f764-4cc3-9338-62b025370dd3"), false, new Guid("b3a2d4fc-332d-4bef-9388-880e8b891edd"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438077/white_2_oicoau.jpg" },
                    { new Guid("44b18950-907b-45ff-97d2-af47615eead4"), false, new Guid("af76e60a-8d08-431e-b31f-5e3a67019a03"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_2_dfpfn5.jpg" },
                    { new Guid("4643deb9-6d5f-4f5d-8efe-cec48b26d244"), false, new Guid("add54b59-9020-4dc8-93a1-de7e5fef1c8d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439222/olive_2_ovtcev.jpg" },
                    { new Guid("4a405a16-5595-4265-aae6-af313adc0123"), true, new Guid("6b3ea4f4-f051-4948-9f3d-fec95c842415"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439223/black_1_lbqxjg.jpg" },
                    { new Guid("4ade6cab-0578-4365-882a-374098ea3f77"), false, new Guid("9095f5ba-9f85-4476-b3d9-38d98063aa46"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438394/black_2_rhdpsy.jpg" },
                    { new Guid("4b3388e9-13b6-4f87-9e30-79faf711c9f2"), true, new Guid("af76e60a-8d08-431e-b31f-5e3a67019a03"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/natural_1_zmlakv.jpg" },
                    { new Guid("4ba0e633-a93f-42c3-a58e-4462cc65a931"), true, new Guid("12cb22c3-d943-4c95-926e-c3b3322a8801"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_1_xaltqx.jpg" },
                    { new Guid("4c5931be-75b3-460d-84d6-07774796b2c8"), false, new Guid("43253cb8-a021-468d-a2b8-5d7f52f6c41e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440194/black_2_yptvzh.jpg" },
                    { new Guid("5226a666-4d99-42e5-add1-f5953770dbf5"), false, new Guid("9cdeefb7-9730-488e-98f0-5bd53266be33"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/gray_2_bzzytk.jpg" },
                    { new Guid("5b8b907d-c1ae-416b-bc72-b264626e37d6"), true, new Guid("8cae94a8-e4c7-4fe5-9979-3520057953be"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_1_v0tcxo.jpg" },
                    { new Guid("5e95f910-acac-4476-868b-ff83e8053042"), false, new Guid("8525067e-beda-460e-bd31-943994af8675"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440316/gray_2_gif5gy.jpg" },
                    { new Guid("62c6f230-feef-49f1-80f6-793a8831626b"), true, new Guid("1fe5e728-6d3d-4b5d-ad23-4ee59a364f96"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440252/navy_1_aewdgs.jpg" },
                    { new Guid("740e6e84-0dd1-4d25-bd90-6c0e9ac637f8"), false, new Guid("ad1750e3-0a68-4a89-9b14-8441088d277e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439896/wine_2_oohqly.jpg" },
                    { new Guid("77b9b78e-b74a-423b-a658-223be9edb675"), true, new Guid("005f274f-57c3-4b79-802f-94fda79f5280"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440195/blue_1_hwruph.jpg" },
                    { new Guid("845e7b48-0dd9-48bf-8aa8-3644ce37f892"), true, new Guid("b596820e-91c9-4306-bb20-67cf240e4e6e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/brown_1_gmlsf5.jpg" },
                    { new Guid("84f5d10e-8405-4c11-8776-6474705e61d0"), true, new Guid("1e9bb361-47f1-4e8e-953a-85a59ceaf9d6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/green_1_pwrndf.jpg" },
                    { new Guid("89f53c3e-5bc9-4b8d-8936-51d7c9f2e920"), true, new Guid("8702532f-b7f6-42d6-867f-be4555adff33"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440317/white_1_lppqmp.jpg" },
                    { new Guid("8ddaf80c-ec4c-4bb2-8739-9f3d5c060f4e"), false, new Guid("b596820e-91c9-4306-bb20-67cf240e4e6e"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440251/brown_2_zqkgfj.jpg" },
                    { new Guid("8e084824-e119-4399-b098-7fb926171c8f"), true, new Guid("7271bb70-7c6c-41d4-ae93-0e5f588dceb2"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440136/light-gray_1_uyfeh7.jpg" },
                    { new Guid("9a797999-3493-4fbf-81b4-89f49006c46c"), true, new Guid("add54b59-9020-4dc8-93a1-de7e5fef1c8d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439221/olive_1_faf0az.jpg" },
                    { new Guid("9ba5a60f-011c-43eb-a339-c1a4c1835491"), false, new Guid("fef783e2-8004-4644-b894-065480904eca"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439959/brown_2_vj3zqp.jpg" },
                    { new Guid("9e5a7a9f-b5a8-44fc-ae7a-dfcfa0192216"), false, new Guid("f787f0b3-7f2c-4ad5-a3d5-e36412c1cc8d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440460/olive_2_q9e65h.jpg" },
                    { new Guid("a86ab141-ade7-404e-b00c-6ac93921b476"), true, new Guid("096b0dda-8a37-48f9-8f53-da3dead40934"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440403/white_1_nd5suu.jpg" },
                    { new Guid("b1152550-4a9e-4de3-bed9-d7ed708cf17b"), false, new Guid("1e5104cd-dc22-4a91-b2ad-4dbc93f3ad76"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/navy_2_kolxaz.jpg" },
                    { new Guid("b7e0090a-fb06-4eae-8ebc-c6751c67d9e3"), true, new Guid("f787f0b3-7f2c-4ad5-a3d5-e36412c1cc8d"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440458/olive_1_uixjsd.jpg" },
                    { new Guid("b812f0a4-6f14-4d41-a818-7e1023d17baa"), false, new Guid("12cb22c3-d943-4c95-926e-c3b3322a8801"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440456/beige_2_xf8iqr.jpg" },
                    { new Guid("bd05fa18-cf05-4569-b93f-83828a54a906"), false, new Guid("8702532f-b7f6-42d6-867f-be4555adff33"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440318/white_2_opuzjo.jpg" },
                    { new Guid("c0f93587-675c-4a1d-95e7-3937ae642447"), false, new Guid("096b0dda-8a37-48f9-8f53-da3dead40934"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440406/white_2_mbd76x.jpg" },
                    { new Guid("c809148a-7c04-458e-9d26-af6ca3358006"), true, new Guid("1c60429a-3ab7-46c5-af10-cd1c85b68b71"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438404/blue_1_eaakhx.jpg" },
                    { new Guid("c950cd04-bf4d-4355-94cd-6c9832c1f968"), true, new Guid("1e5104cd-dc22-4a91-b2ad-4dbc93f3ad76"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439524/navy_1_kkhyqa.jpg" },
                    { new Guid("d048a881-520a-4f1e-b58c-0a8fea419e2a"), false, new Guid("8cae94a8-e4c7-4fe5-9979-3520057953be"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439525/pink_2_ygzmiw.jpg" },
                    { new Guid("d379486a-e5de-45f4-abc9-d2972526d8ac"), false, new Guid("2a4d99d7-2d2a-4da5-aa1b-0b83db4e8830"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440402/navy_2_vgb74l.jpg" },
                    { new Guid("d90eac65-9966-4553-a0d8-6108f04abfe0"), false, new Guid("1e9bb361-47f1-4e8e-953a-85a59ceaf9d6"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722439960/green_2_kf2ll7.jpg" },
                    { new Guid("d9a00c5b-23e4-493f-9880-e89135564f2c"), true, new Guid("4d508f67-86ce-48c7-a0ed-c575b536ada3"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438090/black_1_gkqv9b.jpg" },
                    { new Guid("e77a0980-f5df-4841-a261-064b2a899a0c"), true, new Guid("8525067e-beda-460e-bd31-943994af8675"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440315/gray_1_eurozu.jpg" },
                    { new Guid("efcafc54-633e-4607-8ceb-f74f61b97f77"), false, new Guid("1fe5e728-6d3d-4b5d-ad23-4ee59a364f96"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440253/navy_2_sybyag.jpg" },
                    { new Guid("f27f3763-64bd-44b5-a16a-c543ad962a88"), true, new Guid("b3a2d4fc-332d-4bef-9388-880e8b891edd"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722438076/white_1_t5dag8.jpg" },
                    { new Guid("f9c95af3-92e8-46d1-abd4-9ddd26414067"), false, new Guid("7271bb70-7c6c-41d4-ae93-0e5f588dceb2"), "https://res.cloudinary.com/djsdux2v9/image/upload/v1722440135/light-gray_2_hpszuf.jpg" }
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
