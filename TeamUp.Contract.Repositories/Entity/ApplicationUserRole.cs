using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TeamUp.Core.Utils;
using TeamUp.Repositories.Entity;

namespace TeamUp.Contract.Repositories.Entity
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

}
