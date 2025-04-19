using Microsoft.AspNetCore.Http;

namespace TeamUp.ModelViews.UserModelViews.Request
{
    public class UploadImageRequest
    {
        public IFormFile Image { get; set; }
    }
}
