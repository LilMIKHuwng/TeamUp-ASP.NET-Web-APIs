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
    public class SportsComplex : BaseEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<string> ImageUrls { get; set; }

        public int OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public string Status { get; set; }

        public virtual ICollection<Court> Courts { get; set; }
    }
}
