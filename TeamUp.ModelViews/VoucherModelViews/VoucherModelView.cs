using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.VoucherModelViews
{
    public class VoucherModelView
    {
        public int Id { get ;set; }
        public string Code { get; set; } 
        public string Description { get; set; }

        public int DiscountPercent { get; set; } 
    }
}
