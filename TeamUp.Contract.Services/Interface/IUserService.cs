using TeamUp.ModelViews.UserModelViews;

namespace TeamUp.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<IList<UserResponseModel>> GetAll();
    }
}
