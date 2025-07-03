using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.CourtBookingModelViews
{
    public class TimeRangeFrequency
    {
        public string TimeRangeLabel { get; set; } // e.g. "Morning (6-11h)", "Evening (18-22h)"
        public int Count { get; set; }
    }
}
