using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.RoomPlayerModelViews
{
    public class RoomPlayerModelView
    {
        public int Id { get; set; }
        public RoomModelView Room { get; set; }

        public UserResponseModel Player { get; set; }

        public string Status { get; set; }

        public DateTime JoinedAt { get; set; }
    }
}
