using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.CourtBookingModelViews
{
    public class CreateCourtBookingModelView
    {
        [Required(ErrorMessage = "CourtId không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "CourtId phải lớn hơn 0")]
        public int CourtId { get; set; }

        [Required(ErrorMessage = "UserId không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId phải lớn hơn 0")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu không được để trống")]
        [FutureDate(ErrorMessage = "Thời gian bắt đầu phải lớn hơn hiện tại")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Thời gian kết thúc không được để trống")]
        [DateGreaterThan(nameof(StartTime), ErrorMessage = "Thời gian kết thúc phải sau thời gian bắt đầu")]
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Phương thức thanh toán không được để trống")]
        public string PaymentMethod { get; set; }

        public int? VoucherId { get; set; }
    }
}
