
namespace TeamUp.ModelViews.UserModelViews.Request
{
    public class UpdateUserStatusRequest
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
    }
}
