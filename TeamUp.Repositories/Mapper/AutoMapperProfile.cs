using AutoMapper;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.RoleModelViews;
using TeamUp.ModelViews.UserMessage;
using TeamUp.Repositories.Entity;
using TeamUp.ModelViews.AuthModelViews.Response;
using TeamUp.ModelViews.UserModelViews.Request;
using TeamUp.ModelViews.SportsComplexModelViews;
using TeamUp.ModelViews.CourtModelViews;


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

            //User
            CreateMap<ApplicationUser, UserLoginResponseModel>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseModel>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserProfileRequest>().ReverseMap();

            //Employee
            CreateMap<EmployeeLoginResponseModel, ApplicationUser>().ReverseMap();
            CreateMap<CreateEmployeeRequest, ApplicationUser>().ReverseMap();
            CreateMap<UpdateEmployeeProfileRequest, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, EmployeeResponseModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role))
                .ReverseMap()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());

            //SportsComplex
            CreateMap<SportsComplex, SportsComplexModelView>().ReverseMap();
            CreateMap<SportsComplex, CreateSportsComplexModelView>().ReverseMap();
            CreateMap<SportsComplex, UpdateSportsComplexModelView>().ReverseMap();

            //Court
            CreateMap<Court, CourtModelView>().ReverseMap();
            CreateMap<Court, CreateCourtModelView>().ReverseMap();
            CreateMap<Court, UpdateCourtModelView>().ReverseMap();
        }
    }
}
