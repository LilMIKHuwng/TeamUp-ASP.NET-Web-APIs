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
                    { 1, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6187), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Quản trị viên", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6242), new TimeSpan(0, 7, 0, 0, 0)), "Admin", "ADMIN" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6255), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Người dùng thông thường", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6256), new TimeSpan(0, 7, 0, 0, 0)), "User", "USER" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6259), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Chủ sân thể thao", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6260), new TimeSpan(0, 7, 0, 0, 0)), "Owner", "OWNER" },
                    { 4, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6262), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Coach / Trainer", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6264), new TimeSpan(0, 7, 0, 0, 0)), "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "AvatarUrl", "Certificate", "ConcurrencyStamp", "CountForView", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "Experience", "ExpireDate", "FullName", "Height", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PackageId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PricePerSession", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Specialty", "StartDate", "Status", "StatusForCoach", "TargetObject", "TwoFactorEnabled", "Type", "UserName", "Weight", "WorkingAddress", "WorkingDate" },
                values: new object[,]
                {
                    { 1, 0, null, null, null, "13a7d9f2-fa9e-4034-bce6-8661a01362e5", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6773), new TimeSpan(0, 7, 0, 0, 0)), null, null, "admin@teamup.com", true, null, null, "System Admin", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 60, DateTimeKind.Unspecified).AddTicks(6777), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@TEAMUP.COM", "ADMIN", null, "AQAAAAIAAYagAAAAECx2SDtXNhGDveIMjg8xEVS6cuIs73wQuY2+gCIEO9U08CEfqsMPBztbyuObvEy1iQ==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "048e71a4-07e0-466b-be13-75b0104f31f8", null, null, 1, null, null, false, null, "admin", null, null, null },
                    { 2, 0, null, null, null, "a170aee3-2847-4a4f-991e-8f05c2467caf", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 165, DateTimeKind.Unspecified).AddTicks(7276), new TimeSpan(0, 7, 0, 0, 0)), null, null, "player@teamup.com", true, null, null, "Người Chơi A", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 165, DateTimeKind.Unspecified).AddTicks(7328), new TimeSpan(0, 7, 0, 0, 0)), false, null, "PLAYER@TEAMUP.COM", "PLAYER", null, "AQAAAAIAAYagAAAAEBMiA4ISQUm1puyA5TRpimtN6yOSUBcJQVVnf/Ugk7tI/IBuXiZ7RlRVLqjGfQMBZg==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "e694b4bf-3436-474a-bc3f-2f9e872330bc", null, null, 1, null, null, false, null, "player", null, null, null },
                    { 3, 0, null, null, null, "dc6a10c1-5e96-44ce-94b5-75bc00e73595", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 256, DateTimeKind.Unspecified).AddTicks(4745), new TimeSpan(0, 7, 0, 0, 0)), null, null, "chusan@teamup.com", true, null, null, "Chủ Sân A", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 256, DateTimeKind.Unspecified).AddTicks(4781), new TimeSpan(0, 7, 0, 0, 0)), false, null, "CHUSAN@TEAMUP.COM", "CHUSAN", null, "AQAAAAIAAYagAAAAEMPdUQJ4MoCwVIzKBHfsIjAbuClyYGvIS7/u1KofM4dhKGgqgiIPMwgMRqUEbn4Bhw==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "3b5cc661-786e-42d8-a97e-4e15319b6d3c", null, null, 1, null, null, false, null, "chusan", null, null, null },
                    { 4, 0, null, null, "Chứng chỉ A", "28d1680f-f76b-4aa5-ac26-5e7b091a570e", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 349, DateTimeKind.Unspecified).AddTicks(7942), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV B", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 349, DateTimeKind.Unspecified).AddTicks(7971), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH@TEAMUP.COM", "COACH", null, "AQAAAAIAAYagAAAAEAjYS3PRgcoSUhJgZsGKiAZh39GS8XErTyQT1mCEfhL55UOST5dX3W2rFMirD868Bw==", null, false, 5000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "227c888d-2cae-4e90-a4ce-2ffcb20b0ce7", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach", null, "Sân ABC, Quận 1", "Thứ 2, 4, 6" },
                    { 5, 0, null, null, "Chứng chỉ B", "29b1666a-692c-4c44-af14-434ec764066a", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(7871), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach1@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV 1", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(7889), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH1@TEAMUP.COM", "COACH1", null, "AQAAAAIAAYagAAAAEIcDJkEZUOu+P8XPrHOybvESGeTnPNZ33UIojrtpZPcsynR4MW7bc2jaHxoHAGaLag==", null, false, 250000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "1a8bf453-9d5a-4301-b560-ed431b386085", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach1", null, "Sân XYZ, Quận 5", "Thứ 3, 5" },
                    { 6, 0, null, null, "Chứng chỉ C", "c2e496ba-7571-4c25-947f-0e241dc42990", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(7974), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach2@teamup.com", true, "3 năm huấn luyện cá nhân và nhóm", null, "HLV 2", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(7976), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH2@TEAMUP.COM", "COACH2", null, "AQAAAAIAAYagAAAAED61jNSmCxXMc8YhhAyKH9OW/xBVuxseS+bru5mvHleH0HCKShLaouUdjnssB5Rwiw==", null, false, 180000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "81cf71de-0c33-447d-a82f-100efbb47bf6", "Cầu lông", null, 1, "Active", "Người lớn, học sinh", false, "Cầu lông", "coach2", null, "Sân Lông, Quận 2", "Thứ 2, 4" },
                    { 7, 0, null, null, "Chứng chỉ D", "b618499e-9732-4d61-8723-08de8aa150ae", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(7986), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach3@teamup.com", true, "2 năm giảng dạy cho người mới bắt đầu", null, "HLV 3", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(7987), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH3@TEAMUP.COM", "COACH3", null, "AQAAAAIAAYagAAAAEEwFRYhO/KN53KHeQ17R4VBYIbb8pAa2xo1MR8AMtTsMWIEl1Nmo4t0NNUarkt6cvQ==", null, false, 220000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "f68d5cb5-d099-43b5-bbfd-b4aba6924eb3", "Pickleball", null, 1, "Active", "Người mới chơi, người cao tuổi", false, "Pickleball", "coach3", null, "Sân PB, Quận 7", "Thứ 6, 7" },
                    { 8, 0, null, null, "Chứng chỉ A", "eee33fb4-490e-406f-a9e8-1c1ffe72880e", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8009), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach4@teamup.com", true, "8 năm làm HLV cho các đội phong trào", null, "HLV 4", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8011), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH4@TEAMUP.COM", "COACH4", null, "AQAAAAIAAYagAAAAEHaDXjBK5FxR8+24+gOY+2m7N3AOS5cDNsG9EeoK7jYms+l0jV9CixRmwscCpD0h5g==", null, false, 230000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ed6b98d7-2cbf-47f9-a1dc-206b0dd65d1c", "Bóng đá", null, 1, "Active", "Người lớn, sinh viên", false, "Bóng đá", "coach4", null, "Sân K, Quận 6", "Thứ 3, 6" },
                    { 9, 0, null, null, "Chứng chỉ B", "5b9371f3-e412-4c4d-8f6f-9c0b7517ae93", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8106), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach5@teamup.com", true, "4 năm giảng dạy tại trung tâm thể thao", null, "HLV 5", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8107), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH5@TEAMUP.COM", "COACH5", null, "AQAAAAIAAYagAAAAEBiA53dFZUt5GkRldp10Km2VIhwoeWodtu69qSdVt+/NAUtpLmd/37xVwNfJ/gLPOw==", null, false, 190000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "e7c900a6-ff7b-4af3-9be6-0ddb0fc9c59f", "Cầu lông", null, 1, "Active", "Thiếu nhi, người đi làm", false, "Cầu lông", "coach5", null, "Sân Mây, Quận 10", "Thứ 2, 5" },
                    { 10, 0, null, null, "Chứng chỉ E", "2895e660-a095-4aa0-a90f-a0e6ec8245c8", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8118), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach6@teamup.com", true, "1 năm hỗ trợ luyện tập cơ bản và thi đấu", null, "HLV 6", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8119), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH6@TEAMUP.COM", "COACH6", null, "AQAAAAIAAYagAAAAEG1Q7qK0MD5cvHKE2PZOs1dkLRFRxDKZBiZk3iBf/H/lQDpJAuUJacGIGZ5JXoXXIw==", null, false, 210000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "e0b56e34-42a5-45ad-a414-f4e9a38abb0d", "Pickleball", null, 1, "Active", "Người cao tuổi, học viên nữ", false, "Pickleball", "coach6", null, "Sân Pick, Quận 9", "Thứ 4, 7" },
                    { 11, 0, null, null, "Chứng chỉ C", "e4f6af87-c080-49e0-8a51-3a22d633d31d", 0, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8127), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach7@teamup.com", true, "6 năm giảng dạy các lớp nâng cao", null, "HLV 7", null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 426, DateTimeKind.Unspecified).AddTicks(8128), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH7@TEAMUP.COM", "COACH7", null, "AQAAAAIAAYagAAAAEFxJAuVwJm/c/4PzUo+jH68KPsPXmHz3fL0ydsGq5ChTSlfWiuQ9honaz2gnBhSprw==", null, false, 240000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "bfa7b0e2-6372-45b1-ad03-c3cd67a54847", "Bóng đá", null, 1, "Active", "Học viên đã có nền tảng", false, "Bóng đá", "coach7", null, "Sân Gold, Quận Tân Bình", "Thứ 3, 5, 7" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DurationDays", "LastUpdatedBy", "LastUpdatedTime", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9649), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 30 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9665), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 199000m, "PackageHLV" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9676), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói cao cấp 90 ngày", 90, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9677), new TimeSpan(0, 7, 0, 0, 0)), "Premium", 399000m, "PackageHLV" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9679), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 30 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9680), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 159000m, "PackageHLV" }
                });

            migrationBuilder.InsertData(
                table: "Voucher",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DiscountPercent", "LastUpdatedBy", "LastUpdatedTime" },
                values: new object[,]
                {
                    { 1, "VOUCHER1", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3354), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Giảm 10% cho booking đầu tiên", 10, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3355), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, "VOUCHER2", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3359), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Hỗ trợ website giảm 5%", 5, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3360), new TimeSpan(0, 7, 0, 0, 0)) }
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
                    { 1, "HLV rất chuyên nghiệp, hướng dẫn tận tình.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2980), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2981), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 2, "Chủ sân thân thiện, sân sạch đẹp.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2985), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2986), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 3, "Chủ sân hỗ trợ rất nhiệt tình và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2988), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2989), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 4, "Không gian rộng rãi, dễ đặt lịch.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2990), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2991), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 5, "Thỉnh thoảng hơi chậm phản hồi tin nhắn.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2992), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2993), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 4 },
                    { 6, "Chủ sân dễ tính, rất dễ thương!", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2994), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2995), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 4 },
                    { 7, "Sân tốt, chủ sân chu đáo.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2996), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2997), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 8, "Quản lý chuyên nghiệp, xử lý tình huống nhanh chóng.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2999), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2999), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 9, "Dịch vụ ổn, sẽ quay lại lần nữa.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3001), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3002), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 1 },
                    { 10, "Chủ sân rất thân thiện, đáng tin cậy.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3003), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3004), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 11, "Cần cải thiện thời gian mở cửa đúng giờ hơn.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3005), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3006), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 1 },
                    { 12, "Chất lượng phục vụ tuyệt vời!", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3007), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3008), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 13, "HLV rất tâm huyết và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3041), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3042), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 14, "Giảng dạy dễ hiểu, thái độ thân thiện.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3045), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3046), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 3 },
                    { 15, "Tận tình hỗ trợ, kỹ năng tốt.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3048), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3048), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 5 },
                    { 16, "Cực kỳ có trách nhiệm với học viên.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3050), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3051), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 6 },
                    { 17, "Phương pháp huấn luyện rõ ràng.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3052), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3053), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 2 },
                    { 18, "Đúng giờ, vui vẻ và tận tâm.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3054), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3055), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 3 },
                    { 19, "Cải thiện kỹ năng rõ rệt sau vài buổi.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3056), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3057), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 6 },
                    { 20, "Kỹ năng truyền đạt tốt, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3058), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3059), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 7 },
                    { 21, "Cần tăng tính kỷ luật, nhưng kỹ năng ổn.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3060), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3061), new TimeSpan(0, 7, 0, 0, 0)), 3, 6, 2 },
                    { 22, "Nhiệt tình, vui vẻ, luôn động viên học viên.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3063), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3063), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 3 },
                    { 23, "Bài tập sáng tạo, dễ áp dụng.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3065), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3066), new TimeSpan(0, 7, 0, 0, 0)), 5, 6, 4 },
                    { 24, "Có chuyên môn cao, dễ tiếp cận.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3067), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3068), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 5 },
                    { 25, "Giúp tôi nâng cao thể lực rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3069), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3070), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 2 },
                    { 26, "Đào tạo bài bản, bài tập phù hợp trình độ.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3071), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3072), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 3 },
                    { 27, "Thời gian linh hoạt, hỗ trợ tốt.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3073), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3074), new TimeSpan(0, 7, 0, 0, 0)), 4, 7, 4 },
                    { 28, "Có kinh nghiệm thực tế, phong cách giảng dạy chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3075), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3076), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 6 },
                    { 29, "Khả năng truyền đạt tốt, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3077), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3078), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 2 },
                    { 30, "Kiến thức vững, giao tiếp tốt.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3080), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3080), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 3 },
                    { 31, "Luôn khuyến khích học viên cố gắng.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3082), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3083), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 4 },
                    { 32, "Cực kỳ chuyên nghiệp và dễ thương.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3084), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3085), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 5 },
                    { 33, "Bài giảng sáng tạo, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3086), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3087), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 2 },
                    { 34, "Có nhiều kinh nghiệm thực chiến.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3088), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3089), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 3 },
                    { 35, "Tận tâm với học viên, hỗ trợ thêm ngoài giờ.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3090), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3091), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 6 },
                    { 36, "Chuyên nghiệp, luôn đúng giờ.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3094), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3094), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 7 },
                    { 37, "Nội dung giảng dạy phù hợp từng người.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3096), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3097), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 2 },
                    { 38, "Tạo động lực cho học viên rất tốt.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3098), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3099), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 3 },
                    { 39, "Rất tận tình, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3100), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3101), new TimeSpan(0, 7, 0, 0, 0)), 4, 10, 4 },
                    { 40, "Phong cách dạy chuyên nghiệp và hiệu quả.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3102), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3103), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 5 },
                    { 41, "HLV dày dạn kinh nghiệm, đáng học hỏi.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3104), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3105), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 2 },
                    { 42, "Dạy dễ hiểu, luôn hỗ trợ đúng lúc.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3106), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3107), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 3 },
                    { 43, "Phong thái chuyên nghiệp, vui vẻ.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3108), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3109), new TimeSpan(0, 7, 0, 0, 0)), 4, 11, 6 },
                    { 44, "Giúp tôi cải thiện kỹ thuật rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3110), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3111), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 7 }
                });

            migrationBuilder.InsertData(
                table: "SportsComplexes",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "ImageUrlsJson", "LastUpdatedBy", "LastUpdatedTime", "Latitude", "Longitude", "Name", "OwnerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "Sân bóng đá Tao Đàn, 1 Huyền Trân Công Chúa, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9835), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 870, DateTimeKind.Unspecified).AddTicks(9836), new TimeSpan(0, 7, 0, 0, 0)), 10.773444, 106.690933, "Khu Thể Thao ABC", 3, "Active", "Bóng đá" },
                    { 2, "Sân cầu lông Hồ Kỳ Hòa, 27 Cao Thắng, Quận 3, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(497), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(498), new TimeSpan(0, 7, 0, 0, 0)), 10.768492999999999, 106.681771, "Khu Thể Thao DEF", 3, "Active", "Cầu lông" },
                    { 3, "Sân thể thao Rạch Miễu, 1 Hoa Phượng, Phú Nhuận, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(556), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(564), new TimeSpan(0, 7, 0, 0, 0)), 10.800005000000001, 106.683813, "Khu Thể Thao GHI", 3, "Active", "Pickleball" },
                    { 4, "Sân thể thao Vạn Tường, 59A Nguyễn Du, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(708), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(709), new TimeSpan(0, 7, 0, 0, 0)), 10.77623, 106.699208, "Khu Thể Thao JKL", 3, "Active", "Pickleball" },
                    { 5, "Sân cầu lông Quận 6, 42 Nguyễn Văn Luông, Quận 6, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(813), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(814), new TimeSpan(0, 7, 0, 0, 0)), 10.737717, 106.62858199999999, "Khu Thể Thao MNO", 3, "Active", "Cầu lông" }
                });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "ImageUrlsJson", "LastUpdatedBy", "LastUpdatedTime", "Name", "PricePerHour", "SportsComplexId", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(970), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân cỏ nhân tạo tiêu chuẩn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(971), new TimeSpan(0, 7, 0, 0, 0)), "Sân 5 người A", 10000m, 1, "Active" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1135), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chất lượng cao, đèn chiếu sáng ban đêm", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1136), new TimeSpan(0, 7, 0, 0, 0)), "Sân 7 người B", 350000m, 1, "Active" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1312), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1313), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông A", 150000m, 2, "Active" },
                    { 4, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1588), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn phong trào", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1589), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông B", 100000m, 2, "Active" },
                    { 5, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1725), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, chất lượng cao", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1726), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball A", 250000m, 3, "Active" },
                    { 6, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1902), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, có mái che", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(1903), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball B", 300000m, 3, "Active" },
                    { 7, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2033), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn FIBA, sàn gỗ cao cấp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2034), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball C", 400000m, 4, "Active" },
                    { 8, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2123), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân phong trào, phù hợp nhóm bạn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2124), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball D", 250000m, 4, "Active" },
                    { 9, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2212), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân luyện tập cá nhân", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2213), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball E", 200000m, 4, "Active" },
                    { 10, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2303), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2304), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông C", 300000m, 5, "Active" },
                    { 11, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2493), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, thoáng mát", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2494), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông D", 200000m, 5, "Active" },
                    { 12, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2587), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân thi đấu chuyên nghiệp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2588), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông E", 350000m, 5, "Active" },
                    { 13, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2676), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân tiêu chuẩn quốc tế", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2677), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball F", 280000m, 3, "Active" },
                    { 14, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2767), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân mở ban đêm, có đèn chiếu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(2768), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông F", 270000m, 4, "Active" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CourtId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "HostId", "LastUpdatedBy", "LastUpdatedTime", "MaxPlayers", "Name", "RoomFee", "ScheduledTime", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3235), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tập hợp anh em giao lưu bóng đá sáng thứ 7.", 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3236), new TimeSpan(0, 7, 0, 0, 0)), 10, "Team Sáng Thứ 7", 30000m, new DateTime(2025, 7, 19, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 2, 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3263), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm đối đá giao hữu 7v7 buổi tối.", 3, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3263), new TimeSpan(0, 7, 0, 0, 0)), 14, "Giao lưu buổi tối", 50000m, new DateTime(2025, 7, 20, 20, 0, 0, 0, DateTimeKind.Local), "Completed" },
                    { 3, 3, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3267), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đánh cầu cuối tuần, vui vẻ là chính.", 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3268), new TimeSpan(0, 7, 0, 0, 0)), 4, "Badminton Team CN", 20000m, new DateTime(2025, 7, 21, 9, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 4, 4, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3270), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm team giao lưu vào chiều thứ 5.", 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3271), new TimeSpan(0, 7, 0, 0, 0)), 10, "Đá bóng chiều thứ 5", 30000m, new DateTime(2025, 7, 22, 17, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 5, 5, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3274), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Pickleball nhẹ nhàng chủ nhật.", 3, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3274), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball sáng CN", 40000m, new DateTime(2025, 7, 23, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 6, 6, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3277), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Giao lưu Pickleball chiều cuối tuần.", 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3278), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball chiều thứ 7", 50000m, new DateTime(2025, 7, 24, 15, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 7, 7, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3281), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team Pickleball tụ tập tối thứ 3.", 3, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3282), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball tối thứ 3", 25000m, new DateTime(2025, 7, 21, 20, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 8, 8, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3285), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Vui là chính, ai cũng có thể tham gia.", 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3285), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball phong trào", 20000m, new DateTime(2025, 7, 20, 18, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 9, 10, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3290), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đội hình luyện tập chuẩn bị giải.", 3, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3291), new TimeSpan(0, 7, 0, 0, 0)), 12, "Cầu lông tập luyện", 35000m, new DateTime(2025, 7, 19, 16, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 10, 12, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3293), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team cầu lông nhẹ nhàng tối làm về.", 2, null, new DateTimeOffset(new DateTime(2025, 7, 18, 18, 8, 19, 871, DateTimeKind.Unspecified).AddTicks(3294), new TimeSpan(0, 7, 0, 0, 0)), 4, "Chơi cầu lông tối", 25000m, new DateTime(2025, 7, 22, 19, 0, 0, 0, DateTimeKind.Local), "Waiting" }
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
