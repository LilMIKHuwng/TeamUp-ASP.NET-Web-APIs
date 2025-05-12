using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.ModelViews.RoomJoinRequestModelViews
{
    public class RoomJoinRequestModelView
    {
        public int Id { get; set; }
        public RoomModelView Room { get; set; }
        public UserResponseModel Requester { get; set; }
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime RespondedAt { get; set; }
    }
}
