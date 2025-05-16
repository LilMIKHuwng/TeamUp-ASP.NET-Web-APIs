using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Repositories.Entity;

namespace TeamUp.Repositories.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // DbSet
        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();


        public virtual DbSet<SportsComplex> SportsComplexes { get; set; }
        public virtual DbSet<Court> Courts { get; set; }
        public virtual DbSet<CourtBooking> CourtBookings { get; set; }
        public virtual DbSet<CoachBooking> CoachBookings { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomPlayer> RoomPlayers { get; set; }
        public virtual DbSet<RoomJoinRequest> RoomJoinRequests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }
        public virtual DbSet<Package> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<CourtBooking>(entity =>
            {
                entity.HasOne(cb => cb.User)
                    .WithMany(u => u.CourtBookings)
                    .HasForeignKey(cb => cb.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // ❌ Không dùng CASCADE

                entity.HasOne(cb => cb.Court)
                    .WithMany(c => c.CourtBookings)
                    .HasForeignKey(cb => cb.CourtId)
                    .OnDelete(DeleteBehavior.Restrict); // ❌ Không dùng CASCADE
            });

            builder.Entity<Room>(entity =>
            {
                entity.HasOne(r => r.Host)
                    .WithMany(u => u.HostedRooms)
                    .HasForeignKey(r => r.HostId)
                    .OnDelete(DeleteBehavior.Restrict); // Không dùng cascade

                entity.HasOne(r => r.Court)
                    .WithMany(c => c.Rooms)
                    .HasForeignKey(r => r.CourtId)
                    .OnDelete(DeleteBehavior.Restrict); // Không dùng cascade
            });

            builder.Entity<CoachBooking>()
                .HasOne(cb => cb.Coach)
                .WithMany(u => u.CoachBookingsAsCoach)
                .HasForeignKey(cb => cb.CoachId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CoachBooking>()
                .HasOne(cb => cb.Player)
                .WithMany(u => u.CoachBookingsAsPlayer)
                .HasForeignKey(cb => cb.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.RatingsGiven)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Rating>()
                .HasOne(r => r.Reviewee)
                .WithMany(u => u.RatingsReceived)
                .HasForeignKey(r => r.RevieweeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserMessage>(builder =>
            {
                builder.HasOne(x => x.Sender)
                    .WithMany(x => x.SentMessages)
                    .HasForeignKey(x => x.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(x => x.Recipient)
                    .WithMany(x => x.ReceivedMessages)
                    .HasForeignKey(x => x.RecipientId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed roles
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Quản trị viên" },
                new ApplicationRole { Id = 2, Name = "User", NormalizedName = "USER", Description = "Người dùng thông thường" },
                new ApplicationRole { Id = 3, Name = "Owner", NormalizedName = "OWNER", Description = "Chủ sân thể thao" },
                new ApplicationRole { Id = 4, Name = "Coach", NormalizedName = "COACH", Description = "Coach / Trainer" }
            );

            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@teamup.com",
                NormalizedEmail = "ADMIN@TEAMUP.COM",
                EmailConfirmed = true,
                FullName = "System Admin",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Status = 1,
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            var playerUser = new ApplicationUser
            {
                Id = 2,
                UserName = "player",
                NormalizedUserName = "PLAYER",
                Email = "player@teamup.com",
                NormalizedEmail = "PLAYER@TEAMUP.COM",
                EmailConfirmed = true,
                FullName = "Người Chơi A",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Status = 1,
            };
            playerUser.PasswordHash = hasher.HashPassword(playerUser, "Player@123");

            var courtOwnerUser = new ApplicationUser
            {
                Id = 3,
                UserName = "chusan",
                NormalizedUserName = "CHUSAN",
                Email = "chusan@teamup.com",
                NormalizedEmail = "CHUSAN@TEAMUP.COM",
                EmailConfirmed = true,
                FullName = "Chủ Sân A",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Status = 1,
            };
            courtOwnerUser.PasswordHash = hasher.HashPassword(courtOwnerUser, "Chusan@123");

            var coachUser = new ApplicationUser
            {
                Id = 4,
                UserName = "coach",
                NormalizedUserName = "COACH",
                Email = "coach@teamup.com",
                NormalizedEmail = "COACH@TEAMUP.COM",
                EmailConfirmed = true,
                FullName = "HLV B",
                Specialty = "Bóng đá",
                Certificate = "Chứng chỉ A",
                PricePerSession = 200000,
                WorkingAddress = "Sân ABC, Quận 1",
                WorkingDate = "Thứ 2, 4, 6",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Status = 1,
            };
            coachUser.PasswordHash = hasher.HashPassword(coachUser, "Coach@123");

            builder.Entity<ApplicationUser>().HasData(adminUser, playerUser, courtOwnerUser, coachUser);

            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole { UserId = 1, RoleId = 1 }, // Admin
                new ApplicationUserRole { UserId = 2, RoleId = 2 }, // Người Chơi
                new ApplicationUserRole { UserId = 3, RoleId = 3 }, // Chủ Sân
                new ApplicationUserRole { UserId = 4, RoleId = 4 }  // Huấn Luyện Viên
            );

            builder.Entity<Package>().HasData(
                new Package
                {
                    Id = 1,
                    Name = "Basic",
                    Price = 399000,
                    Description = "Gói dịch vụ 365 ngày",
                    DurationDays = 30,
                    Type = "PackageHLV"
                },
                new Package
                {
                    Id = 2,
                    Name = "Premium",
                    Price = 599000,
                    Description = "Gói cao cấp 1095 ngày",
                    DurationDays = 90,
                    Type = "PackageHLV"
                },
                new Package
                {
                    Id = 3,
                    Name = "Basic",
                    Price = 199000,
                    Description = "Gói cao cấp 1095 ngày",
                    DurationDays = 30,
                    Type = "PackageHLV"
                }
            );

            builder.Entity<SportsComplex>().HasData(
                new SportsComplex
                {
                    Id = 1,
                    Name = "Khu Thể Thao ABC",
                    Type = "Bóng đá",
                    Address = "123 Đường A, Quận 1, TP.HCM",
                    OwnerId = 3, // Chủ sân có Id = 3
                    Status = "Active",
                    ImageUrls = new List<string> { "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media&token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752", "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media&token=b2b2f296-f847-4c95-96d3-50ae7fc827a0" }
                },
                new SportsComplex
                {
                    Id = 2,
                    Name = "Khu Thể Thao DEF",
                    Type = "Cầu lông",
                    Address = "456 Đường B, Quận 5, TP.HCM",
                    OwnerId = 3, // Chủ sân có Id = 3
                    Status = "Active",
                    ImageUrls = new List<string> { "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media&token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c", "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media&token=cc65bd49-e3df-4a51-b513-c7bb534b63d4" }
                }

            );

            var firebaseImageUrls = new List<string>
            {
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media&token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media&token=cc65bd49-e3df-4a51-b513-c7bb534b63d4",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media&token=e239b164-1d55-437b-889d-19781c61a8b0",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media&token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media&token=b2b2f296-f847-4c95-96d3-50ae7fc827a0"
            };

            builder.Entity<Court>().HasData(
                new Court
                {
                    Id = 1,
                    SportsComplexId = 1,
                    Name = "Sân 5 người A",
                    Description = "Sân cỏ nhân tạo tiêu chuẩn",
                    PricePerHour = 200000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 2,
                    SportsComplexId = 1,
                    Name = "Sân 7 người B",
                    Description = "Sân chất lượng cao, đèn chiếu sáng ban đêm",
                    PricePerHour = 350000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 3,
                    SportsComplexId = 2,
                    Name = "Sân cầu lông A",
                    Description = "Sân trong nhà, chuẩn thi đấu",
                    PricePerHour = 150000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 4,
                    SportsComplexId = 2,
                    Name = "Sân cầu lông B",
                    Description = "Sân chuẩn phong trào",
                    PricePerHour = 100000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                }
            );

            builder.Entity<Rating>().HasData(
                new Rating
                {
                    Id = 1,
                    ReviewerId = 2, // player
                    RevieweeId = 4, // coach
                    RatingValue = 5,
                    Comment = "HLV rất chuyên nghiệp, hướng dẫn tận tình."
                },
                new Rating
                {
                    Id = 2,
                    ReviewerId = 2, // player
                    RevieweeId = 3, // court owner
                    RatingValue = 4,
                    Comment = "Chủ sân thân thiện, sân sạch đẹp."
                }
            );

            builder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    HostId = 2, // player
                    CourtId = 1,
                    Name = "Team Sáng Thứ 7",
                    MaxPlayers = 10,
                    Description = "Tập hợp anh em giao lưu bóng đá sáng thứ 7.",
                    RoomFee = 30000,
                    Status = "Waiting",
                    ScheduledTime = DateTime.Today.AddDays(1).AddHours(7) // 7h sáng ngày mai
                },
                new Room
                {
                    Id = 2,
                    HostId = 3, // chủ sân
                    CourtId = 2,
                    Name = "Giao lưu buổi tối",
                    MaxPlayers = 14,
                    Description = "Tìm đối đá giao hữu 7v7 buổi tối.",
                    RoomFee = 50000,
                    Status = "Completed",
                    ScheduledTime = DateTime.Today.AddDays(2).AddHours(20) // 20h ngày kia
                },
                new Room
                {
                    Id = 3,
                    HostId = 2, // player
                    CourtId = 3,
                    Name = "Badminton Team CN",
                    MaxPlayers = 4,
                    Description = "Đánh cầu cuối tuần, vui vẻ là chính.",
                    RoomFee = 20000,
                    Status = "Full",
                    ScheduledTime = DateTime.Today.AddDays(3).AddHours(9) // 9h sáng 3 ngày nữa
                }
            );
        }

    }
}
