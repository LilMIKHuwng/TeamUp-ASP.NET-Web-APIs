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
using TeamUp.ModelViews.RoomModelViews;
using TeamUp.ModelViews.RoomJoinRequestModelViews;
using TeamUp.ModelViews.CourtBookingModelViews;
using TeamUp.ModelViews.CoachBookingModelViews;
using TeamUp.ModelViews.RoomPlayerModelViews;
using TeamUp.ModelViews.RatingModelViews;
using TeamUp.ModelViews.PaymentModelViews;
using TeamUp.ModelViews.PackageModelViews;
using TeamUp.ModelViews.VoucherModelViews;


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

            //Room
            CreateMap<Room, RoomModelView>().ReverseMap();
            CreateMap<Room, CreateRoomModelView>().ReverseMap();
            CreateMap<Room, UpdateRoomModelView>().ReverseMap();

            //RoomJoinRequest
            CreateMap<RoomJoinRequest, RoomJoinRequestModelView>().ReverseMap();
            CreateMap<RoomJoinRequest, CreateRoomJoinRequestModelView>().ReverseMap();
            CreateMap<RoomJoinRequest, UpdateRoomJoinRequestModelView>().ReverseMap();

            //CourtBooking
            CreateMap<CourtBooking, CourtBookingModelView>().ReverseMap();
            CreateMap<CourtBooking, CreateCourtBookingModelView>().ReverseMap();
            CreateMap<CourtBooking, UpdateCourtBookingModelView>().ReverseMap();

            //CoachBooking
            CreateMap<CoachBooking, CoachBookingModelView>().ReverseMap();
            CreateMap<CoachBooking, CreateCoachBookingModelView>().ReverseMap();
            CreateMap<CoachBooking, UpdateCoachBookingModelView>().ReverseMap();

            //RoomPlayer
            CreateMap<RoomPlayer, RoomPlayerModelView>().ReverseMap();
            CreateMap<RoomPlayer, CreateRoomPlayerModelView>().ReverseMap();
            CreateMap<RoomPlayer, UpdateRoomPlayerModelView>().ReverseMap();

            //Rating
            CreateMap<Rating, RatingModelView>().ReverseMap();
            CreateMap<Rating, CreateRatingModelView>().ReverseMap();
            CreateMap<Rating, UpdateRatingModelView>().ReverseMap();

            //Payment
            CreateMap<Payment, PaymentModelView>().ReverseMap();
            CreateMap<Payment, CreatePaymentModelView>().ReverseMap();

            //Package
            CreateMap<Package, PackageModelView>().ReverseMap();
            CreateMap<Package, CreatePackageModelView>().ReverseMap();
            CreateMap<Package, UpdatePackageModelView>().ReverseMap();

            //Voucher
            CreateMap<Voucher, VoucherModelView>().ReverseMap();
            CreateMap<Voucher, CreateVoucherModelView>().ReverseMap();
            CreateMap<Voucher, UpdateVoucherModelView>().ReverseMap();

            //Slot
            CreateMap<Slot, SlotModelView>().ReverseMap();
            CreateMap<Slot, CreateSlotModelView>().ReverseMap();
        }
    }
}
