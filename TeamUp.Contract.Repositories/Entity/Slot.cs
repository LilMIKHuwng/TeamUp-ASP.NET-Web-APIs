using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;

namespace TeamUp.Contract.Repositories.Entity
{
    public class Slot : BaseEntity
    {
        public int CoachBookingId { get; set; }
        public virtual CoachBooking CoachBooking { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
