using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.RoomModelViews
{
    public class CreateRoomModelView
    {
        [Required]
        public int HostId { get; set; }

        [Required]
        public int CourtId { get; set; }

        [Required(ErrorMessage = "Tên phòng không được để trống")]
        public string Name { get; set; }

        [Range(1, 20, ErrorMessage = "Số người tối đa phải từ 1 đến 20")]
        public int MaxPlayers { get; set; }
        public string? Description { get; set; }

        [Range(0, 1000000, ErrorMessage = "Phí phòng phải lớn hơn hoặc bằng 0")]
        public decimal RoomFee { get; set; }

        [Required]
        [FutureDate(ErrorMessage = "Thời gian phải lớn hơn hiện tại")]
        public DateTime ScheduledTime { get; set; }
    }
}
