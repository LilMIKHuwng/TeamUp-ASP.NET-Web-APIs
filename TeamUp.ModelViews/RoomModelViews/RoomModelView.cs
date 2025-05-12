using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.ModelViews.RoomModelViews
{
    public class RoomModelView
    {
        public int Id { get; set; }
        public UserResponseModel Host { get; set; }
        public CourtModelView Court { get; set; }
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
        public string Description { get; set; }
        public decimal RoomFee { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string Status { get; set; }
    }
}
