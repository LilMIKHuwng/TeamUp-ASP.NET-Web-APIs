using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.CourtBookingModelViews
{
    public class UserCourtBookingCRMReport
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public int TotalBookings { get; set; }
        public decimal TotalSpent { get; set; }
        public List<TimeRangeFrequency> BookingTimeDistribution { get; set; }
        public string CustomerCategory { get; set; }

        public List<UserCourtBookingDetailModel> BookingHistory { get; set; }
    }
}
