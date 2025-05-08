
namespace TeamUp.ModelViews.AuthModelViews.Response
{
    public class EmployeeLoginResponseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Specialty { get; set; }
        public string? Certificate { get; set; }
        public string? WorkingAddress { get; set; }
        public string? WorkingDate { get; set; }
        public decimal? PricePerSession { get; set; }

        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset AccessTokenExpiredTime { get; set; }
        public List<string> Roles { get; set; }
    }
}
