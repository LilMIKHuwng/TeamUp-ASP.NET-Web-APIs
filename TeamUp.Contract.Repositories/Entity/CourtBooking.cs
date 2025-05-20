using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class CourtBooking : BaseEntity
    {
        public int CourtId { get; set; }
        public virtual Court Court { get; set; }

        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public bool IsNotified { get; set; } = false;

        public virtual ICollection<Payment> Payments { get; set; } 
    }

}
