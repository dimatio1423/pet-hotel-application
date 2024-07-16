using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.ComponentModel.DataAnnotations;

namespace PetHotelApplicationRazorPage.Pages.User.Payment
{
    public class PaymentModel : PageModel
    {
        [BindProperty]
        public PaymentInfo Payment { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Process payment here

            return RedirectToPage("/Index"); // Redirect to a success page after processing payment
        }

        public class PaymentInfo
        {
            [Required(ErrorMessage = "Name on card is required.")]
            public string NameOnCard { get; set; }

            [Required(ErrorMessage = "Card number is required.")]
            [CreditCard(ErrorMessage = "Invalid card number.")]
            public string CardNumber { get; set; }

            [Required(ErrorMessage = "Expiration date is required.")]
            [DataType(DataType.Date)]
            public DateTime ExpirationDate { get; set; }

            [Required(ErrorMessage = "CVV is required.")]
            [Range(100, 9999, ErrorMessage = "Invalid CVV.")]
            public int CVV { get; set; }

            [Required(ErrorMessage = "Amount is required.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Invalid amount.")]
            public decimal Amount { get; set; }
        }
    }
}
