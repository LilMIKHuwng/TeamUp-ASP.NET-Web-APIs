using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Core.APIResponse;
using TeamUp.Core;
using TeamUp.ModelViews.UserModelViews.Request;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.ModelViews.AuthModelViews.Response;
using TeamUp.ModelViews.AuthModelViews.Request;
using TeamUp.Core.Base;

namespace TeamUp.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResult<EmployeeLoginResponseModel>> RefreshToken(NewRefreshTokenRequestModel request);
        Task<ApiResult<DashboardUserCreateResponse>> GetUsersByCreateTime();


        #region Authen User
        Task<ApiResult<UserLoginResponseModel>> UserLogin(UserLoginRequestModel request);
        Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleByRole(UserLoginGoogleRequest request, string roleId);
        Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleForUser(UserLoginGoogleRequest request);
        Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleForOwner(UserLoginGoogleRequest request);
        Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleForCoach(UserLoginGoogleRequest request);

        Task<ApiResult<object>> RegisterWithRole(UserRegisterRequestModel request, string roleId);
        Task<ApiResult<object>> RegisterUser(UserRegisterRequestModel request);
        Task<ApiResult<object>> RegisterOwner(UserRegisterRequestModel request);
        Task<ApiResult<object>> RegisterCoach(UserRegisterRequestModel request);
        Task<ApiResult<UserLoginResponseModel>> ConfirmUserRegister(ConfirmUserRegisterRequest request);

        Task<ApiResult<object>> ForgotPassword(ForgotPasswordRequest request);
        Task<ApiResult<object>> ResetPassword(ResetPasswordRequestModel request);
        #endregion

        #region Authen Admin/Owner/Coach
        Task<ApiResult<EmployeeLoginResponseModel>> EmployeeLogin(EmployeeLoginRequestModel request);
        Task<ApiResult<object>> EmployeeForgotPassword(ForgotPasswordRequest request);
        Task<ApiResult<object>> EmployeeResetPassword(ResetPasswordRequestModel request);
        #endregion

        #region User

        Task<ApiResult<object>> UpdateUserAndOwnerProfile(UpdateUserProfileRequest request);
        // Get user pagination 
        Task<ApiResult<BasePaginatedList<UserResponseModel>>> GetUserPagination(BaseSearchRequest request);
        Task<ApiResult<UserResponseModel>> GetUserById(int Id);
        Task<ApiResult<object>> DeleteUser(DeleteUserRequest request);
        Task<ApiResult<object>> UpdateUserStatus(UpdateUserStatusRequest request);
        ApiResult<List<UserStatusResponseModel>> GetUserStatus();



        #endregion

        #region Admin/Owner/Coach
        ApiResult<List<UserStatusResponseModel>> GetEmployeeStatus();

        Task<ApiResult<object>> CreateEmployee(CreateEmployeeRequest request);
        Task<ApiResult<object>> UpdateCoachProfile(UpdateEmployeeProfileRequest request);
        Task<ApiResult<object>> UpdateEmployeeStatus(UpdateUserStatusRequest request);
        Task<ApiResult<object>> DeleteEmployee(DeleteUserRequest request);
        Task<ApiResult<BasePaginatedList<EmployeeResponseModel>>> GetOwnerPagination(BaseSearchRequest request);
        Task<ApiResult<List<EmployeeResponseModel>>> GetAllOwner();
        Task<ApiResult<BasePaginatedList<EmployeeResponseModel>>> GetCoachPagination(BaseSearchRequest request);
        Task<ApiResult<List<EmployeeResponseModel>>> GetAllCoach();
        Task<ApiResult<List<EmployeeResponseModel>>> GetAllEmployee();

        Task<ApiResult<List<UserResponseModel>>> GetAllUser();

        Task<ApiResult<EmployeeResponseModel>> GetEmployeeById(int Id);
        Task<ApiResult<UploadImageResponseModel>> UploadImage(UploadImageRequest request);



        #endregion
    }
}
