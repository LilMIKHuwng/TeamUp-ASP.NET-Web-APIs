using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ModelViews.CoachBookingModelViews;
using TeamUp.ModelViews.CourtBookingModelViews;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.ModelViews.PaymentModelViews
{
    public class PaymentModelView
    {
        public string Id { get; set; }
        public UserResponseModel User { get; set; }
        public CourtBookingModelView? CourtBooking { get; set; }
        public CoachBookingModelView? CoachBooking { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
    }
}
