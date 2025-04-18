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


        public virtual DbSet<Court> Courts { get; set; }
        public virtual DbSet<CourtBooking> CourtBookings { get; set; }
        public virtual DbSet<CoachBooking> CoachBookings { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomPlayer> RoomPlayers { get; set; }
        public virtual DbSet<RoomJoinRequest> RoomJoinRequests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<UserChat> UserChats { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }

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

            builder.Entity<UserChat>(builder =>
            {
                builder.HasOne(x => x.User1)
                    .WithMany(x => x.UserChats1)
                    .HasForeignKey(x => x.User1Id)
                    .OnDelete(DeleteBehavior.Restrict); // Tránh vòng lặp

                builder.HasOne(x => x.User2)
                    .WithMany(x => x.UserChats2)
                    .HasForeignKey(x => x.User2Id)
                    .OnDelete(DeleteBehavior.Restrict); // Tránh vòng lặp
            });

            // Seed roles
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Quản trị viên" },
                new ApplicationRole { Id = 2, Name = "Người Chơi", NormalizedName = "NGUOICHOI", Description = "Người dùng thông thường" },
                new ApplicationRole { Id = 3, Name = "Chủ Sân", NormalizedName = "CHUSAN", Description = "Chủ sân thể thao" },
                new ApplicationRole { Id = 4, Name = "Huấn Luyện Viên", NormalizedName = "HUANLUYENVIEN", Description = "Coach / Trainer" }
            );
        }

    }
}
