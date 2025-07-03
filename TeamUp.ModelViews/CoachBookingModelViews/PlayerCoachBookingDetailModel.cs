using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.CoachBookingModelViews
{
    public class PlayerCoachBookingDetailModel
    {
        public int BookingId { get; set; }
        public string CourtName { get; set; }
        public string SportsComplexName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string? Note { get; set; }
        public string? DiscountInfo { get; set; }
    }
}
