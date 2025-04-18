using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class UserMessage : BaseEntity
    {
        public int ChatId { get; set; }
        public virtual UserChat Chat { get; set; }

        public int SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }

        public string Message { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }

}
