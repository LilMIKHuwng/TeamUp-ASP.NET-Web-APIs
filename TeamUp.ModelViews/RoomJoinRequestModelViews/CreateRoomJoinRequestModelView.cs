using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.RoomJoinRequestModelViews
{
    public class CreateRoomJoinRequestModelView
    {
        [Required(ErrorMessage = "Mã phòng không được để trống")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Người gửi yêu cầu không được để trống")]
        public int RequesterId { get; set; }
    }
}
