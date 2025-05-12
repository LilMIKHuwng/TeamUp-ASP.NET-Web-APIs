using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class RoomJoinRequest : BaseEntity
    {
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int RequesterId { get; set; }
        public virtual ApplicationUser Requester { get; set; }

        public string Status { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.Now;
        public DateTime? RespondedAt { get; set; }
    }

}
