using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.CourtBookingModelViews;
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.CoachBookingModelViews
{
    public class CreateCoachBookingModelView
    {
        [Required(ErrorMessage = "CoachId không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "CoachId phải lớn hơn 0")]
        public int CoachId { get; set; }

        [Required(ErrorMessage = "PlayerId không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "PlayerId phải lớn hơn 0")]
        public int PlayerId { get; set; }

        [Required(ErrorMessage = "CourtId không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "CourtId phải lớn hơn 0")]
        public int CourtId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ít nhất một ngày huấn luyện")]
        [MinLength(1, ErrorMessage = "Cần chọn ít nhất một ngày huấn luyện")]
        public List<DateTime> SelectedDates { get; set; }

        [Required(ErrorMessage = "Thời gian bắt đầu không được để trống")]
        public TimeSpan StartTime { get; set; } 

        [Required(ErrorMessage = "Thời gian kết thúc không được để trống")]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "Phương thức thanh toán không được để trống")]
        public string PaymentMethod { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult("Thời gian kết thúc phải lớn hơn thời gian bắt đầu", new[] { nameof(EndTime) });
            }
        }
    }

}
