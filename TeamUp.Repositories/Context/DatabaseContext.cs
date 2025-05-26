using BabyCare.Core.Utils;
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
        public virtual DbSet<PaymentTemp> PaymentTemps { get; set; }


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
                Status = 1
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
                PricePerSession = 5000,
                WorkingAddress = "Sân ABC, Quận 1",
                WorkingDate = "Thứ 2, 4, 6",
                Experience = "5 năm huấn luyện đội trẻ U15",
                TargetObject = "Trẻ em, thanh thiếu niên",
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Status = 1,
                StatusForCoach = "Active",
                Type = "Bóng đá",
            };
            coachUser.PasswordHash = hasher.HashPassword(coachUser, "Coach@123");


            builder.Entity<ApplicationUser>().HasData(adminUser, playerUser, courtOwnerUser, coachUser);

            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole { UserId = 1, RoleId = 1 }, // Admin
                new ApplicationUserRole { UserId = 2, RoleId = 2 }, // Người Chơi
                new ApplicationUserRole { UserId = 3, RoleId = 3 }, // Chủ Sân
                new ApplicationUserRole { UserId = 4, RoleId = 4 }  // Huấn Luyện Viên
            );

            var coachUsers = new List<ApplicationUser>();
            var coachRoles = new List<ApplicationUserRole>();

            coachUsers.AddRange(new[]
            {
                new ApplicationUser
                {
                    Id = 5,
                    UserName = "coach1",
                    NormalizedUserName = "COACH1",
                    Email = "coach1@teamup.com",
                    NormalizedEmail = "COACH1@TEAMUP.COM",
                    EmailConfirmed = true,
                    FullName = "HLV 1",
                    Specialty = "Bóng đá",
                    Certificate = "Chứng chỉ B",
                    PricePerSession = 250000,
                    WorkingAddress = "Sân XYZ, Quận 5",
                    WorkingDate = "Thứ 3, 5",
                    Experience = "5 năm huấn luyện đội trẻ U15",
                    TargetObject = "Trẻ em, thanh thiếu niên",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Status = 1,
                    StatusForCoach = "Active",
                    Type = SystemConstant.Type.Soccer,
                },
                new ApplicationUser
                {
                    Id = 6,
                    UserName = "coach2",
                    NormalizedUserName = "COACH2",
                    Email = "coach2@teamup.com",
                    NormalizedEmail = "COACH2@TEAMUP.COM",
                    EmailConfirmed = true,
                    FullName = "HLV 2",
                    Specialty = "Cầu lông",
                    Certificate = "Chứng chỉ C",
                    PricePerSession = 180000,
                    WorkingAddress = "Sân Lông, Quận 2",
                    WorkingDate = "Thứ 2, 4",
                    Experience = "3 năm huấn luyện cá nhân và nhóm",
                    TargetObject = "Người lớn, học sinh",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Status = 1,
                    StatusForCoach = "Active",
                    Type = SystemConstant.Type.Badminton
                },
                new ApplicationUser
                {
                    Id = 7,
                    UserName = "coach3",
                    NormalizedUserName = "COACH3",
                    Email = "coach3@teamup.com",
                    NormalizedEmail = "COACH3@TEAMUP.COM",
                    EmailConfirmed = true,
                    FullName = "HLV 3",
                    Specialty = "Pickleball",
                    Certificate = "Chứng chỉ D",
                    PricePerSession = 220000,
                    WorkingAddress = "Sân PB, Quận 7",
                    WorkingDate = "Thứ 6, 7",
                    Experience = "2 năm giảng dạy cho người mới bắt đầu",
                    TargetObject = "Người mới chơi, người cao tuổi",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Status = 1,
                    StatusForCoach = "Active",
                    Type = SystemConstant.Type.PickleBall
                },
                new ApplicationUser
                {
                    Id = 8,
                    UserName = "coach4",
                    NormalizedUserName = "COACH4",
                    Email = "coach4@teamup.com",
                    NormalizedEmail = "COACH4@TEAMUP.COM",
                    EmailConfirmed = true,
                    FullName = "HLV 4",
                    Specialty = "Bóng đá",
                    Certificate = "Chứng chỉ A",
                    PricePerSession = 230000,
                    WorkingAddress = "Sân K, Quận 6",
                    WorkingDate = "Thứ 3, 6",
                    Experience = "8 năm làm HLV cho các đội phong trào",
                    TargetObject = "Người lớn, sinh viên",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Status = 1,
                    StatusForCoach = "Active",
                    Type = SystemConstant.Type.Soccer
                },
                new ApplicationUser
                {
                    Id = 9,
                    UserName = "coach5",
                    NormalizedUserName = "COACH5",
                    Email = "coach5@teamup.com",
                    NormalizedEmail = "COACH5@TEAMUP.COM",
                    EmailConfirmed = true,
                    FullName = "HLV 5",
                    Specialty = "Cầu lông",
                    Certificate = "Chứng chỉ B",
                    PricePerSession = 190000,
                    WorkingAddress = "Sân Mây, Quận 10",
                    WorkingDate = "Thứ 2, 5",
                    Experience = "4 năm giảng dạy tại trung tâm thể thao",
                    TargetObject = "Thiếu nhi, người đi làm",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Status = 1,
                    StatusForCoach = "Active",
                    Type = SystemConstant.Type.Badminton
                },
                new ApplicationUser
                {
                    Id = 10,
                    UserName = "coach6",
                    NormalizedUserName = "COACH6",
                    Email = "coach6@teamup.com",
                    NormalizedEmail = "COACH6@TEAMUP.COM",
                    EmailConfirmed = true,
                    FullName = "HLV 6",
                    Specialty = "Pickleball",
                    Certificate = "Chứng chỉ E",
                    PricePerSession = 210000,
                    WorkingAddress = "Sân Pick, Quận 9",
                    WorkingDate = "Thứ 4, 7",
                    Experience = "1 năm hỗ trợ luyện tập cơ bản và thi đấu",
                    TargetObject = "Người cao tuổi, học viên nữ",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Status = 1,
                    StatusForCoach = "Active",
                    Type = SystemConstant.Type.PickleBall
                },
                new ApplicationUser
                {
                    Id = 11,
                    UserName = "coach7",
                    NormalizedUserName = "COACH7",
                    Email = "coach7@teamup.com",
                    NormalizedEmail = "COACH7@TEAMUP.COM",
                    EmailConfirmed = true,
                    FullName = "HLV 7",
                    Specialty = "Bóng đá",
                    Certificate = "Chứng chỉ C",
                    PricePerSession = 240000,
                    WorkingAddress = "Sân Gold, Quận Tân Bình",
                    WorkingDate = "Thứ 3, 5, 7",
                    Experience = "6 năm giảng dạy các lớp nâng cao",
                    TargetObject = "Học viên đã có nền tảng",
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Status = 1,
                    StatusForCoach = "Active",
                    Type = SystemConstant.Type.Soccer
                }

            });

            // Hash passwords
            foreach (var user in coachUsers)
            {
                user.PasswordHash = hasher.HashPassword(user, "Coach@123");
                coachRoles.Add(new ApplicationUserRole { UserId = user.Id, RoleId = 4 }); // Huấn luyện viên
            }

            // Add to modelBuilder
            builder.Entity<ApplicationUser>().HasData(coachUsers);
            builder.Entity<ApplicationUserRole>().HasData(coachRoles);

            builder.Entity<Package>().HasData(
                new Package
                {
                    Id = 1,
                    Name = "Basic",
                    Price = 10000,
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

            var firebaseImageUrls = new List<string>
            {
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media&token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media&token=cc65bd49-e3df-4a51-b513-c7bb534b63d4",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest3.png?alt=media&token=e239b164-1d55-437b-889d-19781c61a8b0",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media&token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752",
                "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media&token=b2b2f296-f847-4c95-96d3-50ae7fc827a0"
            };

            builder.Entity<SportsComplex>().HasData(
                new SportsComplex
                {
                    Id = 1,
                    Name = "Khu Thể Thao ABC",
                    Type = "Bóng đá",
                    Address = "Sân bóng đá Tao Đàn, 1 Huyền Trân Công Chúa, Quận 1, TP.HCM",
                    OwnerId = 3,
                    Status = "Active",
                    Latitude = 10.773444,
                    Longitude = 106.690933,
                    ImageUrls = new List<string>
                    {
                        "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest4.png?alt=media&token=1a0da7ef-2eb3-48e9-a9de-1e2866fe8752",
                        "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest5.png?alt=media&token=b2b2f296-f847-4c95-96d3-50ae7fc827a0"
                    }
                },
                new SportsComplex
                {
                    Id = 2,
                    Name = "Khu Thể Thao DEF",
                    Type = "Cầu lông",
                    Address = "Sân cầu lông Hồ Kỳ Hòa, 27 Cao Thắng, Quận 3, TP.HCM",
                    OwnerId = 3,
                    Status = "Active",
                    Latitude = 10.768493,
                    Longitude = 106.681771,
                    ImageUrls = new List<string>
                    {
                        "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest1.png?alt=media&token=0c05a2e7-869d-4e0c-98b2-41dd842fe90c",
                        "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Ftest2.png?alt=media&token=cc65bd49-e3df-4a51-b513-c7bb534b63d4"
                    }
                },
                new SportsComplex
                {
                    Id = 3,
                    Name = "Khu Thể Thao GHI",
                    Type = "Pickleball",
                    Address = "Sân thể thao Rạch Miễu, 1 Hoa Phượng, Phú Nhuận, TP.HCM",
                    OwnerId = 3,
                    Status = "Active",
                    Latitude = 10.800005,
                    Longitude = 106.683813,
                    ImageUrls = firebaseImageUrls
                },
                new SportsComplex
                {
                    Id = 4,
                    Name = "Khu Thể Thao JKL",
                    Type = "Pickleball",
                    Address = "Sân thể thao Vạn Tường, 59A Nguyễn Du, Quận 1, TP.HCM",
                    OwnerId = 3,
                    Status = "Active",
                    Latitude = 10.776230,
                    Longitude = 106.699208,
                    ImageUrls = firebaseImageUrls
                },
                new SportsComplex
                {
                    Id = 5,
                    Name = "Khu Thể Thao MNO",
                    Type = "Cầu lông",
                    Address = "Sân cầu lông Quận 6, 42 Nguyễn Văn Luông, Quận 6, TP.HCM",
                    OwnerId = 3,
                    Status = "Active",
                    Latitude = 10.737717,
                    Longitude = 106.628582,
                    ImageUrls = firebaseImageUrls
                }
            );



            builder.Entity<Court>().HasData(
                new Court
                {
                    Id = 1,
                    SportsComplexId = 1,
                    Name = "Sân 5 người A",
                    Description = "Sân cỏ nhân tạo tiêu chuẩn",
                    PricePerHour = 10000,
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
                },
                new Court
                {
                    Id = 5,
                    SportsComplexId = 3,
                    Name = "Sân Pickleball A",
                    Description = "Sân ngoài trời, chất lượng cao",
                    PricePerHour = 250000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 6,
                    SportsComplexId = 3,
                    Name = "Sân Pickleball B",
                    Description = "Sân trong nhà, có mái che",
                    PricePerHour = 300000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 7,
                    SportsComplexId = 4,
                    Name = "Sân Pickleball C",
                    Description = "Sân chuẩn FIBA, sàn gỗ cao cấp",
                    PricePerHour = 400000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 8,
                    SportsComplexId = 4,
                    Name = "Sân Pickleball D",
                    Description = "Sân phong trào, phù hợp nhóm bạn",
                    PricePerHour = 250000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 9,
                    SportsComplexId = 4,
                    Name = "Sân Pickleball E",
                    Description = "Sân luyện tập cá nhân",
                    PricePerHour = 200000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 10,
                    SportsComplexId = 5,
                    Name = "Sân cầu lông C",
                    Description = "Sân trong nhà, chuẩn thi đấu",
                    PricePerHour = 300000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 11,
                    SportsComplexId = 5,
                    Name = "Sân cầu lông D",
                    Description = "Sân ngoài trời, thoáng mát",
                    PricePerHour = 200000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 12,
                    SportsComplexId = 5,
                    Name = "Sân cầu lông E",
                    Description = "Sân thi đấu chuyên nghiệp",
                    PricePerHour = 350000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 13,
                    SportsComplexId = 3,
                    Name = "Sân Pickleball F",
                    Description = "Sân tiêu chuẩn quốc tế",
                    PricePerHour = 280000,
                    Status = "Active",
                    ImageUrls = firebaseImageUrls
                },
                new Court
                {
                    Id = 14,
                    SportsComplexId = 4,
                    Name = "Sân cầu lông F",
                    Description = "Sân mở ban đêm, có đèn chiếu",
                    PricePerHour = 270000,
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
                },
                new Rating
                {
                    Id = 3,
                    ReviewerId = 2,
                    RevieweeId = 3,
                    RatingValue = 5,
                    Comment = "Chủ sân hỗ trợ rất nhiệt tình và chuyên nghiệp."
                },
                new Rating
                {
                    Id = 4,
                    ReviewerId = 2,
                    RevieweeId = 3,
                    RatingValue = 4,
                    Comment = "Không gian rộng rãi, dễ đặt lịch."
                },
                new Rating
                {
                    Id = 5,
                    ReviewerId = 4,
                    RevieweeId = 3,
                    RatingValue = 3,
                    Comment = "Thỉnh thoảng hơi chậm phản hồi tin nhắn."
                },
                new Rating
                {
                    Id = 6,
                    ReviewerId = 4,
                    RevieweeId = 3,
                    RatingValue = 5,
                    Comment = "Chủ sân dễ tính, rất dễ thương!"
                },
                new Rating
                {
                    Id = 7,
                    ReviewerId = 2,
                    RevieweeId = 3,
                    RatingValue = 4,
                    Comment = "Sân tốt, chủ sân chu đáo."
                },
                new Rating
                {
                    Id = 8,
                    ReviewerId = 2,
                    RevieweeId = 3,
                    RatingValue = 5,
                    Comment = "Quản lý chuyên nghiệp, xử lý tình huống nhanh chóng."
                },
                new Rating
                {
                    Id = 9,
                    ReviewerId = 1,
                    RevieweeId = 3,
                    RatingValue = 4,
                    Comment = "Dịch vụ ổn, sẽ quay lại lần nữa."
                },
                new Rating
                {
                    Id = 10,
                    ReviewerId = 1,
                    RevieweeId = 3,
                    RatingValue = 5,
                    Comment = "Chủ sân rất thân thiện, đáng tin cậy."
                },
                new Rating
                {
                    Id = 11,
                    ReviewerId = 1,
                    RevieweeId = 3,
                    RatingValue = 3,
                    Comment = "Cần cải thiện thời gian mở cửa đúng giờ hơn."
                },
                new Rating
                {
                    Id = 12,
                    ReviewerId = 1,
                    RevieweeId = 3,
                    RatingValue = 5,
                    Comment = "Chất lượng phục vụ tuyệt vời!"
                }
            );

            builder.Entity<Rating>().HasData(
                // Coach Id = 4
                new Rating { Id = 13, ReviewerId = 2, RevieweeId = 4, RatingValue = 5, Comment = "HLV rất tâm huyết và chuyên nghiệp." },
                new Rating { Id = 14, ReviewerId = 3, RevieweeId = 4, RatingValue = 4, Comment = "Giảng dạy dễ hiểu, thái độ thân thiện." },
                new Rating { Id = 15, ReviewerId = 5, RevieweeId = 4, RatingValue = 4, Comment = "Tận tình hỗ trợ, kỹ năng tốt." },
                new Rating { Id = 16, ReviewerId = 6, RevieweeId = 4, RatingValue = 5, Comment = "Cực kỳ có trách nhiệm với học viên." },

                // Coach Id = 5
                new Rating { Id = 17, ReviewerId = 2, RevieweeId = 5, RatingValue = 4, Comment = "Phương pháp huấn luyện rõ ràng." },
                new Rating { Id = 18, ReviewerId = 3, RevieweeId = 5, RatingValue = 5, Comment = "Đúng giờ, vui vẻ và tận tâm." },
                new Rating { Id = 19, ReviewerId = 6, RevieweeId = 5, RatingValue = 5, Comment = "Cải thiện kỹ năng rõ rệt sau vài buổi." },
                new Rating { Id = 20, ReviewerId = 7, RevieweeId = 5, RatingValue = 4, Comment = "Kỹ năng truyền đạt tốt, dễ hiểu." },

                // Coach Id = 6
                new Rating { Id = 21, ReviewerId = 2, RevieweeId = 6, RatingValue = 3, Comment = "Cần tăng tính kỷ luật, nhưng kỹ năng ổn." },
                new Rating { Id = 22, ReviewerId = 3, RevieweeId = 6, RatingValue = 4, Comment = "Nhiệt tình, vui vẻ, luôn động viên học viên." },
                new Rating { Id = 23, ReviewerId = 4, RevieweeId = 6, RatingValue = 5, Comment = "Bài tập sáng tạo, dễ áp dụng." },
                new Rating { Id = 24, ReviewerId = 5, RevieweeId = 6, RatingValue = 4, Comment = "Có chuyên môn cao, dễ tiếp cận." },

                // Coach Id = 7
                new Rating { Id = 25, ReviewerId = 2, RevieweeId = 7, RatingValue = 5, Comment = "Giúp tôi nâng cao thể lực rõ rệt." },
                new Rating { Id = 26, ReviewerId = 3, RevieweeId = 7, RatingValue = 5, Comment = "Đào tạo bài bản, bài tập phù hợp trình độ." },
                new Rating { Id = 27, ReviewerId = 4, RevieweeId = 7, RatingValue = 4, Comment = "Thời gian linh hoạt, hỗ trợ tốt." },
                new Rating { Id = 28, ReviewerId = 6, RevieweeId = 7, RatingValue = 5, Comment = "Có kinh nghiệm thực tế, phong cách giảng dạy chuyên nghiệp." },

                // Coach Id = 8
                new Rating { Id = 29, ReviewerId = 2, RevieweeId = 8, RatingValue = 4, Comment = "Khả năng truyền đạt tốt, thân thiện." },
                new Rating { Id = 30, ReviewerId = 3, RevieweeId = 8, RatingValue = 4, Comment = "Kiến thức vững, giao tiếp tốt." },
                new Rating { Id = 31, ReviewerId = 4, RevieweeId = 8, RatingValue = 5, Comment = "Luôn khuyến khích học viên cố gắng." },
                new Rating { Id = 32, ReviewerId = 5, RevieweeId = 8, RatingValue = 5, Comment = "Cực kỳ chuyên nghiệp và dễ thương." },

                // Coach Id = 9
                new Rating { Id = 33, ReviewerId = 2, RevieweeId = 9, RatingValue = 5, Comment = "Bài giảng sáng tạo, dễ hiểu." },
                new Rating { Id = 34, ReviewerId = 3, RevieweeId = 9, RatingValue = 4, Comment = "Có nhiều kinh nghiệm thực chiến." },
                new Rating { Id = 35, ReviewerId = 6, RevieweeId = 9, RatingValue = 4, Comment = "Tận tâm với học viên, hỗ trợ thêm ngoài giờ." },
                new Rating { Id = 36, ReviewerId = 7, RevieweeId = 9, RatingValue = 5, Comment = "Chuyên nghiệp, luôn đúng giờ." },

                // Coach Id = 10
                new Rating { Id = 37, ReviewerId = 2, RevieweeId = 10, RatingValue = 5, Comment = "Nội dung giảng dạy phù hợp từng người." },
                new Rating { Id = 38, ReviewerId = 3, RevieweeId = 10, RatingValue = 5, Comment = "Tạo động lực cho học viên rất tốt." },
                new Rating { Id = 39, ReviewerId = 4, RevieweeId = 10, RatingValue = 4, Comment = "Rất tận tình, thân thiện." },
                new Rating { Id = 40, ReviewerId = 5, RevieweeId = 10, RatingValue = 5, Comment = "Phong cách dạy chuyên nghiệp và hiệu quả." },

                // Coach Id = 11
                new Rating { Id = 41, ReviewerId = 2, RevieweeId = 11, RatingValue = 5, Comment = "HLV dày dạn kinh nghiệm, đáng học hỏi." },
                new Rating { Id = 42, ReviewerId = 3, RevieweeId = 11, RatingValue = 5, Comment = "Dạy dễ hiểu, luôn hỗ trợ đúng lúc." },
                new Rating { Id = 43, ReviewerId = 6, RevieweeId = 11, RatingValue = 4, Comment = "Phong thái chuyên nghiệp, vui vẻ." },
                new Rating { Id = 44, ReviewerId = 7, RevieweeId = 11, RatingValue = 5, Comment = "Giúp tôi cải thiện kỹ thuật rõ rệt." }
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
                },
                new Room
                {
                    Id = 4,
                    HostId = 2,
                    CourtId = 4,
                    Name = "Đá bóng chiều thứ 5",
                    MaxPlayers = 10,
                    Description = "Tìm team giao lưu vào chiều thứ 5.",
                    RoomFee = 30000,
                    Status = "Waiting",
                    ScheduledTime = DateTime.Today.AddDays(4).AddHours(17)
                },
                new Room
                {
                    Id = 5,
                    HostId = 3,
                    CourtId = 5,
                    Name = "Pickleball sáng CN",
                    MaxPlayers = 4,
                    Description = "Pickleball nhẹ nhàng chủ nhật.",
                    RoomFee = 40000,
                    Status = "Waiting",
                    ScheduledTime = DateTime.Today.AddDays(5).AddHours(7)
                },
                new Room
                {
                    Id = 6,
                    HostId = 2,
                    CourtId = 6,
                    Name = "Pickleball chiều thứ 7",
                    MaxPlayers = 4,
                    Description = "Giao lưu Pickleball chiều cuối tuần.",
                    RoomFee = 50000,
                    Status = "Waiting",
                    ScheduledTime = DateTime.Today.AddDays(6).AddHours(15)
                },
                new Room
                {
                    Id = 7,
                    HostId = 3,
                    CourtId = 7,
                    Name = "Pickleball tối thứ 3",
                    MaxPlayers = 10,
                    Description = "Team Pickleball tụ tập tối thứ 3.",
                    RoomFee = 25000,
                    Status = "Waiting",
                    ScheduledTime = DateTime.Today.AddDays(3).AddHours(20)
                },
                new Room
                {
                    Id = 8,
                    HostId = 2,
                    CourtId = 8,
                    Name = "Pickleball phong trào",
                    MaxPlayers = 10,
                    Description = "Vui là chính, ai cũng có thể tham gia.",
                    RoomFee = 20000,
                    Status = "Full",
                    ScheduledTime = DateTime.Today.AddDays(2).AddHours(18)
                },
                new Room
                {
                    Id = 9,
                    HostId = 3,
                    CourtId = 10,
                    Name = "Cầu lông tập luyện",
                    MaxPlayers = 12,
                    Description = "Đội hình luyện tập chuẩn bị giải.",
                    RoomFee = 35000,
                    Status = "Waiting",
                    ScheduledTime = DateTime.Today.AddDays(1).AddHours(16)
                },
                new Room
                {
                    Id = 10,
                    HostId = 2,
                    CourtId = 12,
                    Name = "Chơi cầu lông tối",
                    MaxPlayers = 4,
                    Description = "Team cầu lông nhẹ nhàng tối làm về.",
                    RoomFee = 25000,
                    Status = "Waiting",
                    ScheduledTime = DateTime.Today.AddDays(4).AddHours(19)
                }
            );


            builder.Entity<Voucher>().HasData(
                new Voucher
                {
                    Id = 1, 
                    Code = "VOUCHER1",
                    Description = "Giảm 10% cho booking đầu tiên",
                    DiscountPercent = 10
                },
                new Voucher
                {
                    Id = 2,
                    Code = "VOUCHER2",
                    Description = "Hỗ trợ website giảm 5%",
                    DiscountPercent = 5,
                }
            );
        }


    }
}
