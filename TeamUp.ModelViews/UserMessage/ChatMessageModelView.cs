using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.ModelViews.UserMessage
{
    public class ChatMessageModelView
    {
        public int Id { get; set; }
        public UserResponseModel SenderId { get; set; }
        public UserResponseModel ReceiverId { get; set; }
        public string Message { get; set; }
        public DateTime SendAt { get; set; }
    }
}
