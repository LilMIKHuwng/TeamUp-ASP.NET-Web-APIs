using AutoMapper;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.RoleModelViews;
using TeamUp.ModelViews.UserMessage;
using TeamUp.Repositories.Entity;


namespace TeamUp.Repositories.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Role
			CreateMap<ApplicationRole, RoleModelView>().ReverseMap();
			CreateMap<ApplicationRole, CreateRoleModelView>().ReverseMap();
			CreateMap<ApplicationRole, UpdatedRoleModelView>().ReverseMap();

            //Message
            CreateMap<UserMessage, ChatMessageModelView>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseModel>().ReverseMap();

        }
    }
}
