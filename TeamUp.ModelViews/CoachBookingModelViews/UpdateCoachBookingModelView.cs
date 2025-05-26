using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.CoachBookingModelViews
{
    public class UpdateCoachBookingModelView
    {
        public int? CoachId { get; set; }
        public int? PlayerId { get; set; }
        public int? CourtId { get; set; }
        public List<CreateSlotModelView>? Slots { get; set; }
        public string? Status { get; set; }
        public string? PaymentMethod { get; set; }

        public int? VoucherId { get; set; }
    }
}
