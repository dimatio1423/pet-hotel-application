using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.CustomValidators
{
    public class MaxImageCountAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public MaxImageCountAttribute(int min, int max)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as List<IFormFile>;
            if (list == null || list.Count < _min || list.Count > _max)
            {
                return new ValidationResult($"Please choose between {_min} and {_max} images.");
            }
            return ValidationResult.Success!;
        }
    }
}
