using Microsoft.AspNetCore.Http;

namespace TeamUp.ModelViews.UserModelViews.Request
{
    public class UpdateUserProfileRequest
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public IFormFile? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
