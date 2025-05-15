using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Repositories.Entity;

namespace TeamUp.ModelViews.RoomPlayerModelViews
{
    public class UpdateRoomPlayerModelView
    {
        public int? RoomId { get; set; }

        public int? PlayerId { get; set; }

        public string? Status { get; set; }

    }
}
