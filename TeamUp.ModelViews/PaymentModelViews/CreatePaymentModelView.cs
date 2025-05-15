using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.PaymentModelViews
{
    public class CreatePaymentModelView
    {
        public int UserId { get; set; }
        public int? CourtBookingId { get; set; }
        public int? CoachBookingId { get; set; }
    }
}
