using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PetHotelApplicationRazorPage.Pages.User.Payment
{
    public class PaymentMethodModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Please select a payment method.")]
        public string SelectedPaymentMethod { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SelectedPaymentMethod == "Cash")
            {
                return RedirectToPage("/Index");
            }
            else if (SelectedPaymentMethod == "CreditCard")
            {
                return RedirectToPage("Payment");
            }

            return Page();
        }
    }
}
