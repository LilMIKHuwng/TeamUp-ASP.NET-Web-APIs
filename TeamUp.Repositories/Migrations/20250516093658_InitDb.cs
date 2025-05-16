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
                    Specialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    SelectedDates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { 1, null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9086), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Quản trị viên", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9119), new TimeSpan(0, 7, 0, 0, 0)), "Admin", "ADMIN" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9125), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Người dùng thông thường", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9126), new TimeSpan(0, 7, 0, 0, 0)), "User", "USER" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9128), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Chủ sân thể thao", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9128), new TimeSpan(0, 7, 0, 0, 0)), "Owner", "OWNER" },
                    { 4, null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9129), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Coach / Trainer", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9130), new TimeSpan(0, 7, 0, 0, 0)), "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "AvatarUrl", "Certificate", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "ExpireDate", "FullName", "Height", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PackageId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PricePerSession", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Specialty", "StartDate", "Status", "StatusForCoach", "TwoFactorEnabled", "UserName", "Weight", "WorkingAddress", "WorkingDate" },
                values: new object[,]
                {
                    { 1, 0, null, null, null, "e6e65dbf-b956-465b-9b59-727de8f8854e", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9474), new TimeSpan(0, 7, 0, 0, 0)), null, null, "admin@teamup.com", true, null, "System Admin", null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 730, DateTimeKind.Unspecified).AddTicks(9475), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@TEAMUP.COM", "ADMIN", null, "AQAAAAIAAYagAAAAEKv4j0lNI2bFqOCgbLBdHBTR52/7kpYo7WfcG6nxzQhoKlsOfi+yhj7Z1fjAEU0z2Q==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "4f7369ef-5bf1-416a-82d1-b97253d5d75b", null, null, 1, null, false, "admin", null, null, null },
                    { 2, 0, null, null, null, "bdd78af7-8bfb-4883-8bcc-0d9fa7506885", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 798, DateTimeKind.Unspecified).AddTicks(7110), new TimeSpan(0, 7, 0, 0, 0)), null, null, "player@teamup.com", true, null, "Người Chơi A", null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 798, DateTimeKind.Unspecified).AddTicks(7176), new TimeSpan(0, 7, 0, 0, 0)), false, null, "PLAYER@TEAMUP.COM", "PLAYER", null, "AQAAAAIAAYagAAAAEPnTvhtnF8mmqxj0LEtBV4DQ1UhK7xZ62FJ+2e9nsP2m+0QzL1dJTNpBqPh8uC5vaw==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "a526934d-371a-488e-a70c-2da62c313d1e", null, null, 1, null, false, "player", null, null, null },
                    { 3, 0, null, null, null, "46b7e408-8fcf-4144-b3df-e4d223ed5716", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 875, DateTimeKind.Unspecified).AddTicks(1537), new TimeSpan(0, 7, 0, 0, 0)), null, null, "chusan@teamup.com", true, null, "Chủ Sân A", null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 875, DateTimeKind.Unspecified).AddTicks(1592), new TimeSpan(0, 7, 0, 0, 0)), false, null, "CHUSAN@TEAMUP.COM", "CHUSAN", null, "AQAAAAIAAYagAAAAEMZQNxcLWaOqDDzIdmzDp6X6/QKokcga506GkT27zzgNy1lvQhu/o3zoiC9ZVHiA/w==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "d2a30b00-269d-42d9-8cc1-54b0163d23e4", null, null, 1, null, false, "chusan", null, null, null },
                    { 4, 0, null, null, "Chứng chỉ A", "c025a56e-62aa-43ad-835e-829662e75595", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 965, DateTimeKind.Unspecified).AddTicks(6077), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach@teamup.com", true, null, "HLV B", null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 56, 965, DateTimeKind.Unspecified).AddTicks(6148), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH@TEAMUP.COM", "COACH", null, "AQAAAAIAAYagAAAAEHP1FaDg1zDzy/vfcIAmRtHpnorVnSrMRC9doL1AtUZS2DsRQ4mvJ6YDVe6Jk66DSQ==", null, false, 200000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "a474806f-5792-4756-a859-011acc1eb89a", "Bóng đá", null, 1, null, false, "coach", null, "Sân ABC, Quận 1", "Thứ 2, 4, 6" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DurationDays", "LastUpdatedBy", "LastUpdatedTime", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2788), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 365 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2846), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 399000m, "PackageHLV" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2857), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói cao cấp 1095 ngày", 90, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2858), new TimeSpan(0, 7, 0, 0, 0)), "Premium", 599000m, "PackageHLV" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2860), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói cao cấp 1095 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2861), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 199000m, "PackageHLV" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "Comment", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "LastUpdatedBy", "LastUpdatedTime", "RatingValue", "RevieweeId", "ReviewerId" },
                values: new object[,]
                {
                    { 1, "HLV rất chuyên nghiệp, hướng dẫn tận tình.", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3178), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3179), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 2, "Chủ sân thân thiện, sân sạch đẹp.", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3184), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3185), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "SportsComplexes",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "ImageUrls", "LastUpdatedBy", "LastUpdatedTime", "Name", "OwnerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "123 Đường A, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2923), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2924), new TimeSpan(0, 7, 0, 0, 0)), "Khu Thể Thao ABC", 3, "Active", "Bóng đá" },
                    { 2, "456 Đường B, Quận 5, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2943), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\"]", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(2944), new TimeSpan(0, 7, 0, 0, 0)), "Khu Thể Thao DEF", 3, "Active", "Cầu lông" }
                });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "ImageUrls", "LastUpdatedBy", "LastUpdatedTime", "Name", "PricePerHour", "SportsComplexId", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3114), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân cỏ nhân tạo tiêu chuẩn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3116), new TimeSpan(0, 7, 0, 0, 0)), "Sân 5 người A", 200000m, 1, "Active" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3119), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chất lượng cao, đèn chiếu sáng ban đêm", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3119), new TimeSpan(0, 7, 0, 0, 0)), "Sân 7 người B", 350000m, 1, "Active" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3122), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3123), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông A", 150000m, 2, "Active" },
                    { 4, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3125), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn phong trào", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3126), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông B", 100000m, 2, "Active" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CourtId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "HostId", "LastUpdatedBy", "LastUpdatedTime", "MaxPlayers", "Name", "RoomFee", "ScheduledTime", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3245), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tập hợp anh em giao lưu bóng đá sáng thứ 7.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3246), new TimeSpan(0, 7, 0, 0, 0)), 10, "Team Sáng Thứ 7", 30000m, new DateTime(2025, 5, 17, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 2, 2, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3282), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm đối đá giao hữu 7v7 buổi tối.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3307), new TimeSpan(0, 7, 0, 0, 0)), 14, "Giao lưu buổi tối", 50000m, new DateTime(2025, 5, 18, 20, 0, 0, 0, DateTimeKind.Local), "Completed" },
                    { 3, 3, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3345), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đánh cầu cuối tuần, vui vẻ là chính.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 16, 16, 36, 57, 57, DateTimeKind.Unspecified).AddTicks(3346), new TimeSpan(0, 7, 0, 0, 0)), 4, "Badminton Team CN", 20000m, new DateTime(2025, 5, 19, 9, 0, 0, 0, DateTimeKind.Local), "Full" }
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
                name: "IX_CourtBookings_CourtId",
                table: "CourtBookings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_CourtBookings_UserId",
                table: "CourtBookings",
                column: "UserId");

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
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RoomJoinRequests");

            migrationBuilder.DropTable(
                name: "RoomPlayers");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CoachBookings");

            migrationBuilder.DropTable(
                name: "CourtBookings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "SportsComplexes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Packages");
        }
    }
}
