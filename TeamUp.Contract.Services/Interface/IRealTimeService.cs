using TeamUp.ModelViews.UserMessage;
using TeamUp.ModelViews.UserModelViews.Response;

namespace TeamUp.Contract.Services.Interface
{
    public interface IRealTimeService
    {
        Task SendMessage(string channel, string message, int senderId, int receiverId);
        Task<List<ChatMessageModelView>> GetMessageHistory(int senderId, int receiverId);
        Task<List<UserResponseModel>> GetChatPartnersAsync(int senderId);
    }
}

