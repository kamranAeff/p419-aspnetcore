using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductCard_Id_and_IsDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Membership");

            migrationBuilder.CreateTable(
                name: "ContactPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    AnsweredBy = table.Column<int>(type: "int", nullable: true),
                    AnsweredAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    AnsweredMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Membership",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brands_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brands_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brands_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Categories_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HexCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colors_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Colors_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Colors_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SmallName = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sizes_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sizes_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Membership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Membership",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Membership",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Membership",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Membership",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Slug = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    ImagePath = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogPosts_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogPosts_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlogPosts_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Slug = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    UnitOfWeight = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCards_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCards_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCards_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCards_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCards_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCards_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreateBy = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductImages_Users_CreateBy",
                        column: x => x.CreateBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductImages_Users_DeletedBy",
                        column: x => x.DeletedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductImages_Users_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => new { x.UserId, x.ProductCardId });
                    table.ForeignKey(
                        name: "FK_Baskets_ProductCards_ProductCardId",
                        column: x => x.ProductCardId,
                        principalTable: "ProductCards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Baskets_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "Membership",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "33520081-7bc2-47f7-86d9-ead654c0dcc0", "SuperAdmin", "SUPERADMIN" },
                    { 2, "567a55a3-3320-4e05-a37e-d5feb3f4a2e6", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                schema: "Membership",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "a2679ce7-36e9-4db7-9bce-3b52995e5f2b", "akamran@code.edu.az", true, false, null, "AKAMRAN@CODE.EDU.AZ", "KAMRANAEFF", "AQAAAAIAAYagAAAAECpxiem5TR/F3B65TycLMQyRnyHa5Gro8tb/xOsEYdpSMbfEGRE4lQNSD8QOXUPCrg==", null, false, "a2679ce7-36e9-4db7-9bce-1152995e5f2b", false, "kamranAeff" });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreateBy", "CreatedAt", "DeletedAt", "DeletedBy", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Fresh Meat" },
                    { 2, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Vegetables" },
                    { 3, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Fruit & Nut Gifts" },
                    { 4, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Fresh Berries" },
                    { 5, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Ocean Foods" },
                    { 6, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Butter & Eggs" },
                    { 7, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Fastfood" },
                    { 8, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Fresh Onion" },
                    { 9, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Papayaya & Crisps" },
                    { 10, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Oatmeal" },
                    { 11, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Fresh Bananas" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateBy", "CreatedAt", "DeletedAt", "DeletedBy", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Beauty" },
                    { 2, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Travel" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "CreateBy", "CreatedAt", "DeletedAt", "DeletedBy", "HexCode", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "#ffffff", null, null, "Ag" },
                    { 2, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "#000000", null, null, "Qara" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "CreateBy", "CreatedAt", "DeletedAt", "DeletedBy", "ModifiedAt", "ModifiedBy", "Name", "SmallName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Extra small", "XS" },
                    { 2, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Small", "S" },
                    { 3, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Medium", "M" },
                    { 4, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Large", "L" },
                    { 5, 1, new DateTime(2024, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Extra large", "XL" }
                });

            migrationBuilder.InsertData(
                schema: "Membership",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_ProductCardId",
                table: "Baskets",
                column: "ProductCardId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CategoryId",
                table: "BlogPosts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CreateBy",
                table: "BlogPosts",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_DeletedBy",
                table: "BlogPosts",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_ModifiedBy",
                table: "BlogPosts",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreateBy",
                table: "Brands",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_DeletedBy",
                table: "Brands",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ModifiedBy",
                table: "Brands",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreateBy",
                table: "Categories",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedBy",
                table: "Categories",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedBy",
                table: "Categories",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_CreateBy",
                table: "Colors",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_DeletedBy",
                table: "Colors",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_ModifiedBy",
                table: "Colors",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCards_ColorId",
                table: "ProductCards",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCards_CreateBy",
                table: "ProductCards",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCards_DeletedBy",
                table: "ProductCards",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCards_ModifiedBy",
                table: "ProductCards",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCards_ProductId_SizeId_ColorId",
                table: "ProductCards",
                columns: new[] { "ProductId", "SizeId", "ColorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCards_SizeId",
                table: "ProductCards",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_CreateBy",
                table: "ProductImages",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_DeletedBy",
                table: "ProductImages",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ModifiedBy",
                table: "ProductImages",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreateBy",
                table: "Products",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeletedBy",
                table: "Products",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiedBy",
                table: "Products",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Slug",
                table: "Products",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Membership",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Membership",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_CreateBy",
                table: "Sizes",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_DeletedBy",
                table: "Sizes",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ModifiedBy",
                table: "Sizes",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreateBy",
                table: "Tags",
                column: "CreateBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DeletedBy",
                table: "Tags",
                column: "DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ModifiedBy",
                table: "Tags",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Membership",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Membership",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Membership",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Membership",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Membership",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "ContactPosts");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "ProductCards");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Membership");
        }
    }
}
