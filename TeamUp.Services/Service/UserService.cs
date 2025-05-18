using AutoMapper;
using Azure;
using BabyCare.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Repositories.Interface;
using TeamUp.Contract.Services.Interface;
using TeamUp.Core;
using TeamUp.Core.APIResponse;
using TeamUp.Core.Base;
using TeamUp.Core.Utils;
using TeamUp.Core.Utils.Firebase;
using TeamUp.ModelViews.AuthModelViews.Request;
using TeamUp.ModelViews.AuthModelViews.Response;
using TeamUp.ModelViews.CourtModelViews;
using TeamUp.ModelViews.PackageModelViews;
using TeamUp.ModelViews.RoleModelViews;
using TeamUp.ModelViews.UserModelViews.Request;
using TeamUp.ModelViews.UserModelViews.Response;
using TeamUp.Repositories.Entity;
using static BabyCare.Core.Utils.SystemConstant;

namespace TeamUp.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IConfiguration configuration, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<ApplicationRole> roleManager)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResult<UserLoginResponseModel>> ConfirmUserRegister(ConfirmUserRegisterRequest request)
        {
            // Check existed email
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            // Confirm code 
            var result = await _userManager.ConfirmEmailAsync(existingUser, request.Code);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Confirm email unsuccessfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            existingUser.Status = (int)UserStatus.Active;
            var rs = await _userManager.UpdateAsync(existingUser);

            if (!rs.Succeeded)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Update unsuccessfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;

            await _userManager.UpdateAsync(existingUser);
            var response = _mapper.Map<UserLoginResponseModel>(existingUser);
            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.RefreshToken = refreshTokenData.Item1;
            response.RefreshTokenExpiryTime = refreshTokenData.Item2;
            response.FullName = existingUser.FullName ?? "Unknown";

            return new ApiSuccessResult<UserLoginResponseModel>(response, "Register successfully.");
        }

        private (string, DateTime) GenerateRefreshToken()
        {
            var expiredTime = DateTime.Now.AddMinutes(TeamUp.Core.Utils.TimeHelper.DURATION_REFRESH_TOKEN_TIME);
            var refreshToken = "";
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                refreshToken = Convert.ToBase64String(random);
            }
            return (refreshToken, expiredTime);
        }
        private async Task<(string, DateTime)> GenerateAccessTokenAsync(ApplicationUser user)
        {
            var expiredTime = DateTime.Now.AddMinutes(TeamUp.Core.Utils.TimeHelper.DURATION_ACCESS_TOKEN_TIME);
            var authClaims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim("Role", role.ToString()));
            }

            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: expiredTime,
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return (accessToken, expiredTime);
        }

        public async Task<ApiResult<object>> CreateEmployee(CreateEmployeeRequest request)
        {
            // Check existed username
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (existingUser != null)
            {
                return new ApiErrorResult<object>("Username is existed.", System.Net.HttpStatusCode.Conflict);
            }
            // Check existed email
            var existingEmail = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingEmail != null)
            {
                return new ApiErrorResult<object>("Email is existed.", System.Net.HttpStatusCode.Conflict);
            }
            if (_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value == null)
            {
                return new ApiErrorResult<object>("Plase login to use this function.", System.Net.HttpStatusCode.BadRequest);
            }

            // Createe user use mapper
            var user = _mapper.Map<ApplicationUser>(request);
            user.Status = (int)EmployeeStatus.Active;

            if (request.Image != null)
            {
                user.AvatarUrl = await TeamUp.Core.Utils.Firebase.ImageHelper.Upload(request.Image);
            }
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return new ApiErrorResult<object>("Create user unsuccessfully.", result.Errors.Select(x => x.Description).ToList(), HttpStatusCode.BadRequest);
            // Find role
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);
            if (role == null)
                return new ApiErrorResult<object>("Default role not found.", HttpStatusCode.NotFound);

            await _userManager.AddToRoleAsync(user, role.Name);

            return new ApiSuccessResult<object>("Create user successfully.");
        }

        public async Task<ApiResult<object>> DeleteEmployee(DeleteUserRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value == null)
            {
                return new ApiErrorResult<object>("Plase login to use this function.", System.Net.HttpStatusCode.BadRequest);
            }
            // Delete user
            existingUser.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);
            existingUser.DeletedTime = DateTime.Now;
            var result = await _userManager.UpdateAsync(existingUser);
            // Return to client
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Delete user unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Delete user successfully.");
        }

        public async Task<ApiResult<object>> DeleteUser(DeleteUserRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value == null)
            {
                return new ApiErrorResult<object>("Plase login to use this function.", System.Net.HttpStatusCode.BadRequest);
            }
            // Delete user
            existingUser.DeletedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);
            existingUser.DeletedTime = DateTime.Now;
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Delete usere unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Delete user successfully.");
        }

        public async Task<ApiResult<object>> EmployeeForgotPassword(ForgotPasswordRequest request)
        {
            var email = request.Email;
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            var encodedToken = Uri.EscapeDataString(token);

            // Correct relative path from current directory to the HTML file
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "SendCode.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");
            }

            var frontEndUrl = _configuration["URL:FrontEnd"];
            var fullForgotPasswordUrl = frontEndUrl + "auth/employee-reset-password?email=" + email + "&token=" + encodedToken;
            string contentCustomer = System.IO.File.ReadAllText(path);
            contentCustomer = contentCustomer.Replace("{{VerifyCode}}", fullForgotPasswordUrl);
            var sendMailResult = DoingMail.SendMail("TeamUp", "Yêu cầu thay đổi mật khẩu", contentCustomer, email);
            if (!sendMailResult)
            {
                return new ApiErrorResult<object>("Lỗi hệ thống. Vui lòng thử lại sau", System.Net.HttpStatusCode.NotFound);
            }
            return new ApiSuccessResult<object>("Please check your mail to reset password.");
        }

        public async Task<ApiResult<EmployeeLoginResponseModel>> EmployeeLogin(EmployeeLoginRequestModel request)
        {
            // Check valid username
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            if (existingUser.DeletedBy != null)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            // Check valid password
            var validPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!validPassword)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            // Check valid role doctor or admin
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (!userRoles.Contains("Owner") && !userRoles.Contains("Admin") && !userRoles.Contains("Coach"))
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Username or password is not correct.", System.Net.HttpStatusCode.NotFound);
            }
            if (existingUser.Status == ((int)SystemConstant.EmployeeStatus.InActive))
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("You cannot access system.", System.Net.HttpStatusCode.NotFound);

            }

            // Generate refresh token
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;
            await _userManager.UpdateAsync(existingUser);
            // Response to client
            var response = _mapper.Map<EmployeeLoginResponseModel>(existingUser);
            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.FullName = existingUser.FullName ?? "Unknown";
            // Take role
            var roles = await _userManager.GetRolesAsync(existingUser);
            response.Roles = roles.ToList();
            return new ApiSuccessResult<EmployeeLoginResponseModel>(response, "Login successfully.");
        }

        public async Task<ApiResult<object>> EmployeeResetPassword(ResetPasswordRequestModel request)
        {
            // Check existed email
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            // Check valid role Doctor or Admin
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (!userRoles.Contains("Owner") && !userRoles.Contains("Admin") && !userRoles.Contains("Coach"))
            {
                return new ApiErrorResult<object>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            // Valid token
            var result = await _userManager.ResetPasswordAsync(existingUser, request.Token, request.Password);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Reset password unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Reset password successfully.");
        }

        public async Task<ApiResult<object>> ForgotPassword(ForgotPasswordRequest request)
        {
            // Check existed email
            var email = request.Email;
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(existingUser);
            if (!isConfirmed)
            {
                return new ApiErrorResult<object>("Email is not confirm.", System.Net.HttpStatusCode.NotFound);

            }
            // Generate token
            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            // Send email
            // Correct relative path from current directory to the HTML file
            var encodedToken = Uri.EscapeDataString(token);


            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "SendCodeCustomer.html");
            path = Path.GetFullPath(path);


            if (!System.IO.File.Exists(path))
            {
                return new ApiErrorResult<object>("System error, try later", System.Net.HttpStatusCode.NotFound);
            }
            var frontEndUrl = _configuration["URL:FrontEnd"];
            var fullForgotPasswordUrl = frontEndUrl + "auth/reset-password?email=" + email + "&token=" + encodedToken;
            string contentCustomer = System.IO.File.ReadAllText(path);
            contentCustomer = contentCustomer.Replace("{{VerifyCode}}", fullForgotPasswordUrl);
            var sendMailResult = DoingMail.SendMail("TeamUp", "Yêu cầu thay đổi mật khẩu", contentCustomer, email);
            if (!sendMailResult)
            {
                return new ApiErrorResult<object>("Lỗi hệ thống. Vui lòng thử lại sau", System.Net.HttpStatusCode.NotFound);
            }
            return new ApiSuccessResult<object>("Please check your mail to reset password.");
        }


        public async Task<ApiResult<List<EmployeeResponseModel>>> GetAllEmployee()
        {
            // Lấy danh sách Role có tên DOCTOR hoặc ADMIN
            var AdminRoles = await _roleManager.Roles
                .Where(r => r.Name == SystemConstant.Role.OWNER || r.Name == SystemConstant.Role.ADMIN || r.Name == SystemConstant.Role.COACH)
                .ToListAsync();

            if (AdminRoles == null || !AdminRoles.Any())
            {
                return new ApiSuccessResult<List<EmployeeResponseModel>>(new List<EmployeeResponseModel>());
            }

            // Lấy danh sách RoleId tương ứng
            var roleIds = AdminRoles.Select(r => r.Id).ToList();

            // Lấy danh sách UserId của những người có RoleId nằm trong danh sách roleIds
            var doctorAdminUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => roleIds.Contains(ur.RoleId))
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = await _userManager.Users
                .Where(u => doctorAdminUserIds.Contains(u.Id) && u.DeletedBy == null)
                .OrderByDescending(ur => ur.LastUpdatedTime)
                .ToListAsync();

            // Trả về danh sách nhân viên
            var items = users.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Height = user.Height,
                Weight = user.Weight,
                Age = user.Age,
                Status = ((EmployeeStatus)user.Status).ToString(),
                Specialty = user.Specialty,
                Certificate = user.Certificate,
                WorkingAddress = user.WorkingAddress,
                WorkingDate = user.WorkingDate,
                PricePerSession = user.PricePerSession,
                StatusForCoach = user.StatusForCoach,
                StartDate = user.StartDate,
                ExpireDate = user.ExpireDate,

                Package = _mapper.Map<PackageModelView>(user.Package),

                // Lấy role phù hợp của user
                Role = new ModelViews.RoleModelViews.RoleModelView()
                {
                    Id = (int)AdminRoles.FirstOrDefault(r => r.Id ==
                         _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                         .Where(ur => ur.UserId == user.Id)
                         .Select(ur => ur.RoleId)
                         .FirstOrDefault()
                    )?.Id,
                    Name = AdminRoles.FirstOrDefault(r => r.Id ==
                         _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                         .Where(ur => ur.UserId == user.Id)
                         .Select(ur => ur.RoleId)
                         .FirstOrDefault()
                    )?.Name
                }
            }).ToList();

            return new ApiSuccessResult<List<EmployeeResponseModel>>(items);
        }

        public async Task<ApiResult<List<UserResponseModel>>> GetAllUser()
        {
            var userRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == SystemConstant.Role.USER);


            // Filter users

            var UserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == userRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = await _userManager.Users
                .Where(u => UserIds.Contains(u.Id) && u.DeletedBy == null)
                .OrderByDescending(x => x.LastUpdatedTime)
                .ToListAsync();



            var items = users.Select(x => new UserResponseModel
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                Age = x.Age,
                Height = x.Height,
                Weight = x.Weight,
                AvatarUrl = x.AvatarUrl,
                PhoneNumber = x.PhoneNumber,
                Status = ((EmployeeStatus)x.Status).ToString(),

            }).ToList();

            // return to client
            return new ApiSuccessResult<List<UserResponseModel>>(items);
        }


        public async Task<ApiResult<EmployeeResponseModel>> GetEmployeeById(int Id)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id );
            if (existingUser == null)
            {
                return new ApiErrorResult<EmployeeResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (existingUser.DeletedBy != null)
            {
                return new ApiErrorResult<EmployeeResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);

            }
            // Check role Admin or Doctor
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            if (!userRoles.Contains("Owner") && !userRoles.Contains("Admin") && !userRoles.Contains("Coach"))
            {
                return new ApiErrorResult<EmployeeResponseModel>("User is not valid.", System.Net.HttpStatusCode.NotFound);
            }

            if (userRoles.Contains("Coach"))
            {
                if (existingUser.StatusForCoach == PackageStatus.InActive)
                {
                    return new ApiErrorResult<EmployeeResponseModel>("User is not valid.", System.Net.HttpStatusCode.NotFound);
                }
            }

            // Response to client
            var response = _mapper.Map<EmployeeResponseModel>(existingUser);
            if (Enum.IsDefined(typeof(EmployeeStatus), existingUser.Status))
            {
                response.Status = ((EmployeeStatus)existingUser.Status).ToString();
            }
            else
            {
                response.Status = "Unknown";
            }

            response.Package= _mapper.Map<PackageModelView>(existingUser.Package);

            return new ApiSuccessResult<EmployeeResponseModel>(response);
        }

        public ApiResult<List<UserStatusResponseModel>> GetEmployeeStatus()
        {
            var statusList = Enum.GetValues(typeof(EmployeeStatus))
                      .Cast<UserStatus>()
                      .Select(status => new UserStatusResponseModel
                      {
                          Id = (int)status,
                          Status = status.ToString()
                      })
                      .ToList();


            return new ApiSuccessResult<List<UserStatusResponseModel>>(statusList);
        }

        public async Task<ApiResult<UserResponseModel>> GetUserById(int Id)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<UserResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            // Check isUser
            var isValidUser = await _userManager.GetRolesAsync(existingUser);
            foreach (var item in isValidUser)
            {
                if (item != SystemConstant.Role.USER)
                {
                    return new ApiErrorResult<UserResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
                }
            }
            // Response to client
            var response = _mapper.Map<UserResponseModel>(existingUser);
            if (Enum.IsDefined(typeof(UserStatus), existingUser.Status))
            {
                response.Status = ((UserStatus)existingUser.Status).ToString();
            }
            else
            {
                response.Status = "Unknown";
            }


            return new ApiSuccessResult<UserResponseModel>(response);
        }

        public async Task<ApiResult<BasePaginatedList<UserResponseModel>>> GetUserPagination(BaseSearchRequest request)
        {
            // all user


            var userRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == SystemConstant.Role.USER);


            // Filter users

            var doctorUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
        .Where(ur => ur.RoleId == userRole.Id)
        .Select(ur => ur.UserId)
        .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = _userManager.Users
                .OrderByDescending(r => r.LastUpdatedTime)
                .Where(u => doctorUserIds.Contains(u.Id) && u.DeletedBy == null);
            // filter by search 
            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                users = users.Where(x => x.FullName.ToLower().Contains(request.SearchValue.ToLower()) || x.Email.ToLower().Contains(request.SearchValue.ToLower()));
            }
            // paging
            var currentPage = request.PageIndex ?? 1;
            var pageSize = request.PageSize ?? SystemConstant.PAGE_SIZE;
            var total = users.Count();
            var data = await users.Skip((currentPage - 1) * currentPage).Take(pageSize).ToListAsync();
            // calculate total page

            var items = data.Select(x => new UserResponseModel
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                Age = x.Age,
                Height = x.Height,
                Weight = x.Weight,
                AvatarUrl = x.AvatarUrl,
                PhoneNumber = x.PhoneNumber,
                Status = ((EmployeeStatus)x.Status).ToString(),

            }).ToList();

            var response = new BasePaginatedList<UserResponseModel>(items, total, currentPage, pageSize);
            // return to client
            return new ApiSuccessResult<BasePaginatedList<UserResponseModel>>(response);
        }

        public async Task<ApiResult<DashboardUserCreateResponse>> GetUsersByCreateTime()
        {
            var now = DateTime.Now;

            var users = await _userManager.Users.ToListAsync(); // Lấy tất cả users để xử lý
            var usersWithRoleUser = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(SystemConstant.Role.USER)) // Kiểm tra user có role "User"
                {
                    usersWithRoleUser.Add(user);
                }
            }

            var usersY = usersWithRoleUser.Where(u => u.CreatedTime.Year == now.Year).ToList();
            var usersM = usersWithRoleUser.Where(u => u.CreatedTime.Month == now.Month).ToList();
            var usersD = usersWithRoleUser.Where(u => u.CreatedTime.Date == now.Date).ToList();

            var res = new DashboardUserCreateResponse()
            {
                InDay = usersD.Count,
                InMonth = usersM.Count,
                InYear = usersY.Count,
            };

            return new ApiSuccessResult<DashboardUserCreateResponse>(res);
        }

        public ApiResult<List<UserStatusResponseModel>> GetUserStatus()
        {
            var statusList = Enum.GetValues(typeof(UserStatus))
                        .Cast<UserStatus>()
                        .Select(status => new UserStatusResponseModel
                        {
                            Id = (int)status,
                            Status = status.ToString()
                        })
                        .ToList();


            return new ApiSuccessResult<List<UserStatusResponseModel>>(statusList);
        }

        public async Task<ApiResult<EmployeeLoginResponseModel>> RefreshToken(NewRefreshTokenRequestModel request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            // Check refresh token
            if (existingUser.RefreshToken != request.RefreshToken)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Refresh token is not correct.", System.Net.HttpStatusCode.BadRequest);
            }
            // Check expired time
            if (existingUser.RefreshTokenExpiryTime < DateTime.Now)
            {
                return new ApiErrorResult<EmployeeLoginResponseModel>("Refresh token is expired.", System.Net.HttpStatusCode.BadRequest);
            }
            // Generate new refresh token
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;
            await _userManager.UpdateAsync(existingUser);
            // Response to client
            var response = _mapper.Map<EmployeeLoginResponseModel>(existingUser);
            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.FullName = existingUser.FullName ?? "Unknown";

            // Take role
            var roles = await _userManager.GetRolesAsync(existingUser);
            response.Roles = roles.ToList();
            return new ApiSuccessResult<EmployeeLoginResponseModel>(response, "Refresh token successfully.");
        }

        public async Task<ApiResult<object>> ResetPassword(ResetPasswordRequestModel request)
        {
            // Check existed email
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (request.ConfirmPassword != request.Password)
            {
                return new ApiErrorResult<object>("Password is not matched.", System.Net.HttpStatusCode.NotFound);
            }
            // Valid token
            var result = await _userManager.ResetPasswordAsync(existingUser, request.Token, request.Password);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Reset password unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Reset password successfully.");
        }

        public async Task<ApiResult<object>> UpdateCoachProfile(UpdateEmployeeProfileRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value == null)
            {
                return new ApiErrorResult<object>("Plase login to use this function.", System.Net.HttpStatusCode.BadRequest);
            }
            var existingImage = existingUser.AvatarUrl;

            // Update user profile by mapper
            _mapper.Map(request, existingUser);
            existingUser.LastUpdatedTime = DateTime.Now;
            existingUser.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);
            if (request.AvatarUrl != null)
            {
                existingUser.AvatarUrl = await ImageHelper.Upload(request.AvatarUrl);
            }
            else
            {
                existingUser.AvatarUrl = existingImage;
            }
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Update profile unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Update profile successfully.");
        }

        public async Task<ApiResult<object>> UpdateEmployeeStatus(UpdateUserStatusRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value == null)
            {
                return new ApiErrorResult<object>("Plase login to use this function.", System.Net.HttpStatusCode.BadRequest);
            }
            // Check status included on enum
            if (!Enum.IsDefined(typeof(SystemConstant.EmployeeStatus), request.Status))
            {
                return new ApiErrorResult<object>("Status is not correct.", System.Net.HttpStatusCode.BadRequest);
            }
            // Update status
            existingUser.Status = (int)request.Status;
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Update status unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Update status successfully.");
        }

        public async Task<ApiResult<object>> UpdateUserAndOwnerProfile(UpdateUserProfileRequest request)
        {
            // check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value == null)
            {
                return new ApiErrorResult<object>("Plase login to use this function.", System.Net.HttpStatusCode.BadRequest);
            }
            var existingImage = existingUser.AvatarUrl;

            // Update user profile by mapper
            _mapper.Map(request, existingUser);
            existingUser.LastUpdatedTime = DateTime.Now;
            existingUser.LastUpdatedBy = int.Parse(_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value);

            if (request.AvatarUrl != null)
            {
                existingUser.AvatarUrl = await ImageHelper.Upload(request.AvatarUrl);
            }
            else
            {
                existingUser.AvatarUrl = existingImage;
            }
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Update profile unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Update profile successfully.");
        }

        public async Task<ApiResult<object>> UpdateUserStatus(UpdateUserStatusRequest request)
        {
            // Check existed user
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existingUser == null)
            {
                return new ApiErrorResult<object>("User is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            if (_contextAccessor.HttpContext?.User?.FindFirst("userId")?.Value == null)
            {
                return new ApiErrorResult<object>("Plase login to use this function.", System.Net.HttpStatusCode.BadRequest);
            }
            // Check status included on enum
            if (!Enum.IsDefined(typeof(SystemConstant.UserStatus), request.Status))
            {
                return new ApiErrorResult<object>("Status is not correct.", System.Net.HttpStatusCode.BadRequest);
            }

            // Update status
            existingUser.Status = (int)request.Status;
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Update status unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Update status successfully.");
        }

        public async Task<ApiResult<UploadImageResponseModel>> UploadImage(UploadImageRequest request)
        {
            string res = await TeamUp.Core.Utils.Firebase.ImageHelper.Upload(request.Image);
            return new ApiSuccessResult<UploadImageResponseModel>(new UploadImageResponseModel { ImageUrl = res });
        }

        public async Task<ApiResult<UserLoginResponseModel>> UserLogin(UserLoginRequestModel request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");
            }
            if (existingUser.DeletedBy != null)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");
            }
            var validPassword = await _userManager.CheckPasswordAsync(existingUser, request.Password);
            if (!validPassword)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");
            }
            var roles = await _userManager.GetRolesAsync(existingUser);
            
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(existingUser);
            if (!isConfirmed)
            {
                return new ApiErrorResult<UserLoginResponseModel>("Email hoặc mật khẩu không đúng.");

            }

            if (existingUser.Status == ((int)SystemConstant.UserStatus.InActive))
            {
                return new ApiErrorResult<UserLoginResponseModel>("You cannot access system.");

            }
            var refreshTokenData = GenerateRefreshToken();
            var accessTokenData = await GenerateAccessTokenAsync(existingUser);
            existingUser.RefreshToken = refreshTokenData.Item1;
            existingUser.RefreshTokenExpiryTime = refreshTokenData.Item2;

            await _userManager.UpdateAsync(existingUser);
            var response = _mapper.Map<UserLoginResponseModel>(existingUser);

            foreach (var role in roles)
            {
                response.Role = role;
            }

            response.AccessToken = accessTokenData.Item1;
            response.AccessTokenExpiredTime = accessTokenData.Item2;
            response.RefreshToken = refreshTokenData.Item1;
            response.RefreshTokenExpiryTime = refreshTokenData.Item2;
            return new ApiSuccessResult<UserLoginResponseModel>(response, "Đăng nhập thành công.");
        }

        public async Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleByRole(UserLoginGoogleRequest request, string roleId)
        {
            var userCheckExisted = await _userManager.FindByEmailAsync(request.Email);
            if (userCheckExisted != null)
            {
                if (userCheckExisted.Status == ((int)SystemConstant.UserStatus.InActive))
                {
                    return new ApiErrorResult<UserLoginResponseModel>("You cannot access system.", HttpStatusCode.Forbidden);
                }

                var refreshTokenData = GenerateRefreshToken();
                var accessTokenData = await GenerateAccessTokenAsync(userCheckExisted);

                userCheckExisted.RefreshToken = refreshTokenData.Item1;
                userCheckExisted.RefreshTokenExpiryTime = refreshTokenData.Item2;

                await _userManager.UpdateAsync(userCheckExisted);

                var response = _mapper.Map<UserLoginResponseModel>(userCheckExisted);
                response.AccessToken = accessTokenData.Item1;
                response.AccessTokenExpiredTime = accessTokenData.Item2;
                response.RefreshToken = refreshTokenData.Item1;
                response.RefreshTokenExpiryTime = refreshTokenData.Item2;

                return new ApiSuccessResult<UserLoginResponseModel>(response);
            }

            var newUser = new ApplicationUser
            {
                Email = request.Email,
                EmailConfirmed = request.Email_verified,
                FullName = $"{request.Family_name} {request.Given_name} {request.Name}",
                AvatarUrl = request.Picture,
                UserName = await _generateGGUsernameAsync(),
                Status = (int)UserStatus.Active
            };

            var createUserResult = await _userManager.CreateAsync(newUser);
            if (!createUserResult.Succeeded)
            {
                var errors = createUserResult.Errors.Select(e => e.Description).ToList();
                return new ApiErrorResult<UserLoginResponseModel>("Create user failed.", errors);
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                await _userManager.DeleteAsync(newUser);
                return new ApiErrorResult<UserLoginResponseModel>("Role not found.");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(newUser, role.Name);
            if (!addToRoleResult.Succeeded)
            {
                await _userManager.DeleteAsync(newUser);
                var errors = addToRoleResult.Errors.Select(e => e.Description).ToList();
                return new ApiErrorResult<UserLoginResponseModel>("Add to role failed.", errors);
            }

            var loginInfo = new UserLoginInfo(Provider.GOOGLE, request.Sub, Provider.GOOGLE);
            var addLoginResult = await _userManager.AddLoginAsync(newUser, loginInfo);
            if (!addLoginResult.Succeeded)
            {
                await _userManager.DeleteAsync(newUser);
                var errors = addLoginResult.Errors.Select(e => e.Description).ToList();
                return new ApiErrorResult<UserLoginResponseModel>("Add login failed.", errors);
            }

            var refreshTokenDataNew = GenerateRefreshToken();
            var accessTokenDataNew = await GenerateAccessTokenAsync(newUser);
            newUser.RefreshToken = refreshTokenDataNew.Item1;
            newUser.RefreshTokenExpiryTime = refreshTokenDataNew.Item2;

            await _userManager.UpdateAsync(newUser);

            var responseModel = _mapper.Map<UserLoginResponseModel>(newUser);
            responseModel.AccessToken = accessTokenDataNew.Item1;
            responseModel.AccessTokenExpiredTime = accessTokenDataNew.Item2;
            responseModel.RefreshToken = refreshTokenDataNew.Item1;
            responseModel.RefreshTokenExpiryTime = refreshTokenDataNew.Item2;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "WelcomeGG.html");
            if (!File.Exists(path))
            {
                return new ApiErrorResult<UserLoginResponseModel>("Không tìm thấy file gửi mail");
            }
            var content = File.ReadAllText(path);
            content = content.Replace("{{Name}}", newUser.FullName);

            string roleMessage = roleId switch
            {
                "2" => $"Chúc mừng {newUser.FullName} đã đăng ký thành công tài khoản Người chơi trên TeamUp.",
                "3" => $"Chúc mừng {newUser.FullName} đã trở thành Chủ sân thể thao trên TeamUp.",
                "4" => $"Chúc mừng {newUser.FullName} đã gia nhập TeamUp với vai trò Huấn luyện viên.",
                _ => $"Chúc mừng {newUser.FullName} đã đăng ký thành công tài khoản trên TeamUp."
            };

            content = content.Replace("{{RoleMessage}}", roleMessage);
            DoingMail.SendMail("TeamUp", "Welcome", content, newUser.Email);

            return new ApiSuccessResult<UserLoginResponseModel>(responseModel);
        }

        private async Task<string> _generateGGUsernameAsync()
        {
            Random random = new Random();
            while (true)
            {
                string username = "GG_" + random.Next(0, 999999);
                var usernameCheckExisted = await _userManager.FindByNameAsync(username);
                if (usernameCheckExisted == null)
                {
                    return username;
                }
            }
        }

        public async Task<ApiResult<object>> RegisterWithRole(UserRegisterRequestModel request, string roleId)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.Status == (int)UserStatus.Active);
            if (existingUser != null)
            {
                return new ApiErrorResult<object>("Email is existed.", System.Net.HttpStatusCode.BadRequest);
            }

            var existingUser2 = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email && x.Status == (int)UserStatus.InActive);
            ApplicationUser user;

            if (existingUser2 != null)
            {
                // Update user cũ thay vì xóa
                existingUser2.FullName = request.FullName;
                existingUser2.UserName = await _generateUsernameOfGuestAsync();
                existingUser2.PhoneNumber = request.PhoneNumber;
                existingUser2.Status = (int)UserStatus.InActive;

                var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(existingUser2);
                var resetResult = await _userManager.ResetPasswordAsync(existingUser2, resetPassToken, request.Password);
                if (!resetResult.Succeeded)
                {
                    return new ApiErrorResult<object>("Reset mật khẩu không thành công.", resetResult.Errors.Select(x => x.Description).ToList());
                }

                // Xóa roles cũ (nếu có) và gán role mới
                var existingRoles = await _userManager.GetRolesAsync(existingUser2);
                if (existingRoles.Count > 0)
                {
                    await _userManager.RemoveFromRolesAsync(existingUser2, existingRoles);
                }

                user = existingUser2;
            }
            else
            {
                user = new ApplicationUser
                {
                    Email = request.Email,
                    UserName = await _generateUsernameOfGuestAsync(),
                    FullName = request.FullName,
                    Status = (int)UserStatus.InActive,
                    PhoneNumber = request.PhoneNumber
                };

                var createResult = await _userManager.CreateAsync(user, request.Password);
                if (!createResult.Succeeded)
                {
                    return new ApiErrorResult<object>("Register unsuccessfully.", createResult.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
                }
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return new ApiErrorResult<object>("Không tìm thấy vai trò.");
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!addRoleResult.Succeeded)
            {
                return new ApiErrorResult<object>("Không thể gán vai trò.", addRoleResult.Errors.Select(x => x.Description).ToList());
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "Welcome.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");
            }

            var content = File.ReadAllText(path);
            content = content.Replace("{{OTP}}", Uri.EscapeDataString(token));
            content = content.Replace("{{Name}}", user.Email);

            string roleMessage = roleId switch
            {
                "2" => $"Chúc mừng {user.Email} đã đăng ký thành công tài khoản Người chơi trên TeamUp.",
                "3" => $"Chúc mừng {user.Email} đã trở thành Chủ sân thể thao trên TeamUp.",
                "4" => $"Chúc mừng {user.Email} đã gia nhập TeamUp với vai trò Huấn luyện viên.",
                _ => $"Chúc mừng {user.Email} đã đăng ký thành công tài khoản trên TeamUp."
            };
            content = content.Replace("{{RoleMessage}}", roleMessage);

            var resultSendMail = DoingMail.SendMail("TeamUp", "Welcome", content, user.Email);
            if (!resultSendMail)
            {
                return new ApiErrorResult<object>("Cannot send email to " + request.Email);
            }

            return new ApiSuccessResult<object>("Please check your email to confirm.");
        }

        public async Task<ApiResult<object>> ResendOtpAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ApiErrorResult<object>("Không tìm thấy người dùng với email này.");
            }

            if (user.Status == (int)UserStatus.Active)
            {
                return new ApiErrorResult<object>("Tài khoản đã được xác nhận, không cần gửi lại OTP.");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "Welcome.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");
            }

            var content = File.ReadAllText(path);
            content = content.Replace("{{OTP}}", Uri.EscapeDataString(token));
            content = content.Replace("{{Name}}", user.Email);

            string roleMessage = "Vui lòng xác nhận lại email để hoàn tất quá trình đăng ký tài khoản TeamUp.";
            content = content.Replace("{{RoleMessage}}", roleMessage);

            var sendResult = DoingMail.SendMail("TeamUp", "Xác nhận lại Email", content, user.Email);
            if (!sendResult)
            {
                return new ApiErrorResult<object>("Không thể gửi email xác nhận đến " + email);
            }

            return new ApiSuccessResult<object>("OTP đã được gửi lại thành công. Vui lòng kiểm tra email.");
        }

        private async Task<string> _generateUsernameOfGuestAsync()
        {
            Random random = new Random();
            while (true)
            {
                var username = "USER_" + random.Next(0, 999999).ToString("D6");

                var userCheckExisted = await _userManager.FindByNameAsync(username);
                if (userCheckExisted == null)
                {
                    return username;
                }
            }
        }

        public async Task<ApiResult<BasePaginatedList<EmployeeResponseModel>>> GetOwnerPagination(BaseSearchRequest request)
        {
            var ownerRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == SystemConstant.Role.OWNER);
            if (ownerRole == null)
            {
                return new ApiErrorResult<BasePaginatedList<EmployeeResponseModel>>("Role OWNER not found");
            }

            var ownerUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == ownerRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            var usersQuery = _userManager.Users
                .OrderByDescending(r => r.LastUpdatedTime)
                .Where(u => ownerUserIds.Contains(u.Id) && u.DeletedBy == null);

            // Apply search
            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                var searchLower = request.SearchValue.ToLower();
                usersQuery = usersQuery.Where(x =>
                    x.FullName.ToLower().Contains(searchLower) ||
                    x.Email.ToLower().Contains(searchLower));
            }

            // Pagination
            var currentPage = request.PageIndex ?? 1;
            var pageSize = request.PageSize ?? SystemConstant.PAGE_SIZE;
            var total = await usersQuery.CountAsync();
            var pagedUsers = await usersQuery
                .OrderBy(u => u.FullName) // Optional: consistent order
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = pagedUsers.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Type = user.Type,
                Specialty = user.Specialty,
                Certificate = user.Certificate,
                WorkingAddress = user.WorkingAddress,
                WorkingDate = user.WorkingDate,
                PricePerSession = user.PricePerSession,
                StatusForCoach = user.StatusForCoach,

                StartDate = user.StartDate,
                ExpireDate = user.ExpireDate,

                Package = _mapper.Map<PackageModelView>(user.Package),

                Status = Enum.IsDefined(typeof(EmployeeStatus), user.Status)
                    ? ((EmployeeStatus)user.Status).ToString()
                    : "Unknown",
                Role = new RoleModelView
                {
                    Id = ownerRole.Id,
                    Name = ownerRole.Name
                }
            }).ToList();

            var response = new BasePaginatedList<EmployeeResponseModel>(items, total, currentPage, pageSize);
            // return to client
            return new ApiSuccessResult<BasePaginatedList<EmployeeResponseModel>>(response);
        }

        public async Task<ApiResult<List<EmployeeResponseModel>>> GetAllOwner()
        {
            var ownerRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == SystemConstant.Role.OWNER);


            // Filter users

            var doctorUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == ownerRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = await _userManager.Users
                .Where(u => doctorUserIds.Contains(u.Id) && u.DeletedBy == null)
                .OrderByDescending(x => x.LastUpdatedTime)
                .ToListAsync();



            var items = users.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Specialty = user.Specialty,
                Certificate = user.Certificate,
                WorkingAddress = user.WorkingAddress,
                WorkingDate = user.WorkingDate,
                PricePerSession = user.PricePerSession,
                StatusForCoach = user.StatusForCoach,

                StartDate = user.StartDate,
                ExpireDate = user.ExpireDate,

                Package = _mapper.Map<PackageModelView>(user.Package),

                Status = Enum.IsDefined(typeof(EmployeeStatus), user.Status)
                    ? ((EmployeeStatus)user.Status).ToString()
                    : "Unknown",
                Role = new RoleModelView
                {
                    Id = ownerRole.Id,
                    Name = ownerRole.Name
                }
            }).ToList();

            // return to client
            return new ApiSuccessResult<List<EmployeeResponseModel>>(items);
        }

        public async Task<ApiResult<BasePaginatedList<EmployeeResponseModel>>> GetCoachPagination(BaseSearchRequest request)
        {
            var coachRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == SystemConstant.Role.COACH);
            if (coachRole == null)
            {
                return new ApiErrorResult<BasePaginatedList<EmployeeResponseModel>>("Role OWNER not found");
            }

            var coachUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == coachRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            var usersQuery = _userManager.Users
                .OrderByDescending(r => r.LastUpdatedTime)
                .Where(u => coachUserIds.Contains(u.Id) && u.DeletedBy == null && u.StatusForCoach == PackageStatus.Active);

            // Apply search
            if (!string.IsNullOrWhiteSpace(request.SearchValue))
            {
                var searchLower = request.SearchValue.ToLower();
                usersQuery = usersQuery.Where(x =>
                    x.FullName.ToLower().Contains(searchLower) ||
                    x.Email.ToLower().Contains(searchLower) || x.Type.ToLower().Contains(searchLower));
            }

            // Pagination
            var currentPage = request.PageIndex ?? 1;
            var pageSize = request.PageSize ?? SystemConstant.PAGE_SIZE;
            var total = await usersQuery.CountAsync();
            var pagedUsers = await usersQuery
                .OrderBy(u => u.FullName) // Optional: consistent order
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = pagedUsers.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Type = user.Type,
                Specialty = user.Specialty,
                Certificate = user.Certificate,
                WorkingAddress = user.WorkingAddress,
                WorkingDate = user.WorkingDate,
                PricePerSession = user.PricePerSession,
                StatusForCoach = user.StatusForCoach,

                StartDate = user.StartDate,
                ExpireDate = user.ExpireDate,

                Package = _mapper.Map<PackageModelView>(user.Package),

                Status = Enum.IsDefined(typeof(EmployeeStatus), user.Status)
                    ? ((EmployeeStatus)user.Status).ToString()
                    : "Unknown",
                Role = new RoleModelView
                {
                    Id = coachRole.Id,
                    Name = coachRole.Name
                }
            }).ToList();

            var response = new BasePaginatedList<EmployeeResponseModel>(items, total, currentPage, pageSize);
            // return to client
            return new ApiSuccessResult<BasePaginatedList<EmployeeResponseModel>>(response);
        }

        public async Task<ApiResult<List<EmployeeResponseModel>>> GetAllCoach()
        {
            var coachRole = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == SystemConstant.Role.COACH);


            // Filter users

            var doctorUserIds = await _unitOfWork.GetRepository<ApplicationUserRole>().Entities
                .Where(ur => ur.RoleId == coachRole.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            // Lọc danh sách user theo UserId từ bảng User
            var users = await _userManager.Users
                .Where(u => doctorUserIds.Contains(u.Id) && u.DeletedBy == null && u.StatusForCoach == PackageStatus.Active)
                .OrderByDescending(x => x.LastUpdatedTime)
                .ToListAsync();



            var items = users.Select(user => new EmployeeResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Specialty = user.Specialty,
                Certificate = user.Certificate,
                WorkingAddress = user.WorkingAddress,
                WorkingDate = user.WorkingDate,
                PricePerSession = user.PricePerSession,
                StatusForCoach = user.StatusForCoach,

                StartDate = user.StartDate,
                ExpireDate = user.ExpireDate,

                Package = _mapper.Map<PackageModelView>(user.Package),

                Status = Enum.IsDefined(typeof(EmployeeStatus), user.Status)
                    ? ((EmployeeStatus)user.Status).ToString()
                    : "Unknown",
                Role = new RoleModelView
                {
                    Id = coachRole.Id,
                    Name = coachRole.Name
                }
            }).ToList();

            // return to client
            return new ApiSuccessResult<List<EmployeeResponseModel>>(items);
        }

        public async Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleForUser(UserLoginGoogleRequest request)
        {
            return await UserLoginGoogleByRole(request, roleId: "2");
        }

        public async Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleForOwner(UserLoginGoogleRequest request)
        {
            return await UserLoginGoogleByRole(request, roleId: "3");
        }

        public async Task<ApiResult<UserLoginResponseModel>> UserLoginGoogleForCoach(UserLoginGoogleRequest request)
        {
            return await UserLoginGoogleByRole(request, roleId: "4");
        }

        public async Task<ApiResult<object>> RegisterUser(UserRegisterRequestModel request)
        {
            return await RegisterWithRole(request, roleId: "2");
        }

        public async Task<ApiResult<object>> RegisterOwner(UserRegisterRequestModel request)
        {
            return await RegisterWithRole(request, roleId: "3");
        }

        public async Task<ApiResult<object>> RegisterCoach(UserRegisterRequestModel request)
        {
            return await RegisterWithRole(request, roleId: "4");
        }
    }
}
