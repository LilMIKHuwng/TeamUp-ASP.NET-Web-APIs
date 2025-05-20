using System;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class RoomPlayer : BaseEntity
    {
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int PlayerId { get; set; }
        public virtual ApplicationUser Player { get; set; }

        public string Status { get; set; }

        public bool IsNotified { get; set; } = false;

        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }
}
