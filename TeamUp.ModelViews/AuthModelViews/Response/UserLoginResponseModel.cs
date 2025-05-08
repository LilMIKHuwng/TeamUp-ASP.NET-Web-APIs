
namespace TeamUp.ModelViews.AuthModelViews.Response
{
    public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset AccessTokenExpiredTime { get; set; }
    }
}
