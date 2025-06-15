using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamUp.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentTemps",
                columns: table => new
                {
                    OrderCode = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTemps", x => x.OrderCode);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<float>(type: "float", nullable: true),
                    Weight = table.Column<float>(type: "float", nullable: true),
                    AvatarUrl = table.Column<string>(type: "longtext", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: true),
                    Specialty = table.Column<string>(type: "longtext", nullable: true),
                    Certificate = table.Column<string>(type: "longtext", nullable: true),
                    WorkingAddress = table.Column<string>(type: "longtext", nullable: true),
                    WorkingDate = table.Column<string>(type: "longtext", nullable: true),
                    Experience = table.Column<string>(type: "longtext", nullable: true),
                    TargetObject = table.Column<string>(type: "longtext", nullable: true),
                    PricePerSession = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusForCoach = table.Column<string>(type: "longtext", nullable: true),
                    CountForView = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    RefreshToken = table.Column<string>(type: "longtext", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    RevieweeId = table.Column<int>(type: "int", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_RevieweeId",
                        column: x => x.RevieweeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SportsComplexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    ImageUrlsJson = table.Column<string>(type: "longtext", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportsComplexes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SportsComplexes_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MessageContent = table.Column<string>(type: "longtext", nullable: false),
                    ChannelName = table.Column<string>(type: "longtext", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SportsComplexId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrlsJson = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courts_SportsComplexes_SportsComplexId",
                        column: x => x.SportsComplexId,
                        principalTable: "SportsComplexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoachBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false),
                    PaymentStatus = table.Column<string>(type: "longtext", nullable: false),
                    IsNotified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoachBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoachBookings_AspNetUsers_CoachId",
                        column: x => x.CoachId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoachBookings_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoachBookings_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoachBookings_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CourtBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false),
                    PaymentStatus = table.Column<string>(type: "longtext", nullable: false),
                    IsNotified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtBookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourtBookings_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourtBookings_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    RoomFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    ScheduledTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_AspNetUsers_HostId",
                        column: x => x.HostId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rooms_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CoachBookingId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_CoachBookings_CoachBookingId",
                        column: x => x.CoachBookingId,
                        principalTable: "CoachBookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourtBookingId = table.Column<int>(type: "int", nullable: true),
                    CoachBookingId = table.Column<int>(type: "int", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_CoachBookings_CoachBookingId",
                        column: x => x.CoachBookingId,
                        principalTable: "CoachBookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_CourtBookings_CourtBookingId",
                        column: x => x.CourtBookingId,
                        principalTable: "CourtBookings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoomJoinRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RespondedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomJoinRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomJoinRequests_AspNetUsers_RequesterId",
                        column: x => x.RequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomJoinRequests_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoomPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false),
                    IsNotified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomPlayers_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomPlayers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5830), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Quản trị viên", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5879), new TimeSpan(0, 7, 0, 0, 0)), "Admin", "ADMIN" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5885), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Người dùng thông thường", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5886), new TimeSpan(0, 7, 0, 0, 0)), "User", "USER" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5887), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Chủ sân thể thao", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5888), new TimeSpan(0, 7, 0, 0, 0)), "Owner", "OWNER" },
                    { 4, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5889), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Coach / Trainer", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(5890), new TimeSpan(0, 7, 0, 0, 0)), "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "AvatarUrl", "Certificate", "ConcurrencyStamp", "CountForView", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "Experience", "ExpireDate", "FullName", "Height", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PackageId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PricePerSession", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Specialty", "StartDate", "Status", "StatusForCoach", "TargetObject", "TwoFactorEnabled", "Type", "UserName", "Weight", "WorkingAddress", "WorkingDate" },
                values: new object[,]
                {
                    { 1, 0, null, null, null, "5bf67093-08ea-41c0-bdc2-b4ef9b8cc3f8", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(6102), new TimeSpan(0, 7, 0, 0, 0)), null, null, "admin@teamup.com", true, null, null, "System Admin", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 208, DateTimeKind.Unspecified).AddTicks(6103), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@TEAMUP.COM", "ADMIN", null, "AQAAAAIAAYagAAAAEPhH/oy4XhipfMCz6bMzRMgobTnzJIeG3lrgZQjFfh00ei68YqgrYueKDCseJEuP2A==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2aa06976-1232-4a64-96f5-82b67fc91dfd", null, null, 1, null, null, false, null, "admin", null, null, null },
                    { 2, 0, null, null, null, "24e3a9bc-c8b3-4d69-b3b2-cb63f8478e4a", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 268, DateTimeKind.Unspecified).AddTicks(1767), new TimeSpan(0, 7, 0, 0, 0)), null, null, "player@teamup.com", true, null, null, "Người Chơi A", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 268, DateTimeKind.Unspecified).AddTicks(1810), new TimeSpan(0, 7, 0, 0, 0)), false, null, "PLAYER@TEAMUP.COM", "PLAYER", null, "AQAAAAIAAYagAAAAEJZ29RR301/XjBu/5CipuaGBM3e+ANO+Dp9M0tw7jFSOIvQHuPJPv0P+IXHsQmtBKg==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "e42fbf81-5bd6-41b3-9b60-3437c8d19f0d", null, null, 1, null, null, false, null, "player", null, null, null },
                    { 3, 0, null, null, null, "eec0dad3-c657-44d5-9618-b2b07d95a0f2", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 329, DateTimeKind.Unspecified).AddTicks(4718), new TimeSpan(0, 7, 0, 0, 0)), null, null, "chusan@teamup.com", true, null, null, "Chủ Sân A", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 329, DateTimeKind.Unspecified).AddTicks(4732), new TimeSpan(0, 7, 0, 0, 0)), false, null, "CHUSAN@TEAMUP.COM", "CHUSAN", null, "AQAAAAIAAYagAAAAEL5CrsHyUZrrVBWvgj7CxDs3tZWdYScM1lO62FHVzxEs9rC51dffnXVqvKSPRxkimA==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "3c885ee5-8b3c-45ba-be1f-f3de5679cea2", null, null, 1, null, null, false, null, "chusan", null, null, null },
                    { 4, 0, null, null, "Chứng chỉ A", "0b2b2acd-2093-446c-ba3a-9cbfe7b92b34", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 393, DateTimeKind.Unspecified).AddTicks(3924), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV B", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 393, DateTimeKind.Unspecified).AddTicks(3943), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH@TEAMUP.COM", "COACH", null, "AQAAAAIAAYagAAAAEItfDw3h+46SF+HenBbsMdWjmIgvBqES550k8biVJqGJS1/BTKsfmGsvMop0+HhNwg==", null, false, 5000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "699bb3bf-270e-4bd2-93f3-180ec5a75c6b", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach", null, "Sân ABC, Quận 1", "Thứ 2, 4, 6" },
                    { 5, 0, null, null, "Chứng chỉ B", "03c03dc4-46e2-43bc-97fc-596379c01e76", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2164), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach1@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV 1", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2182), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH1@TEAMUP.COM", "COACH1", null, "AQAAAAIAAYagAAAAEFD3T6raodzRHXTIRrrpJg0CrYxBjOUjIH4v+hZipR5zPS6dufN24TFsLU99OuGDxw==", null, false, 250000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ed7884c1-3f5a-44a8-bf74-24b11aa605f6", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach1", null, "Sân XYZ, Quận 5", "Thứ 3, 5" },
                    { 6, 0, null, null, "Chứng chỉ C", "3c746c4c-b183-4e8b-8455-901c8c5942ab", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2345), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach2@teamup.com", true, "3 năm huấn luyện cá nhân và nhóm", null, "HLV 2", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2346), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH2@TEAMUP.COM", "COACH2", null, "AQAAAAIAAYagAAAAEC0GDqhuqTYxAEcY1CMQx+sc60LMCSLLF5/Xh5bBrN122cU8EvtaTlF9h0epwfkmqw==", null, false, 180000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "09796285-2350-4311-b2ff-b11b2d6e5800", "Cầu lông", null, 1, "Active", "Người lớn, học sinh", false, "Cầu lông", "coach2", null, "Sân Lông, Quận 2", "Thứ 2, 4" },
                    { 7, 0, null, null, "Chứng chỉ D", "30f50b9c-4184-4d7d-8ae5-14c69a62f2b6", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2362), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach3@teamup.com", true, "2 năm giảng dạy cho người mới bắt đầu", null, "HLV 3", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2364), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH3@TEAMUP.COM", "COACH3", null, "AQAAAAIAAYagAAAAEP311OiXCBHJpeqyr6Xt8s4bpBxwluASAO6JBVDuHHCCkwZieU8C54pke+VOuBZQKg==", null, false, 220000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "7610329c-1986-4856-8e62-f36c4b169f8f", "Pickleball", null, 1, "Active", "Người mới chơi, người cao tuổi", false, "Pickleball", "coach3", null, "Sân PB, Quận 7", "Thứ 6, 7" },
                    { 8, 0, null, null, "Chứng chỉ A", "3acbe71c-d645-47b6-af66-3c9300359e15", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2372), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach4@teamup.com", true, "8 năm làm HLV cho các đội phong trào", null, "HLV 4", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2373), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH4@TEAMUP.COM", "COACH4", null, "AQAAAAIAAYagAAAAEPwdseTnWvKb0sLTSkfkGs/tA2JxFVpkkRMhL/855lmJPegpN6VPcJWvoGmajZ0F/w==", null, false, 230000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "2b2c3f64-4f76-4a2b-a0cf-a865602a2fe4", "Bóng đá", null, 1, "Active", "Người lớn, sinh viên", false, "Bóng đá", "coach4", null, "Sân K, Quận 6", "Thứ 3, 6" },
                    { 9, 0, null, null, "Chứng chỉ B", "3cfe5722-b7ea-4efe-858e-92368abc80a7", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2380), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach5@teamup.com", true, "4 năm giảng dạy tại trung tâm thể thao", null, "HLV 5", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2381), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH5@TEAMUP.COM", "COACH5", null, "AQAAAAIAAYagAAAAEBcLUauqB6tkso2mR0z4ll4HyACxBNmzNsmRKrFPnJ738aUzEFweCRwQypKJhcCTPg==", null, false, 190000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "4c5afbaf-1b8e-4664-b498-383634fb52ab", "Cầu lông", null, 1, "Active", "Thiếu nhi, người đi làm", false, "Cầu lông", "coach5", null, "Sân Mây, Quận 10", "Thứ 2, 5" },
                    { 10, 0, null, null, "Chứng chỉ E", "d608c298-bd5a-4888-b831-4082a83a0f89", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2389), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach6@teamup.com", true, "1 năm hỗ trợ luyện tập cơ bản và thi đấu", null, "HLV 6", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2390), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH6@TEAMUP.COM", "COACH6", null, "AQAAAAIAAYagAAAAEBeXc7ieVWvGTW3hoQezQbJLvTZ8ljmRpfMEuYfGM1FAFA5jI2AZzWrCGE5cLRGpwQ==", null, false, 210000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "a85b7140-6644-430d-a974-8925b629e4fd", "Pickleball", null, 1, "Active", "Người cao tuổi, học viên nữ", false, "Pickleball", "coach6", null, "Sân Pick, Quận 9", "Thứ 4, 7" },
                    { 11, 0, null, null, "Chứng chỉ C", "83143d16-9925-42ce-b484-6ca4ad03f6c3", 0, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2400), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach7@teamup.com", true, "6 năm giảng dạy các lớp nâng cao", null, "HLV 7", null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 458, DateTimeKind.Unspecified).AddTicks(2401), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH7@TEAMUP.COM", "COACH7", null, "AQAAAAIAAYagAAAAENLU/zFRDTL766N+QU9xHlTxUEVxLNuBEZMPgXBVF/zGtfFvsCZIzcddh2SuseHM9A==", null, false, 240000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "91d95862-604d-45e9-b033-0755da061acf", "Bóng đá", null, 1, "Active", "Học viên đã có nền tảng", false, "Bóng đá", "coach7", null, "Sân Gold, Quận Tân Bình", "Thứ 3, 5, 7" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DurationDays", "LastUpdatedBy", "LastUpdatedTime", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3244), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 30 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3274), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 10000m, "PackageHLV" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3287), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói cao cấp 90 ngày", 90, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3288), new TimeSpan(0, 7, 0, 0, 0)), "Premium", 10000m, "PackageHLV" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3290), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 30 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3291), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 10000m, "PackageHLV" }
                });

            migrationBuilder.InsertData(
                table: "Voucher",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DiscountPercent", "LastUpdatedBy", "LastUpdatedTime" },
                values: new object[,]
                {
                    { 1, "VOUCHER1", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7355), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Giảm 10% cho booking đầu tiên", 10, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7356), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, "VOUCHER2", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7359), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Hỗ trợ website giảm 5%", 5, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7360), new TimeSpan(0, 7, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 7 },
                    { 4, 8 },
                    { 4, 9 },
                    { 4, 10 },
                    { 4, 11 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Comment", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "RatingValue", "RevieweeId", "ReviewerId" },
                values: new object[,]
                {
                    { 1, "HLV rất chuyên nghiệp, hướng dẫn tận tình.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6947), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6949), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 2, "Chủ sân thân thiện, sân sạch đẹp.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6953), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6954), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 3, "Chủ sân hỗ trợ rất nhiệt tình và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6955), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6956), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 4, "Không gian rộng rãi, dễ đặt lịch.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6958), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6959), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 5, "Thỉnh thoảng hơi chậm phản hồi tin nhắn.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6960), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6961), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 4 },
                    { 6, "Chủ sân dễ tính, rất dễ thương!", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6962), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6963), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 4 },
                    { 7, "Sân tốt, chủ sân chu đáo.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6965), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6966), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 8, "Quản lý chuyên nghiệp, xử lý tình huống nhanh chóng.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6967), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6968), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 9, "Dịch vụ ổn, sẽ quay lại lần nữa.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6969), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6970), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 1 },
                    { 10, "Chủ sân rất thân thiện, đáng tin cậy.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6971), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6972), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 11, "Cần cải thiện thời gian mở cửa đúng giờ hơn.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6974), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6975), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 1 },
                    { 12, "Chất lượng phục vụ tuyệt vời!", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6976), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6977), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 13, "HLV rất tâm huyết và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7012), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7013), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 14, "Giảng dạy dễ hiểu, thái độ thân thiện.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7018), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7019), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 3 },
                    { 15, "Tận tình hỗ trợ, kỹ năng tốt.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7020), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7021), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 5 },
                    { 16, "Cực kỳ có trách nhiệm với học viên.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7023), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7024), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 6 },
                    { 17, "Phương pháp huấn luyện rõ ràng.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7025), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7026), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 2 },
                    { 18, "Đúng giờ, vui vẻ và tận tâm.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7027), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7028), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 3 },
                    { 19, "Cải thiện kỹ năng rõ rệt sau vài buổi.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7030), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7031), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 6 },
                    { 20, "Kỹ năng truyền đạt tốt, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7032), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7033), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 7 },
                    { 21, "Cần tăng tính kỷ luật, nhưng kỹ năng ổn.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7034), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7035), new TimeSpan(0, 7, 0, 0, 0)), 3, 6, 2 },
                    { 22, "Nhiệt tình, vui vẻ, luôn động viên học viên.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7036), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7037), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 3 },
                    { 23, "Bài tập sáng tạo, dễ áp dụng.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7039), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7040), new TimeSpan(0, 7, 0, 0, 0)), 5, 6, 4 },
                    { 24, "Có chuyên môn cao, dễ tiếp cận.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7041), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7042), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 5 },
                    { 25, "Giúp tôi nâng cao thể lực rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7043), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7044), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 2 },
                    { 26, "Đào tạo bài bản, bài tập phù hợp trình độ.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7046), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7047), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 3 },
                    { 27, "Thời gian linh hoạt, hỗ trợ tốt.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7048), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7049), new TimeSpan(0, 7, 0, 0, 0)), 4, 7, 4 },
                    { 28, "Có kinh nghiệm thực tế, phong cách giảng dạy chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7050), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7051), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 6 },
                    { 29, "Khả năng truyền đạt tốt, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7052), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7053), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 2 },
                    { 30, "Kiến thức vững, giao tiếp tốt.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7055), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7056), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 3 },
                    { 31, "Luôn khuyến khích học viên cố gắng.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7057), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7058), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 4 },
                    { 32, "Cực kỳ chuyên nghiệp và dễ thương.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7059), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7060), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 5 },
                    { 33, "Bài giảng sáng tạo, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7089), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7090), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 2 },
                    { 34, "Có nhiều kinh nghiệm thực chiến.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7092), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7093), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 3 },
                    { 35, "Tận tâm với học viên, hỗ trợ thêm ngoài giờ.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7094), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7095), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 6 },
                    { 36, "Chuyên nghiệp, luôn đúng giờ.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7100), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7101), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 7 },
                    { 37, "Nội dung giảng dạy phù hợp từng người.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7102), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7103), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 2 },
                    { 38, "Tạo động lực cho học viên rất tốt.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7104), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7105), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 3 },
                    { 39, "Rất tận tình, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7107), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7108), new TimeSpan(0, 7, 0, 0, 0)), 4, 10, 4 },
                    { 40, "Phong cách dạy chuyên nghiệp và hiệu quả.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7109), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 5 },
                    { 41, "HLV dày dạn kinh nghiệm, đáng học hỏi.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7111), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7112), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 2 },
                    { 42, "Dạy dễ hiểu, luôn hỗ trợ đúng lúc.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7114), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7115), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 3 },
                    { 43, "Phong thái chuyên nghiệp, vui vẻ.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7116), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7117), new TimeSpan(0, 7, 0, 0, 0)), 4, 11, 6 },
                    { 44, "Giúp tôi cải thiện kỹ thuật rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7118), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7119), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 7 }
                });

            migrationBuilder.InsertData(
                table: "SportsComplexes",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "ImageUrlsJson", "LastUpdatedBy", "LastUpdatedTime", "Latitude", "Longitude", "Name", "OwnerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "Sân bóng đá Tao Đàn, 1 Huyền Trân Công Chúa, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3513), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(3514), new TimeSpan(0, 7, 0, 0, 0)), 10.773444, 106.690933, "Khu Thể Thao ABC", 3, "Active", "Bóng đá" },
                    { 2, "Sân cầu lông Hồ Kỳ Hòa, 27 Cao Thắng, Quận 3, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4345), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4346), new TimeSpan(0, 7, 0, 0, 0)), 10.768492999999999, 106.681771, "Khu Thể Thao DEF", 3, "Active", "Cầu lông" },
                    { 3, "Sân thể thao Rạch Miễu, 1 Hoa Phượng, Phú Nhuận, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4407), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4414), new TimeSpan(0, 7, 0, 0, 0)), 10.800005000000001, 106.683813, "Khu Thể Thao GHI", 3, "Active", "Pickleball" },
                    { 4, "Sân thể thao Vạn Tường, 59A Nguyễn Du, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4577), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4578), new TimeSpan(0, 7, 0, 0, 0)), 10.77623, 106.699208, "Khu Thể Thao JKL", 3, "Active", "Pickleball" },
                    { 5, "Sân cầu lông Quận 6, 42 Nguyễn Văn Luông, Quận 6, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4686), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4687), new TimeSpan(0, 7, 0, 0, 0)), 10.737717, 106.62858199999999, "Khu Thể Thao MNO", 3, "Active", "Cầu lông" }
                });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "ImageUrlsJson", "LastUpdatedBy", "LastUpdatedTime", "Name", "PricePerHour", "SportsComplexId", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4945), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân cỏ nhân tạo tiêu chuẩn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(4946), new TimeSpan(0, 7, 0, 0, 0)), "Sân 5 người A", 10000m, 1, "Active" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5082), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chất lượng cao, đèn chiếu sáng ban đêm", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5083), new TimeSpan(0, 7, 0, 0, 0)), "Sân 7 người B", 350000m, 1, "Active" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5191), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5192), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông A", 150000m, 2, "Active" },
                    { 4, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5501), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn phong trào", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5502), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông B", 100000m, 2, "Active" },
                    { 5, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5650), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, chất lượng cao", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5651), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball A", 250000m, 3, "Active" },
                    { 6, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5765), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, có mái che", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5766), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball B", 300000m, 3, "Active" },
                    { 7, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5896), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn FIBA, sàn gỗ cao cấp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5897), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball C", 400000m, 4, "Active" },
                    { 8, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5995), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân phong trào, phù hợp nhóm bạn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(5997), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball D", 250000m, 4, "Active" },
                    { 9, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6091), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân luyện tập cá nhân", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6092), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball E", 200000m, 4, "Active" },
                    { 10, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6281), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6282), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông C", 300000m, 5, "Active" },
                    { 11, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6449), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, thoáng mát", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6450), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông D", 200000m, 5, "Active" },
                    { 12, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6546), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân thi đấu chuyên nghiệp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6547), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông E", 350000m, 5, "Active" },
                    { 13, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6640), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân tiêu chuẩn quốc tế", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6641), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball F", 280000m, 3, "Active" },
                    { 14, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6765), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân mở ban đêm, có đèn chiếu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(6767), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông F", 270000m, 4, "Active" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CourtId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "HostId", "LastUpdatedBy", "LastUpdatedTime", "MaxPlayers", "Name", "RoomFee", "ScheduledTime", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7225), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tập hợp anh em giao lưu bóng đá sáng thứ 7.", 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7226), new TimeSpan(0, 7, 0, 0, 0)), 10, "Team Sáng Thứ 7", 30000m, new DateTime(2025, 6, 16, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 2, 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7261), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm đối đá giao hữu 7v7 buổi tối.", 3, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7262), new TimeSpan(0, 7, 0, 0, 0)), 14, "Giao lưu buổi tối", 50000m, new DateTime(2025, 6, 17, 20, 0, 0, 0, DateTimeKind.Local), "Completed" },
                    { 3, 3, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7265), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đánh cầu cuối tuần, vui vẻ là chính.", 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7266), new TimeSpan(0, 7, 0, 0, 0)), 4, "Badminton Team CN", 20000m, new DateTime(2025, 6, 18, 9, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 4, 4, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7269), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm team giao lưu vào chiều thứ 5.", 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7270), new TimeSpan(0, 7, 0, 0, 0)), 10, "Đá bóng chiều thứ 5", 30000m, new DateTime(2025, 6, 19, 17, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 5, 5, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7272), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Pickleball nhẹ nhàng chủ nhật.", 3, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7273), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball sáng CN", 40000m, new DateTime(2025, 6, 20, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 6, 6, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7276), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Giao lưu Pickleball chiều cuối tuần.", 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7277), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball chiều thứ 7", 50000m, new DateTime(2025, 6, 21, 15, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 7, 7, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7280), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team Pickleball tụ tập tối thứ 3.", 3, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7281), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball tối thứ 3", 25000m, new DateTime(2025, 6, 18, 20, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 8, 8, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7284), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Vui là chính, ai cũng có thể tham gia.", 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7285), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball phong trào", 20000m, new DateTime(2025, 6, 17, 18, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 9, 10, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7287), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đội hình luyện tập chuẩn bị giải.", 3, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7288), new TimeSpan(0, 7, 0, 0, 0)), 12, "Cầu lông tập luyện", 35000m, new DateTime(2025, 6, 16, 16, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 10, 12, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7291), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team cầu lông nhẹ nhàng tối làm về.", 2, null, new DateTimeOffset(new DateTime(2025, 6, 15, 19, 27, 38, 895, DateTimeKind.Unspecified).AddTicks(7292), new TimeSpan(0, 7, 0, 0, 0)), 4, "Chơi cầu lông tối", 25000m, new DateTime(2025, 6, 19, 19, 0, 0, 0, DateTimeKind.Local), "Waiting" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "IX_AspNetUsers_PackageId",
                table: "AspNetUsers",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoachBookings_CoachId",
                table: "CoachBookings",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachBookings_CourtId",
                table: "CoachBookings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachBookings_PlayerId",
                table: "CoachBookings",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_CoachBookings_VoucherId",
                table: "CoachBookings",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtBookings_CourtId",
                table: "CourtBookings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtBookings_UserId",
                table: "CourtBookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtBookings_VoucherId",
                table: "CourtBookings",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Courts_SportsComplexId",
                table: "Courts",
                column: "SportsComplexId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CoachBookingId",
                table: "Payments",
                column: "CoachBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CourtBookingId",
                table: "Payments",
                column: "CourtBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PackageId",
                table: "Payments",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RevieweeId",
                table: "Ratings",
                column: "RevieweeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ReviewerId",
                table: "Ratings",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomJoinRequests_RequesterId",
                table: "RoomJoinRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomJoinRequests_RoomId",
                table: "RoomJoinRequests",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayers_PlayerId",
                table: "RoomPlayers",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlayers_RoomId",
                table: "RoomPlayers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CourtId",
                table: "Rooms",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HostId",
                table: "Rooms",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_CoachBookingId",
                table: "Slots",
                column: "CoachBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_SportsComplexes_OwnerId",
                table: "SportsComplexes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_RecipientId",
                table: "UserMessages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_SenderId",
                table: "UserMessages",
                column: "SenderId");
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
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentTemps");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RoomJoinRequests");

            migrationBuilder.DropTable(
                name: "RoomPlayers");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CourtBookings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "CoachBookings");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "SportsComplexes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
