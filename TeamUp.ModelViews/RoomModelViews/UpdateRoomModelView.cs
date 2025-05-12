using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.RoomModelViews
{
    public class UpdateRoomModelView
    {
        public int? HostId { get; set; }
        public int? CourtId { get; set; }
        public string? Name { get; set; }
        public int? MaxPlayers { get; set; }
        public string? Description { get; set; }
        public decimal? RoomFee { get; set; }
        public DateTime? ScheduledTime { get; set; }
        public string? Status { get; set; }
    }
}
