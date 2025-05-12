using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using TeamUp.Contract.Repositories.Entity;
using TeamUp.Contract.Services.Interface;
using TeamUp.Repositories.Context;
using TeamUp.Repositories.Entity;
using TeamUp.Repositories.Mapper;
using TeamUp.Services;
using TeamUp.Services.Service;
using VNPAY.NET;

namespace TeamUp.API
{
    public static class DependencyInjection
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigRoute();
            services.AddDatabase(configuration);
            services.AddIdentity();
            services.AddInfrastructure(configuration);
            services.AddServices();
            services.AddAutoMapperProfiles();
        }
        public static void ConfigRoute(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
        }
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("TeamUpDB"));
            });
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
             .AddEntityFrameworkStores<DatabaseContext>()
             .AddDefaultTokenProviders();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IRealTimeService, RealTimeService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAIWebsiteService, AIWebsiteService>()
                .AddScoped<ISportsComplexService, SportsComplexService>()
                .AddScoped<ICourtService, CourtService>()
                .AddScoped<IRoomService, RoomService>()
                .AddScoped<IRoomJoinRequestService, RoomJoinRequestService>()
                .AddScoped<ICourtBookingService, CourtBookingService>()
                .AddScoped<ICoachBookingService, CoachBookingService>()
                .AddScoped<IVnpay, Vnpay>()
                .AddHttpContextAccessor();
        }

        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }

        public static void AddCorsPolicyBackend(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder
                        .WithOrigins(
                            "http://localhost:5173",
                            "http://localhost:3000",
                            "https://baby-care-theta.vercel.app"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

        }
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "TeamUp", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void AddConfigJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role
                };
            });
        }
    }
}
