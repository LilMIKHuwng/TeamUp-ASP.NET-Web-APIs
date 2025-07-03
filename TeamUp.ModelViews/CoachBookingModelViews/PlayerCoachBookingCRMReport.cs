using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ModelViews.CourtBookingModelViews;

namespace TeamUp.ModelViews.CoachBookingModelViews
{
    public class PlayerCoachBookingCRMReport
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int TotalBookings { get; set; }
        public decimal TotalSpent { get; set; }
        public List<TimeRangeFrequency> BookingTimeDistribution { get; set; }
        public string CustomerCategory { get; set; }

        public List<PlayerCoachBookingDetailModel> BookingHistory { get; set; }
    }
}
