using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.RoomJoinRequestModelViews
{
    public class UpdateRoomJoinRequestModelView
    {
        public int? RoomId { get; set; }
        public int? RequesterId { get; set; }
        public string? Status { get; set; }
    }
}
