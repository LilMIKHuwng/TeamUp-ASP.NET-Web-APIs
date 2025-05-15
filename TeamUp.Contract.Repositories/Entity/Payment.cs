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
    public class Payment : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int? CourtBookingId { get; set; }
        public virtual CourtBooking? CourtBooking { get; set; }

        public int? CoachBookingId { get; set; }
        public virtual CoachBooking? CoachBooking { get; set; }

        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }

        public string Method { get; set; }
        public string Status { get; set; }

    }

}
