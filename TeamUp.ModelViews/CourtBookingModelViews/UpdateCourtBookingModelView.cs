using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.CourtBookingModelViews
{
    public class UpdateCourtBookingModelView
    {
        public int? CourtId { get; set; }
        public int? UserId { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
