using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.VoucherModelViews
{
    public class CreateVoucherModelView
    {
        [Required(ErrorMessage = "Mã voucher là bắt buộc.")]
        public string Code { get; set; }

        public string? Description { get; set; }

        [Range(1, 100, ErrorMessage = "Phần trăm giảm giá phải từ 1 đến 100.")]
        public int DiscountPercent { get; set; }
    }
}
