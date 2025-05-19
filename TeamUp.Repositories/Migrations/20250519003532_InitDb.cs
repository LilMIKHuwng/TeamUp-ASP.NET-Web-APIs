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
                    { 1, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7548), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Quản trị viên", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7602), new TimeSpan(0, 7, 0, 0, 0)), "Admin", "ADMIN" },
                    { 2, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7610), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Người dùng thông thường", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7611), new TimeSpan(0, 7, 0, 0, 0)), "User", "USER" },
                    { 3, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7613), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Chủ sân thể thao", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7614), new TimeSpan(0, 7, 0, 0, 0)), "Owner", "OWNER" },
                    { 4, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7615), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Coach / Trainer", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(7616), new TimeSpan(0, 7, 0, 0, 0)), "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Age", "AvatarUrl", "Certificate", "ConcurrencyStamp", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "Experience", "ExpireDate", "FullName", "Height", "LastUpdatedBy", "LastUpdatedTime", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PackageId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PricePerSession", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "Specialty", "StartDate", "Status", "StatusForCoach", "TargetObject", "TwoFactorEnabled", "Type", "UserName", "Weight", "WorkingAddress", "WorkingDate" },
                values: new object[,]
                {
                    { 1, 0, null, null, null, "03e70932-2042-49ef-b2f5-86b46c2c7f0d", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(8052), new TimeSpan(0, 7, 0, 0, 0)), null, null, "admin@teamup.com", true, null, null, "System Admin", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 730, DateTimeKind.Unspecified).AddTicks(8055), new TimeSpan(0, 7, 0, 0, 0)), false, null, "ADMIN@TEAMUP.COM", "ADMIN", null, "AQAAAAIAAYagAAAAELFezNtW+dFMAcmiRAT3cz5e5vvohYooMVXAbM+vd0TK+4GEPgnxr/l3JPOLoH8BQg==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "d2ec4394-c6ae-4ac8-9896-bba86de66f94", null, null, 1, null, null, false, null, "admin", null, null, null },
                    { 2, 0, null, null, null, "72772bdf-4a9e-4747-b3d9-9e7a8e308885", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 823, DateTimeKind.Unspecified).AddTicks(3459), new TimeSpan(0, 7, 0, 0, 0)), null, null, "player@teamup.com", true, null, null, "Người Chơi A", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 823, DateTimeKind.Unspecified).AddTicks(3492), new TimeSpan(0, 7, 0, 0, 0)), false, null, "PLAYER@TEAMUP.COM", "PLAYER", null, "AQAAAAIAAYagAAAAEJyZvGV2bbMkEZj4pc/jUXdsAFC4KsQjztzqjz+ZJNbkfgVsaVFmWa+mViQfEn5wPg==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "b9cd92fd-7450-483b-916f-1f2882023951", null, null, 1, null, null, false, null, "player", null, null, null },
                    { 3, 0, null, null, null, "70a86227-ba7a-415d-9258-dc653e82dcf9", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 914, DateTimeKind.Unspecified).AddTicks(6723), new TimeSpan(0, 7, 0, 0, 0)), null, null, "chusan@teamup.com", true, null, null, "Chủ Sân A", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 29, 914, DateTimeKind.Unspecified).AddTicks(6742), new TimeSpan(0, 7, 0, 0, 0)), false, null, "CHUSAN@TEAMUP.COM", "CHUSAN", null, "AQAAAAIAAYagAAAAEEuWLfQOP4A0zWJy5olCJC6ahPHgNh0MnfFlAC6iIh/948BrBVUHYW/74sxs0LsjEQ==", null, false, null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "13753dd8-f6ae-4bd0-8761-4756a50a3557", null, null, 1, null, null, false, null, "chusan", null, null, null },
                    { 4, 0, null, null, "Chứng chỉ A", "8ace79db-8b1e-4709-91f6-9e3bf66edca4", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 15, DateTimeKind.Unspecified).AddTicks(2656), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV B", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 15, DateTimeKind.Unspecified).AddTicks(2676), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH@TEAMUP.COM", "COACH", null, "AQAAAAIAAYagAAAAEHMiPVT2Ew0ceKAHvJgqZ4ASX4wcOdlY83Rc3GRSzABxBG/aWKwu8ZRlX31May/1SA==", null, false, 200000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "546653b9-6dcf-49b7-9c43-0859439f0f1b", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach", null, "Sân ABC, Quận 1", "Thứ 2, 4, 6" },
                    { 5, 0, null, null, "Chứng chỉ B", "b2d7074e-3ba2-4db3-8362-a306f933a95e", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1174), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach1@teamup.com", true, "5 năm huấn luyện đội trẻ U15", null, "HLV 1", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1191), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH1@TEAMUP.COM", "COACH1", null, "AQAAAAIAAYagAAAAEKbyRoO9Es9PZV9A9T/t0iqLvx1OwhQMf/t00nRtQJT5CDDw8uT4sOpSg37IkZJcEA==", null, false, 250000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ffc5db0c-b1c2-46d0-93b0-fb48e7ff6ad1", "Bóng đá", null, 1, "Active", "Trẻ em, thanh thiếu niên", false, "Bóng đá", "coach1", null, "Sân XYZ, Quận 5", "Thứ 3, 5" },
                    { 6, 0, null, null, "Chứng chỉ C", "affc3a7b-9404-4ab3-9d48-12551825af3e", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1293), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach2@teamup.com", true, "3 năm huấn luyện cá nhân và nhóm", null, "HLV 2", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1294), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH2@TEAMUP.COM", "COACH2", null, "AQAAAAIAAYagAAAAEHdUX5Lde1WnG4rVqTKw3jJuf/syIrPe3XUahsLw5iq78rINZ8RTVwPANVRDNpgArw==", null, false, 180000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "53d19e5a-8312-4cd8-a2fe-3fe65f42f964", "Cầu lông", null, 1, "Active", "Người lớn, học sinh", false, "Cầu lông", "coach2", null, "Sân Lông, Quận 2", "Thứ 2, 4" },
                    { 7, 0, null, null, "Chứng chỉ D", "ad9435ad-3712-4607-90b2-1656f224d3ca", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1325), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach3@teamup.com", true, "2 năm giảng dạy cho người mới bắt đầu", null, "HLV 3", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1327), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH3@TEAMUP.COM", "COACH3", null, "AQAAAAIAAYagAAAAEFxpjM9F3TcF+aOIXjviJHoY5EFuFLCr5uznNpzKZAWu2L1fZvhvsdwd1dv6JdXLzQ==", null, false, 220000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ba9ee07c-d63a-4aca-98da-3a2d62e35808", "Pickleball", null, 1, "Active", "Người mới chơi, người cao tuổi", false, "Pickleball", "coach3", null, "Sân PB, Quận 7", "Thứ 6, 7" },
                    { 8, 0, null, null, "Chứng chỉ A", "96b861de-253e-452a-87f5-dd75a0192141", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1379), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach4@teamup.com", true, "8 năm làm HLV cho các đội phong trào", null, "HLV 4", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1381), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH4@TEAMUP.COM", "COACH4", null, "AQAAAAIAAYagAAAAEAFjFsJfTW/kH8FrvUrbc0b/uJIZzOybO/A4pIh/5yFP56D3le2W7kLaRVuaSg0Vcw==", null, false, 230000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "e7df931e-f2e2-4b6c-9639-4bafb76b0216", "Bóng đá", null, 1, "Active", "Người lớn, sinh viên", false, "Bóng đá", "coach4", null, "Sân K, Quận 6", "Thứ 3, 6" },
                    { 9, 0, null, null, "Chứng chỉ B", "7943fa6f-2800-4bda-825f-6ea4f7e6590c", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1388), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach5@teamup.com", true, "4 năm giảng dạy tại trung tâm thể thao", null, "HLV 5", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1389), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH5@TEAMUP.COM", "COACH5", null, "AQAAAAIAAYagAAAAEEs6nTmxweVrPUg7SZQG41wW8nPoQtI4rd7hUvYtt7tkyZa7fX6c48uINe1TtWPtmQ==", null, false, 190000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "ff782874-b72f-4203-938f-812a972095c7", "Cầu lông", null, 1, "Active", "Thiếu nhi, người đi làm", false, "Cầu lông", "coach5", null, "Sân Mây, Quận 10", "Thứ 2, 5" },
                    { 10, 0, null, null, "Chứng chỉ E", "68e7bedf-6eec-4ea5-ad0c-1f8509532929", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1399), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach6@teamup.com", true, "1 năm hỗ trợ luyện tập cơ bản và thi đấu", null, "HLV 6", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1400), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH6@TEAMUP.COM", "COACH6", null, "AQAAAAIAAYagAAAAEJwcJ0fvgd3TW4Y2S2t7yZxZqb6KWVoUYw71gKYlz9WCsyEReuwG93xIQPq8gJgIhg==", null, false, 210000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "31f10132-207c-44bd-b7e1-80be5be792d0", "Pickleball", null, 1, "Active", "Người cao tuổi, học viên nữ", false, "Pickleball", "coach6", null, "Sân Pick, Quận 9", "Thứ 4, 7" },
                    { 11, 0, null, null, "Chứng chỉ C", "8f1ad250-d9a0-40e3-8d45-9c19e2e465e7", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1407), new TimeSpan(0, 7, 0, 0, 0)), null, null, "coach7@teamup.com", true, "6 năm giảng dạy các lớp nâng cao", null, "HLV 7", null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 116, DateTimeKind.Unspecified).AddTicks(1408), new TimeSpan(0, 7, 0, 0, 0)), false, null, "COACH7@TEAMUP.COM", "COACH7", null, "AQAAAAIAAYagAAAAEKHMU2QjEfhFbUteto3BenJapY2ec3PTuWLK+ZXQbtPxJ9quRx+8VqjP1bpCQlaA2Q==", null, false, 240000m, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "bb67312c-3c77-475d-bedb-34e52b90b02a", "Bóng đá", null, 1, "Active", "Học viên đã có nền tảng", false, "Bóng đá", "coach7", null, "Sân Gold, Quận Tân Bình", "Thứ 3, 5, 7" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "DurationDays", "LastUpdatedBy", "LastUpdatedTime", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3325), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói dịch vụ 365 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3338), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 399000m, "PackageHLV" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3350), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói cao cấp 1095 ngày", 90, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3351), new TimeSpan(0, 7, 0, 0, 0)), "Premium", 599000m, "PackageHLV" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3353), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Gói cao cấp 1095 ngày", 30, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3354), new TimeSpan(0, 7, 0, 0, 0)), "Basic", 199000m, "PackageHLV" }
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
                    { 1, "HLV rất chuyên nghiệp, hướng dẫn tận tình.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3779), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3780), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 2, "Chủ sân thân thiện, sân sạch đẹp.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3784), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3785), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 3, "Chủ sân hỗ trợ rất nhiệt tình và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3787), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3788), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 4, "Không gian rộng rãi, dễ đặt lịch.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3789), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3791), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 5, "Thỉnh thoảng hơi chậm phản hồi tin nhắn.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3792), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3793), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 4 },
                    { 6, "Chủ sân dễ tính, rất dễ thương!", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3795), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3796), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 4 },
                    { 7, "Sân tốt, chủ sân chu đáo.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3799), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3800), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 2 },
                    { 8, "Quản lý chuyên nghiệp, xử lý tình huống nhanh chóng.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3801), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3802), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 2 },
                    { 9, "Dịch vụ ổn, sẽ quay lại lần nữa.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3804), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3805), new TimeSpan(0, 7, 0, 0, 0)), 4, 3, 1 },
                    { 10, "Chủ sân rất thân thiện, đáng tin cậy.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3806), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3808), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 11, "Cần cải thiện thời gian mở cửa đúng giờ hơn.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3809), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3810), new TimeSpan(0, 7, 0, 0, 0)), 3, 3, 1 },
                    { 12, "Chất lượng phục vụ tuyệt vời!", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3812), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3813), new TimeSpan(0, 7, 0, 0, 0)), 5, 3, 1 },
                    { 13, "HLV rất tâm huyết và chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3854), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3856), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 2 },
                    { 14, "Giảng dạy dễ hiểu, thái độ thân thiện.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3858), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3859), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 3 },
                    { 15, "Tận tình hỗ trợ, kỹ năng tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3860), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3861), new TimeSpan(0, 7, 0, 0, 0)), 4, 4, 5 },
                    { 16, "Cực kỳ có trách nhiệm với học viên.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3863), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3864), new TimeSpan(0, 7, 0, 0, 0)), 5, 4, 6 },
                    { 17, "Phương pháp huấn luyện rõ ràng.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3865), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3867), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 2 },
                    { 18, "Đúng giờ, vui vẻ và tận tâm.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3868), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3869), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 3 },
                    { 19, "Cải thiện kỹ năng rõ rệt sau vài buổi.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3871), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3872), new TimeSpan(0, 7, 0, 0, 0)), 5, 5, 6 },
                    { 20, "Kỹ năng truyền đạt tốt, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3873), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3874), new TimeSpan(0, 7, 0, 0, 0)), 4, 5, 7 },
                    { 21, "Cần tăng tính kỷ luật, nhưng kỹ năng ổn.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3876), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3877), new TimeSpan(0, 7, 0, 0, 0)), 3, 6, 2 },
                    { 22, "Nhiệt tình, vui vẻ, luôn động viên học viên.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3878), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3879), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 3 },
                    { 23, "Bài tập sáng tạo, dễ áp dụng.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3881), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3882), new TimeSpan(0, 7, 0, 0, 0)), 5, 6, 4 },
                    { 24, "Có chuyên môn cao, dễ tiếp cận.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3883), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3885), new TimeSpan(0, 7, 0, 0, 0)), 4, 6, 5 },
                    { 25, "Giúp tôi nâng cao thể lực rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3886), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3887), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 2 },
                    { 26, "Đào tạo bài bản, bài tập phù hợp trình độ.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3889), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3890), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 3 },
                    { 27, "Thời gian linh hoạt, hỗ trợ tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3891), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3892), new TimeSpan(0, 7, 0, 0, 0)), 4, 7, 4 },
                    { 28, "Có kinh nghiệm thực tế, phong cách giảng dạy chuyên nghiệp.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3894), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3895), new TimeSpan(0, 7, 0, 0, 0)), 5, 7, 6 },
                    { 29, "Khả năng truyền đạt tốt, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3898), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3899), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 2 },
                    { 30, "Kiến thức vững, giao tiếp tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3900), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3902), new TimeSpan(0, 7, 0, 0, 0)), 4, 8, 3 },
                    { 31, "Luôn khuyến khích học viên cố gắng.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3903), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3904), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 4 },
                    { 32, "Cực kỳ chuyên nghiệp và dễ thương.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3906), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3907), new TimeSpan(0, 7, 0, 0, 0)), 5, 8, 5 },
                    { 33, "Bài giảng sáng tạo, dễ hiểu.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3908), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3909), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 2 },
                    { 34, "Có nhiều kinh nghiệm thực chiến.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3911), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3912), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 3 },
                    { 35, "Tận tâm với học viên, hỗ trợ thêm ngoài giờ.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3914), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3915), new TimeSpan(0, 7, 0, 0, 0)), 4, 9, 6 },
                    { 36, "Chuyên nghiệp, luôn đúng giờ.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3916), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3917), new TimeSpan(0, 7, 0, 0, 0)), 5, 9, 7 },
                    { 37, "Nội dung giảng dạy phù hợp từng người.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3926), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3928), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 2 },
                    { 38, "Tạo động lực cho học viên rất tốt.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3929), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3930), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 3 },
                    { 39, "Rất tận tình, thân thiện.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3932), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3933), new TimeSpan(0, 7, 0, 0, 0)), 4, 10, 4 },
                    { 40, "Phong cách dạy chuyên nghiệp và hiệu quả.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3934), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3935), new TimeSpan(0, 7, 0, 0, 0)), 5, 10, 5 },
                    { 41, "HLV dày dạn kinh nghiệm, đáng học hỏi.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3937), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3938), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 2 },
                    { 42, "Dạy dễ hiểu, luôn hỗ trợ đúng lúc.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3940), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3941), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 3 },
                    { 43, "Phong thái chuyên nghiệp, vui vẻ.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3942), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3943), new TimeSpan(0, 7, 0, 0, 0)), 4, 11, 6 },
                    { 44, "Giúp tôi cải thiện kỹ thuật rõ rệt.", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3945), new TimeSpan(0, 7, 0, 0, 0)), null, null, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3946), new TimeSpan(0, 7, 0, 0, 0)), 5, 11, 7 }
                });

            migrationBuilder.InsertData(
                table: "SportsComplexes",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "ImageUrls", "LastUpdatedBy", "LastUpdatedTime", "Name", "OwnerId", "Status", "Type" },
                values: new object[,]
                {
                    { 1, "123 Đường A, Quận 1, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3465), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3467), new TimeSpan(0, 7, 0, 0, 0)), "Khu Thể Thao ABC", 3, "Active", "Bóng đá" },
                    { 2, "456 Đường B, Quận 5, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3474), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3482), new TimeSpan(0, 7, 0, 0, 0)), "Khu Thể Thao DEF", 3, "Active", "Cầu lông" },
                    { 3, "789 Đường C, Quận 3, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3521), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3522), new TimeSpan(0, 7, 0, 0, 0)), "Khu Thể Thao GHI", 3, "Active", "Pickleball" },
                    { 4, "321 Đường D, Quận 4, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3524), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3526), new TimeSpan(0, 7, 0, 0, 0)), "Khu Thể Thao JKL", 3, "Active", "Pickleball" },
                    { 5, "654 Đường E, Quận 6, TP.HCM", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3527), new TimeSpan(0, 7, 0, 0, 0)), null, null, "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3528), new TimeSpan(0, 7, 0, 0, 0)), "Khu Thể Thao MNO", 3, "Active", "Cầu lông" }
                });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "ImageUrls", "LastUpdatedBy", "LastUpdatedTime", "Name", "PricePerHour", "SportsComplexId", "Status" },
                values: new object[,]
                {
                    { 1, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3584), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân cỏ nhân tạo tiêu chuẩn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3585), new TimeSpan(0, 7, 0, 0, 0)), "Sân 5 người A", 200000m, 1, "Active" },
                    { 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3588), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chất lượng cao, đèn chiếu sáng ban đêm", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3589), new TimeSpan(0, 7, 0, 0, 0)), "Sân 7 người B", 350000m, 1, "Active" },
                    { 3, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3592), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3593), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông A", 150000m, 2, "Active" },
                    { 4, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3595), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn phong trào", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3596), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông B", 100000m, 2, "Active" },
                    { 5, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3598), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, chất lượng cao", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3599), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball A", 250000m, 3, "Active" },
                    { 6, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3602), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, có mái che", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3603), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball B", 300000m, 3, "Active" },
                    { 7, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3681), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân chuẩn FIBA, sàn gỗ cao cấp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3682), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball C", 400000m, 4, "Active" },
                    { 8, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3684), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân phong trào, phù hợp nhóm bạn", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3685), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball D", 250000m, 4, "Active" },
                    { 9, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3687), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân luyện tập cá nhân", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3688), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball E", 200000m, 4, "Active" },
                    { 10, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3690), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân trong nhà, chuẩn thi đấu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3691), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông C", 300000m, 5, "Active" },
                    { 11, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3693), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân ngoài trời, thoáng mát", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3694), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông D", 200000m, 5, "Active" },
                    { 12, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3697), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân thi đấu chuyên nghiệp", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3698), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông E", 350000m, 5, "Active" },
                    { 13, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3700), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân tiêu chuẩn quốc tế", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3701), new TimeSpan(0, 7, 0, 0, 0)), "Sân Pickleball F", 280000m, 3, "Active" },
                    { 14, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3703), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Sân mở ban đêm, có đèn chiếu", "[\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media\\u0026token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media\\u0026token=cc65bd49-e3df-4a51-b513-c7bb534b63d4\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media\\u0026token=e239b164-1d55-437b-889d-19781c61a8b0\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media\\u0026token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752\",\"https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media\\u0026token=b2b2f296-f847-4c95-96d3-50ae7fc827a0\"]", null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(3704), new TimeSpan(0, 7, 0, 0, 0)), "Sân cầu lông F", 270000m, 4, "Active" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CourtId", "CreatedBy", "CreatedTime", "DeletedBy", "DeletedTime", "Description", "HostId", "LastUpdatedBy", "LastUpdatedTime", "MaxPlayers", "Name", "RoomFee", "ScheduledTime", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4048), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tập hợp anh em giao lưu bóng đá sáng thứ 7.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4049), new TimeSpan(0, 7, 0, 0, 0)), 10, "Team Sáng Thứ 7", 30000m, new DateTime(2025, 5, 20, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 2, 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4074), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm đối đá giao hữu 7v7 buổi tối.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4075), new TimeSpan(0, 7, 0, 0, 0)), 14, "Giao lưu buổi tối", 50000m, new DateTime(2025, 5, 21, 20, 0, 0, 0, DateTimeKind.Local), "Completed" },
                    { 3, 3, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4079), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đánh cầu cuối tuần, vui vẻ là chính.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4080), new TimeSpan(0, 7, 0, 0, 0)), 4, "Badminton Team CN", 20000m, new DateTime(2025, 5, 22, 9, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 4, 4, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4083), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Tìm team giao lưu vào chiều thứ 5.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4084), new TimeSpan(0, 7, 0, 0, 0)), 10, "Đá bóng chiều thứ 5", 30000m, new DateTime(2025, 5, 23, 17, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 5, 5, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4087), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Pickleball nhẹ nhàng chủ nhật.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4088), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball sáng CN", 40000m, new DateTime(2025, 5, 24, 7, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 6, 6, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4091), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Giao lưu Pickleball chiều cuối tuần.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4092), new TimeSpan(0, 7, 0, 0, 0)), 4, "Pickleball chiều thứ 7", 50000m, new DateTime(2025, 5, 25, 15, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 7, 7, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4095), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team Pickleball tụ tập tối thứ 3.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4096), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball tối thứ 3", 25000m, new DateTime(2025, 5, 22, 20, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 8, 8, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4099), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Vui là chính, ai cũng có thể tham gia.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4100), new TimeSpan(0, 7, 0, 0, 0)), 10, "Pickleball phong trào", 20000m, new DateTime(2025, 5, 21, 18, 0, 0, 0, DateTimeKind.Local), "Full" },
                    { 9, 10, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4103), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Đội hình luyện tập chuẩn bị giải.", 3, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4104), new TimeSpan(0, 7, 0, 0, 0)), 12, "Cầu lông tập luyện", 35000m, new DateTime(2025, 5, 20, 16, 0, 0, 0, DateTimeKind.Local), "Waiting" },
                    { 10, 12, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4107), new TimeSpan(0, 7, 0, 0, 0)), null, null, "Team cầu lông nhẹ nhàng tối làm về.", 2, null, new DateTimeOffset(new DateTime(2025, 5, 19, 7, 35, 30, 692, DateTimeKind.Unspecified).AddTicks(4108), new TimeSpan(0, 7, 0, 0, 0)), 4, "Chơi cầu lông tối", 25000m, new DateTime(2025, 5, 23, 19, 0, 0, 0, DateTimeKind.Local), "Waiting" }
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
