using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;

namespace TeamUp.Contract.Repositories.Entity
{
    public class Voucher : BaseEntity
    {
        public string Code { get; set; } // VD: VOUCHER1, VOUCHER2
        public string Description { get; set; }

        public int DiscountPercent { get; set; } // VD: 0.10m là 10%

        public virtual ICollection<CourtBooking> CourtBookings { get; set; }
        public virtual ICollection<CoachBooking> CoachBookings { get; set; }
    }
}
