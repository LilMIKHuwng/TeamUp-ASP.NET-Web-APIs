using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class Court : BaseEntity
    {
        public int OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [MaxLength(300)]
        public string? ImageUrl { get; set; }

        public virtual ICollection<CourtBooking> CourtBookings { get; set; } 
        public virtual ICollection<CoachBooking> CoachBookings { get; set; } 
        public virtual ICollection<Room> Rooms { get; set; } 
    }

}
