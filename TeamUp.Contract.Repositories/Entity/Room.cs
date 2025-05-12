using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class Room : BaseEntity
    {
        public int HostId { get; set; }
        public virtual ApplicationUser Host { get; set; }

        public int CourtId { get; set; }
        public virtual Court Court { get; set; }

        public string Name { get; set; }

        public int MaxPlayers { get; set; }

        public string Description { get; set; }

        public decimal RoomFee { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledTime { get; set; }

        public virtual ICollection<RoomPlayer> RoomPlayers { get; set; } 
        public virtual ICollection<RoomJoinRequest> JoinRequests { get; set; }
    }

}
