using Microsoft.AspNetCore.Http;

namespace TeamUp.ModelViews.UserModelViews.Request
{
    public class UpdateEmployeeProfileRequest
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public IFormFile? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }


        public string? Type { get; set; }
        public string? Specialty { get; set; }
        public string? Certificate { get; set; }
        public string? WorkingAddress { get; set; }
        public string? WorkingDate { get; set; }
        public decimal? PricePerSession { get; set; }

    }
}
