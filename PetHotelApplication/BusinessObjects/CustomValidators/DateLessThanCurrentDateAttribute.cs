using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.CustomValidators
{
    public class DateLessThanCurrentDateAttribute : ValidationAttribute
    {
        public string ErrorMessage { get; set; }        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateOnly date)
            {
                if (date >= DateOnly.FromDateTime(DateTime.Now))
                {
                    return new ValidationResult(ErrorMessage);
                } 
            }

            return ValidationResult.Success;
        }
    }
}
