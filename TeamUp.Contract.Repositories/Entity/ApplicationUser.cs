using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Core.Base;
using TeamUp.Core.Utils;

namespace TeamUp.Repositories.Entity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; }
        public int? Age { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? AvatarUrl { get; set; }

        // Nếu là HLV
        public string? Specialty { get; set; }
        public string? Certificate { get; set; }
        public string? WorkingAddress { get; set; }
        public string? WorkingDate { get; set; }
        public decimal? PricePerSession { get; set; }

        public int? Status { get; set; }

        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset LastUpdatedTime { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? DeletedTime { get; set; }

        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }


        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } 
        public virtual ICollection<SportsComplex> SportsComplexs { get; set; } 
        public virtual ICollection<CourtBooking> CourtBookings { get; set; } 
        public virtual ICollection<CoachBooking> CoachBookingsAsCoach { get; set; } 
        public virtual ICollection<CoachBooking> CoachBookingsAsPlayer { get; set; } 
        public virtual ICollection<Room> HostedRooms { get; set; } 
        public virtual ICollection<RoomPlayer> RoomPlayers { get; set; } 
        public virtual ICollection<RoomJoinRequest> RoomJoinRequests { get; set; } 
        public virtual ICollection<Rating> RatingsGiven { get; set; } 
        public virtual ICollection<Rating> RatingsReceived { get; set; } 
        public virtual ICollection<Payment> Payments { get; set; } 
        public virtual ICollection<UserMessage> SentMessages { get; set; }
        public virtual ICollection<UserMessage> ReceivedMessages { get; set; }
    }

}
