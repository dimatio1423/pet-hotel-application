using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.UserModel.Request
{
    public class RegisterUserReqModel
    {
        [Required(ErrorMessage = "Full name is required")]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]+$", ErrorMessage = "Full name cannot contain numbers or special characters")]
        [Display(Name = "Full name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"^\d{9,10}$", ErrorMessage = "Phone number must be 9 or 10 digits and cannot contain letters.")]
        [Display(Name = "Phone number")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(255, ErrorMessage = "Address cannot be longer than 255 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password cannot be longer than 100 characters")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        [Display(Name = "Confirm password")]
        public string? ConfirmPassword { get; set; }
    }
}
