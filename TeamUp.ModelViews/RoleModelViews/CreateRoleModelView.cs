using System.ComponentModel.DataAnnotations;

namespace TeamUp.ModelViews.RoleModelViews
{
    public class CreateRoleModelView
    {
        [Required(ErrorMessage = "RoleName is required.")]
        public string Name { get; set; }

    }
}
