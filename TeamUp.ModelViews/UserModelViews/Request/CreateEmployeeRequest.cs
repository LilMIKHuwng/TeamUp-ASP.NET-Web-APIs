using Microsoft.AspNetCore.Http;

namespace TeamUp.ModelViews.UserModelViews.Request
{
    public class CreateEmployeeRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public IFormFile? Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
