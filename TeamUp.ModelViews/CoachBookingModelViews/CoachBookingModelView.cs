using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.ModelViews.VoucherModelViews;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.CoachBookingModelViews
{
    public class CoachBookingModelView
    {
        public int Id { get; set; }
        public EmployeeResponseModel Coach { get; set; }
        public UserResponseModel Player { get; set; }
        public CourtModelView Court { get; set; }
        public List<SlotModelView> Slots { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public decimal DiscountAmount { get; set; }

        public VoucherModelView? Voucher { get; set; }

    }
}
