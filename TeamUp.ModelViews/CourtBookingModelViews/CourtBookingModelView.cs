using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.PackageModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.ModelViews.VoucherModelViews;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.CourtBookingModelViews
{
    public class CourtBookingModelView
    {
        public int Id { get; set; }
        public CourtModelView Court { get; set; }
        public UserResponseModel User { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public decimal DiscountAmount { get; set; }

        public VoucherModelView? Voucher { get; set; }
    }
}
