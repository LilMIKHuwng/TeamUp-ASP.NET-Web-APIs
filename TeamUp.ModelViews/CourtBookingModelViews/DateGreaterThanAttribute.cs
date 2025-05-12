using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamUp.ModelViews.CourtBookingModelViews
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentValue = (DateTime?)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                return new ValidationResult($"Thuộc tính {_comparisonProperty} không tồn tại");

            var comparisonValue = (DateTime?)property.GetValue(validationContext.ObjectInstance);

            if (currentValue.HasValue && comparisonValue.HasValue && currentValue <= comparisonValue)
            {
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} phải lớn hơn {_comparisonProperty}");
            }

            return ValidationResult.Success;
        }
    }
}
