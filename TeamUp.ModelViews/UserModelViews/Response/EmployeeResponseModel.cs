﻿using TeamUp.Contract.Repositories.Entity;
using TeamUp.ModelViews.PackageModelViews;
using TeamUp.ModelViews.RoleModelViews;

namespace TeamUp.ModelViews.UserModelViews.Response
{
    public class EmployeeResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public int? Age { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }

        public string? Type { get; set; } 
        public string? Specialty { get; set; }
        public string? Certificate { get; set; }
        public string? WorkingAddress { get; set; }
        public string? WorkingDate { get; set; }
        public decimal? PricePerSession { get; set; }
        public string? Experience { get; set; }
        public string? TargetObject { get; set; }

        public string? StatusForCoach { get; set; }

        public string? Status { get; set; }

        public virtual PackageModelView? Package { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        public RoleModelView Role { get; set; }
    }
}
