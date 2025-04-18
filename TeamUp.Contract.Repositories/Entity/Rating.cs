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
    public class Rating : BaseEntity
    {
        public int ReviewerId { get; set; }
        public virtual ApplicationUser Reviewer { get; set; }

        public int RevieweeId { get; set; }
        public virtual ApplicationUser Reviewee { get; set; }

        [Range(1, 5)]
        public int RatingValue { get; set; }

        public string? Comment { get; set; }
    }

}
