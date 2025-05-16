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
        public int SportsComplexId { get; set; }
        public virtual SportsComplex SportsComplex { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal PricePerHour { get; set; }

        public List<string> ImageUrls { get; set; }

        public string Status { get; set; }

        public virtual ICollection<CourtBooking> CourtBookings { get; set; } 
        public virtual ICollection<CoachBooking> CoachBookings { get; set; } 
        public virtual ICollection<Room> Rooms { get; set; } 
    }

}
