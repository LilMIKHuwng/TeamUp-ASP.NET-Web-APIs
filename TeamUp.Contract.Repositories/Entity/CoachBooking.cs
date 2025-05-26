using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class CoachBooking : BaseEntity
    {
        public int CoachId { get; set; }
        public virtual ApplicationUser Coach { get; set; }

        public int PlayerId { get; set; }
        public virtual ApplicationUser Player { get; set; }

        public int CourtId { get; set; }
        public virtual Court Court { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public bool IsNotified { get; set; } = false;

        public int? VoucherId { get; set; }
        public virtual Voucher? Voucher { get; set; }

        public decimal DiscountAmount { get; set; } = 0;

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Slot> Slots { get; set; }
    }

}
