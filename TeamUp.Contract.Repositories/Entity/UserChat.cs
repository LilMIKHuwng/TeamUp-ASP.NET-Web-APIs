using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.Base;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class UserChat : BaseEntity
    {
        public int User1Id { get; set; }
        public virtual ApplicationUser User1 { get; set; }

        public int User2Id { get; set; }
        public virtual ApplicationUser User2 { get; set; }

        public virtual ICollection<UserMessage> Messages { get; set; } 
    }

}
