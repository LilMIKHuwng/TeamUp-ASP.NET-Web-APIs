using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationDays = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTemps",
                columns: table => new
                {
                    OrderCode = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTemps", x => x.OrderCode);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<float>(type: "real", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetObject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerSession = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StatusForCoach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                        name: "FK_AspNetUsers_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
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
                });

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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    RevieweeId = table.Column<int>(type: "int", nullable: false),
                    RatingValue = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "SportsComplexes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    RecipientId = table.Column<int>(type: "int", nullable: false),
                    SendAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChannelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportsComplexId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "CoachBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNotified = table.Column<bool>(type: "bit", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "CourtBookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNotified = table.Column<bool>(type: "bit", nullable: false),
                    VoucherId = table.Column<int>(type: "int", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduledTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachBookingId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourtBookingId = table.Column<int>(type: "int", nullable: true),
                    CoachBookingId = table.Column<int>(type: "int", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "RoomJoinRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    RequesterId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RespondedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "RoomPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNotified = table.Column<bool>(type: "bit", nullable: false),
                    JoinedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DeletedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
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
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "LastUpdatedBy", "LastUpdatedTime", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8418), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Quản trị viên", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8441), new TimeSpan(0, 7, 0, 0, 0)), "Admin", "ADMIN" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8456), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Người dùng thông thường", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8458), new TimeSpan(0, 7, 0, 0, 0)), "User", "USER" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8461), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Chủ sân thể thao", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8462), new TimeSpan(0, 7, 0, 0, 0)), "Owner", "OWNER" },
                    { 4, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8466), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Coach / Trainer", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8467), new TimeSpan(0, 7, 0, 0, 0)), "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "AvatarUrl", "Certificate", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "Experience", "ExpireDate", "FullName", "Height", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PackageId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PricePerSession", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Specialty", "StartDate", "Status", "StatusForCoach", "TargetObject", "TwoFactorEnabled", "Type", "UserName", "Weight", "WorkingAddress", "WorkingDate" },
                values: new object[,]
                {
                    { 1, 0, null, null, null, "864e7c77-dc69-4d49-8e2c-5279fc75aa01", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8853), new TimeSpan(0, 7, 0, 0, 0)), null, null, "admin@teamup.com", true, null, null, "System Admin", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 459, DateTimeKind.Unspecified).AddTicks(8855), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@TEAMUP.COM", "ADMIN", null, "AQAAAAIAAYagAAAAEHQNhgiHLXF+LVtZNEX+NI1CdUoa6R8h+TTsxs6yDMZLmlBnxBU86g+Zd5phdoswpQ==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "b2fde84d-8b5f-44b4-b52a-1b7dfa66987e", null, null, 1, null, null, false, null, "admin", null, null, null },
                    { 2, 0, null, null, null, "900e2e2b-af7b-4dac-8f0d-54c739253a0c", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 545, DateTimeKind.Unspecified).AddTicks(3298), new TimeSpan(0, 7, 0, 0, 0)), null, null, "player@teamup.com", true, null, null, "Người Chơi A", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 545, DateTimeKind.Unspecified).AddTicks(3316), new TimeSpan(0, 7, 0, 0, 0)), false, null, "PLAYER@TEAMUP.COM", "PLAYER", null, "AQAAAAIAAYagAAAAEMXRDT9Ioz8k3b2Iw4kmgWvW5Qb3EKKDHaFKXE7qbZ3MY25o0zzsFzUm7r3JXEkKZA==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "49e9ac01-98f6-436a-bc43-429e298d78de", null, null, 1, null, null, false, null, "player", null, null, null },
                    { 3, 0, null, null, null, "a9c9941b-cea2-445d-b95c-bffe4d0815a4", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 638, DateTimeKind.Unspecified).AddTicks(9693), new TimeSpan(0, 7, 0, 0, 0)), null, null, "chusan@teamup.com", true, null, null, "Chủ Sân A", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 639, DateTimeKind.Unspecified).AddTicks(298), new TimeSpan(0, 7, 0, 0, 0)), false, null, "CHUSAN@TEAMUP.COM", "CHUSAN", null, "AQAAAAIAAYagAAAAEKyYSJSsE7X5Znj9+kiLr3btKC1fxH0xK+hVk5OypPmh/S4peIjVbEFxt8wAXCl/BA==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "c0792ed2-5619-4944-b057-fa0baae63e4c", null, null, 1, null, null, false, null, "chusan", null, null, null },
                    { 4, 0, null, null, "Chứng chỉ A", "f3c67dad-8498-4d59-bfc8-892ce6d92a27", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 737, DateTimeKind.Unspecified).AddTicks(6011), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV B", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 737, DateTimeKind.Unspecified).AddTicks(6031), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH@TEAMUP.COM", "COACH", null, "AQAAAAIAAYagAAAAELI691WvYkf+AxLDFLQf8XQ0IOe02QGCdDi6OF9ue+DyHLIfivgJktveRVTykZZWZg==", null, false, 5000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "578883f1-88d4-4225-b9a5-b081beb23f30", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach", null, "Sân ABC, Quận 1", "Thứ 2, 4, 6" },
                    { 5, 0, null, null, "Chứng chỉ B", "1f763af9-dfb2-443d-b282-f9d84ed8afcd", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4317), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach1@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV 1", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4345), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH1@TEAMUP.COM", "COACH1", null, "AQAAAAIAAYagAAAAEJbkT0mmMOB2ZECQI1vaDj1HHPQZgXgZrwZo8w6xkS8wAVFNocDCvYE2zRG20WjMkQ==", null, false, 250000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "4282a6b4-d4a1-4ae2-9c7e-5703aa730835", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach1", null, "Sân XYZ, Quận 5", "Thứ 3, 5" },
                    { 6, 0, null, null, "Chứng chỉ C", "33a06d37-2946-4f4d-9e6f-9a4d2458eb08", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4503), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach2@teamup.com", true, "3 năm huấn luyện cá nhân và nhóm", null, "HLV 2", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4504), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH2@TEAMUP.COM", "COACH2", null, "AQAAAAIAAYagAAAAEMbiEwR2enDeiH3pVzflSkkQxR3xClXNk0PNVPjFRA5Q2Vf1Jkd6QPo93PL9qxfx3A==", null, false, 180000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "bb87d827-a9cb-4df6-abc7-9972324961ce", "Cầu lông", null, 1, "Active", "Người lớn, học sinh", false, "Cầu lông", "coach2", null, "Sân Lông, Quận 2", "Thứ 2, 4" },
                    { 7, 0, null, null, "Chứng chỉ D", "5b171f25-2cbb-40d5-87bd-6c7277e2fb92", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4515), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach3@teamup.com", true, "2 năm giảng dạy cho người mới bắt đầu", null, "HLV 3", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4517), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH3@TEAMUP.COM", "COACH3", null, "AQAAAAIAAYagAAAAEP0n8+0PDw3O05UOm8Rhh4ShXt38DtIhTn8qk57BOH/ctZiKnzBK1KFOiqRukTiS4Q==", null, false, 220000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "f37b6cba-13c8-4d75-8945-3b0f71dc7d0d", "Pickleball", null, 1, "Active", "Người mới chơi, người cao tuổi", false, "Pickleball", "coach3", null, "Sân PB, Quận 7", "Thứ 6, 7" },
                    { 8, 0, null, null, "Chứng chỉ A", "ec8f16ec-c18d-4691-9cc4-880af181af54", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4758), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach4@teamup.com", true, "8 năm làm HLV cho các đội phong trào", null, "HLV 4", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4760), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH4@TEAMUP.COM", "COACH4", null, "AQAAAAIAAYagAAAAEL3vRQiDEKhaGUmwvqCwnId3hYsu3iF9AQVHvp5Crbww3SerQWdGARw75EOOIKHYTQ==", null, false, 230000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "833c704f-5acf-42d9-bc7a-a0ee47c2b225", "Bóng đá", null, 1, "Active", "Người lớn, sinh viên", false, "Bóng đá", "coach4", null, "Sân K, Quận 6", "Thứ 3, 6" },
                    { 9, 0, null, null, "Chứng chỉ B", "59e27220-4256-494c-ac33-499eae453a5c", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4770), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach5@teamup.com", true, "4 năm giảng dạy tại trung tâm thể thao", null, "HLV 5", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4772), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH5@TEAMUP.COM", "COACH5", null, "AQAAAAIAAYagAAAAEIqezacBCV0OTszQBYHN0/EjtwUzsEZPFGY3clZVFSSMF414r2mVzTshWq26EOgedw==", null, false, 190000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "f5e00413-6671-42d6-8247-1c8e7b9938a8", "Cầu lông", null, 1, "Active", "Thiếu nhi, người đi làm", false, "Cầu lông", "coach5", null, "Sân Mây, Quận 10", "Thứ 2, 5" },
                    { 10, 0, null, null, "Chứng chỉ E", "50418fae-c833-4939-97b9-edfc2b82ca19", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4780), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach6@teamup.com", true, "1 năm hỗ trợ luyện tập cơ bản và thi đấu", null, "HLV 6", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4782), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH6@TEAMUP.COM", "COACH6", null, "AQAAAAIAAYagAAAAEHNNpD4MgooN5k1TN7s+NrYmP0nzpwYqaEb5MYMf3fM4NXKQ6dAhA+hKaeH8cjiOnw==", null, false, 210000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "b03d1245-7762-465c-ac28-d28eefb61685", "Pickleball", null, 1, "Active", "Người cao tuổi, học viên nữ", false, "Pickleball", "coach6", null, "Sân Pick, Quận 9", "Thứ 4, 7" },
                    { 11, 0, null, null, "Chứng chỉ C", "65473526-b565-4994-8885-485bbd1da5e5", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4791), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach7@teamup.com", true, "6 năm giảng dạy các lớp nâng cao", null, "HLV 7", null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 19, 827, DateTimeKind.Unspecified).AddTicks(4804), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH7@TEAMUP.COM", "COACH7", null, "AQAAAAIAAYagAAAAEJXzii9CaLKzSySCALr6SQeg45ZtUbcjJV0jqm1tLgj9w9nMDjsSNXQcEDYoOnqRKg==", null, false, 240000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "d4a99b60-7af3-4bdc-8b52-f61a8c3b37ce", "Bóng đá", null, 1, "Active", "Học viên đã có nền tảng", false, "Bóng đá", "coach7", null, "Sân Gold, Quận Tân Bình", "Thứ 3, 5, 7" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DurationDays", "LastUpdatedBy", "LastUpdatedTime", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(5965), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 30 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(5984), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 10000m, "PackageHLV" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6003), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói cao cấp 90 ngày", 90, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6004), new TimeSpan(0, 7, 0, 0, 0)), "Premium", 10000m, "PackageHLV" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6007), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 30 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6007), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 10000m, "PackageHLV" }
                });

            migrationBuilder.InsertData(
                table: "Voucher",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DiscountPercent", "LastUpdatedBy", "LastUpdatedTime" },
                values: new object[,]
                {
                    { 1, "VOUCHER1", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7135), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Giảm 10% cho booking đầu tiên", 10, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7135), new TimeSpan(0, 7, 0, 0, 0)) },
                    { 2, "VOUCHER2", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7140), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Hỗ trợ website giảm 5%", 5, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7141), new TimeSpan(0, 7, 0, 0, 0)) }
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
                    { 1, "HLV rất chuyên nghiệp, hướng dẫn tận tình.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6499), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6499), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 2, "Chủ sân thân thiện, sân sạch đẹp.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6504), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6505), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 3, "Chủ sân hỗ trợ rất nhiệt tình và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6507), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6508), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 4, "Không gian rộng rãi, dễ đặt lịch.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6509), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6510), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 5, "Thỉnh thoảng hơi chậm phản hồi tin nhắn.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6512), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6512), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 4 },
                    { 6, "Chủ sân dễ tính, rất dễ thương!", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6514), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6515), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 4 },
                    { 7, "Sân tốt, chủ sân chu đáo.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6516), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6517), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 8, "Quản lý chuyên nghiệp, xử lý tình huống nhanh chóng.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6518), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6519), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 9, "Dịch vụ ổn, sẽ quay lại lần nữa.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6521), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6521), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 1 },
                    { 10, "Chủ sân rất thân thiện, đáng tin cậy.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6523), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6523), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 11, "Cần cải thiện thời gian mở cửa đúng giờ hơn.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6525), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6526), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 1 },
                    { 12, "Chất lượng phục vụ tuyệt vời!", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6527), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6528), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 13, "HLV rất tâm huyết và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6574), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6575), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 14, "Giảng dạy dễ hiểu, thái độ thân thiện.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6577), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6577), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 3 },
                    { 15, "Tận tình hỗ trợ, kỹ năng tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6579), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6580), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 5 },
                    { 16, "Cực kỳ có trách nhiệm với học viên.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6582), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6582), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 6 },
                    { 17, "Phương pháp huấn luyện rõ ràng.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6633), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6633), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 2 },
                    { 18, "Đúng giờ, vui vẻ và tận tâm.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6637), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6638), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 3 },
                    { 19, "Cải thiện kỹ năng rõ rệt sau vài buổi.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6639), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6640), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 6 },
                    { 20, "Kỹ năng truyền đạt tốt, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6642), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6642), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 7 },
                    { 21, "Cần tăng tính kỷ luật, nhưng kỹ năng ổn.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6644), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6645), new TimeSpan(0, 7, 0, 0, 0)), 3, 6, 2 },
                    { 22, "Nhiệt tình, vui vẻ, luôn động viên học viên.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6647), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6647), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 3 },
                    { 23, "Bài tập sáng tạo, dễ áp dụng.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6649), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6650), new TimeSpan(0, 7, 0, 0, 0)), 5, 6, 4 },
                    { 24, "Có chuyên môn cao, dễ tiếp cận.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6651), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6652), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 5 },
                    { 25, "Giúp tôi nâng cao thể lực rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6654), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6654), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 2 },
                    { 26, "Đào tạo bài bản, bài tập phù hợp trình độ.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6656), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6657), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 3 },
                    { 27, "Thời gian linh hoạt, hỗ trợ tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6658), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6659), new TimeSpan(0, 7, 0, 0, 0)), 4, 7, 4 },
                    { 28, "Có kinh nghiệm thực tế, phong cách giảng dạy chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6661), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6662), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 6 },
                    { 29, "Khả năng truyền đạt tốt, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6663), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6664), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 2 },
                    { 30, "Kiến thức vững, giao tiếp tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6666), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6666), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 3 },
                    { 31, "Luôn khuyến khích học viên cố gắng.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6668), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6669), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 4 },
                    { 32, "Cực kỳ chuyên nghiệp và dễ thương.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6670), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6671), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 5 },
                    { 33, "Bài giảng sáng tạo, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6673), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6673), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 2 },
                    { 34, "Có nhiều kinh nghiệm thực chiến.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6675), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6676), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 3 },
                    { 35, "Tận tâm với học viên, hỗ trợ thêm ngoài giờ.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6677), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6678), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 6 },
                    { 36, "Chuyên nghiệp, luôn đúng giờ.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6679), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6680), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 7 },
                    { 37, "Nội dung giảng dạy phù hợp từng người.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6682), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6682), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 2 },
                    { 38, "Tạo động lực cho học viên rất tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6685), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6685), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 3 },
                    { 39, "Rất tận tình, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6687), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6688), new TimeSpan(0, 7, 0, 0, 0)), 4, 10, 4 },
                    { 40, "Phong cách dạy chuyên nghiệp và hiệu quả.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6692), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6692), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 5 },
                    { 41, "HLV dày dạn kinh nghiệm, đáng học hỏi.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6695), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6696), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 2 },
                    { 42, "Dạy dễ hiểu, luôn hỗ trợ đúng lúc.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6697), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6698), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 3 },
                    { 43, "Phong thái chuyên nghiệp, vui vẻ.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6700), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6700), new TimeSpan(0, 7, 0, 0, 0)), 4, 11, 6 },
                    { 44, "Giúp tôi cải thiện kỹ thuật rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6702), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6702), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 7 }
                });

            migrationBuilder.InsertData(
                table: "SportsComplexes",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "ImageUrls", "LastUpdatedBy", "LastUpdatedTime", "Latitude", "Longitude", "Name", "OwnerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "Sân bóng đá Tao Đàn, 1 Huyền Trân Công Chúa, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6290), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6291), new TimeSpan(0, 7, 0, 0, 0)), 10.773444, 106.690933, "Khu Thể Thao ABC", 3, "Active", "Bóng đá" },
                    { 2, "Sân cầu lông Hồ Kỳ Hòa, 27 Cao Thắng, Quận 3, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6303), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6304), new TimeSpan(0, 7, 0, 0, 0)), 10.768492999999999, 106.681771, "Khu Thể Thao DEF", 3, "Active", "Cầu lông" },
                    { 3, "Sân thể thao Rạch Miễu, 1 Hoa Phượng, Phú Nhuận, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6309), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6309), new TimeSpan(0, 7, 0, 0, 0)), 10.800005000000001, 106.683813, "Khu Thể Thao GHI", 3, "Active", "Pickleball" },
                    { 4, "Sân thể thao Vạn Tường, 59A Nguyễn Du, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6313), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6314), new TimeSpan(0, 7, 0, 0, 0)), 10.77623, 106.699208, "Khu Thể Thao JKL", 3, "Active", "Pickleball" },
                    { 5, "Sân cầu lông Quận 6, 42 Nguyễn Văn Luông, Quận 6, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6317), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6317), new TimeSpan(0, 7, 0, 0, 0)), 10.737717, 106.62858199999999, "Khu Thể Thao MNO", 3, "Active", "Cầu lông" }
                });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "ImageUrls", "LastUpdatedBy", "LastUpdatedTime", "Name", "PricePerHour", "SportsComplexId", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6382), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân cỏ nhân tạo tiêu chuẩn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6382), new TimeSpan(0, 7, 0, 0, 0)), "Sân 5 người A", 10000m, 1, "Active" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6392), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chất lượng cao, đèn chiếu sáng ban đêm", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6392), new TimeSpan(0, 7, 0, 0, 0)), "Sân 7 người B", 350000m, 1, "Active" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6395), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6396), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông A", 150000m, 2, "Active" },
                    { 4, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6399), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn phong trào", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6399), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông B", 100000m, 2, "Active" },
                    { 5, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6402), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, chất lượng cao", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6403), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball A", 250000m, 3, "Active" },
                    { 6, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6405), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, có mái che", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6406), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball B", 300000m, 3, "Active" },
                    { 7, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6408), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn FIBA, sàn gỗ cao cấp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6409), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball C", 400000m, 4, "Active" },
                    { 8, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6411), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân phong trào, phù hợp nhóm bạn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6412), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball D", 250000m, 4, "Active" },
                    { 9, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6414), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân luyện tập cá nhân", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6415), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball E", 200000m, 4, "Active" },
                    { 10, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6417), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6417), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông C", 300000m, 5, "Active" },
                    { 11, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6419), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, thoáng mát", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6420), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông D", 200000m, 5, "Active" },
                    { 12, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6422), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân thi đấu chuyên nghiệp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6423), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông E", 350000m, 5, "Active" },
                    { 13, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6427), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân tiêu chuẩn quốc tế", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6427), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball F", 280000m, 3, "Active" },
                    { 14, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6429), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân mở ban đêm, có đèn chiếu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6430), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông F", 270000m, 4, "Active" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CourtId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "HostId", "LastUpdatedBy", "LastUpdatedTime", "MaxPlayers", "Name", "RoomFee", "ScheduledTime", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6942), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tập hợp anh em giao lưu bóng đá sáng thứ 7.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(6943), new TimeSpan(0, 7, 0, 0, 0)), 10, "Team Sáng Thứ 7", 30000m, new DateTime(2025, 6, 1, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 2, 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7019), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm đối đá giao hữu 7v7 buổi tối.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7020), new TimeSpan(0, 7, 0, 0, 0)), 14, "Giao lưu buổi tối", 50000m, new DateTime(2025, 6, 2, 20, 0, 0, 0, DateTimeKind.Local), "Completed" },
                    { 3, 3, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7025), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đánh cầu cuối tuần, vui vẻ là chính.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7026), new TimeSpan(0, 7, 0, 0, 0)), 4, "Badminton Team CN", 20000m, new DateTime(2025, 6, 3, 9, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 4, 4, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7030), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm team giao lưu vào chiều thứ 5.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7031), new TimeSpan(0, 7, 0, 0, 0)), 10, "Đá bóng chiều thứ 5", 30000m, new DateTime(2025, 6, 4, 17, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 5, 5, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7035), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Pickleball nhẹ nhàng chủ nhật.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7035), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball sáng CN", 40000m, new DateTime(2025, 6, 5, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 6, 6, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7039), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Giao lưu Pickleball chiều cuối tuần.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7040), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball chiều thứ 7", 50000m, new DateTime(2025, 6, 6, 15, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 7, 7, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7043), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team Pickleball tụ tập tối thứ 3.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7044), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball tối thứ 3", 25000m, new DateTime(2025, 6, 3, 20, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 8, 8, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7049), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Vui là chính, ai cũng có thể tham gia.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7049), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball phong trào", 20000m, new DateTime(2025, 6, 2, 18, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 9, 10, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7054), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đội hình luyện tập chuẩn bị giải.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7054), new TimeSpan(0, 7, 0, 0, 0)), 12, "Cầu lông tập luyện", 35000m, new DateTime(2025, 6, 1, 16, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 10, 12, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7058), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team cầu lông nhẹ nhàng tối làm về.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 31, 16, 47, 20, 420, DateTimeKind.Unspecified).AddTicks(7059), new TimeSpan(0, 7, 0, 0, 0)), 4, "Chơi cầu lông tối", 25000m, new DateTime(2025, 6, 4, 19, 0, 0, 0, DateTimeKind.Local), "Waiting" }
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
                name: "IX_AspNetUsers_PackageId",
                table: "AspNetUsers",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
