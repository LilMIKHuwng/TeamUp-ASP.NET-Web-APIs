using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class UserMessage : BaseEntity
    {
        public int SenderId { get; set; } // người gửi
        public virtual ApplicationUser Sender { get; set; }

        public int RecipientId { get; set; } // người nhận
        public virtual ApplicationUser Recipient { get; set; }

        public DateTime SendAt { get; set; }
        public string MessageContent { get; set; }
        public string ChannelName { get; set; }
    }

}
