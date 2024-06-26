using BusinessObjects.Entities;
using BusinessObjects.Enums.RoleEnums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PetHotelApplicationRazorPage.Pages
{
    public class AuthorizePageModel : PageModel
    {
        public BusinessObjects.Entities.User currentAccount { get; private set; }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);

            var path = HttpContext.Request.Path;

            currentAccount = SessionHelper.GetObjectSession<BusinessObjects.Entities.User>(HttpContext.Session, "Account");

            if (currentAccount == null)
            {
                if (path.Value.ToLower().Contains("/staff") || path.Value.ToLower().Contains("/admin") || path.Value.ToLower().Contains("/manager"))
                {
                    context.Result = Redirect("/User/Login");
                }

                if (path.Value.ToLower().Contains("/user/pets"))
                {
                    context.Result = Redirect("/User/Login");
                }

                if (path.Value.ToLower().Contains("/user/feedbacks/create") 
                    || path.Value.ToLower().Contains("/user/feedbacks/details") 
                    || path.Value.ToLower().Contains("/user/feedbacks/delete"))
                {
                    context.Result = Redirect("/User/Login");
                }

            }
            else
            {

                if (path.Value.ToLower().Contains("/user/pets"))
                {
                    if (currentAccount.RoleId.Equals(((int)RoleEnums.Staff + 1).ToString()))
                    {
                        context.Result = Redirect("/Staff");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Admin + 1).ToString()))
                    {
                        context.Result = Redirect("/Admin");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Manager + 1).ToString()))
                    {
                        context.Result = Redirect("/Manager");
                    }
                }


                if (path.Value.Equals("/"))
                {
                    if (currentAccount.RoleId.Equals(((int)RoleEnums.Staff + 1).ToString()))
                    {
                        context.Result = Redirect("/Staff");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Admin + 1).ToString()))
                    {
                        context.Result = Redirect("/Admin");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Manager + 1).ToString()))
                    {
                        context.Result = Redirect("/Manager");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Customer + 1).ToString()))
                    {
                        context.Result = Redirect("/Index");
                    }
                }

                if (path.Value.Contains("/PetCareServices"))
                {
                    if (currentAccount.RoleId.Equals(((int)RoleEnums.Staff + 1).ToString()))
                    {
                        context.Result = Redirect("/Staff");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Admin + 1).ToString()))
                    {
                        context.Result = Redirect("/Admin");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Manager + 1).ToString()))
                    {
                        context.Result = Redirect("/Manager");
                    }
                }

                if (path.Value.Equals("/User/Login"))
                {
                    if (currentAccount.RoleId.Equals(((int)RoleEnums.Staff + 1).ToString()))
                    {
                        context.Result = Redirect("/Staff");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Admin + 1).ToString()))
                    {
                        context.Result = Redirect("/Admin");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Manager + 1).ToString()))
                    {
                        context.Result = Redirect("/Manager");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Customer + 1).ToString()))
                    {
                        context.Result = Redirect("/Index");
                    }
                }

                if (path.Value.ToLower().Contains("/staff"))
                {
                    if (!currentAccount.RoleId.Equals(((int)RoleEnums.Staff + 1).ToString())){
                        context.Result = Redirect("/Forbidden");
                    }
                }

                if (path.Value.ToLower().Contains("/admin"))
                {
                    if (!currentAccount.RoleId.Equals(((int)RoleEnums.Admin + 1).ToString())){
                        context.Result = Redirect("/Forbidden");
                    }
                    
                }

                if (path.Value.ToLower().Contains("/manager"))
                {
                    if (!currentAccount.RoleId.Equals(((int)RoleEnums.Manager + 1).ToString())) {
                        context.Result = Redirect("/Forbidden");
                    }
                }

                if (path.Value.ToLower().Contains("/user/feedbacks"))
                {
                    if (currentAccount.RoleId.Equals(((int)RoleEnums.Staff + 1).ToString()))
                    {
                        context.Result = Redirect("/Staff");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Admin + 1).ToString()))
                    {
                        context.Result = Redirect("/Admin");
                    }
                    else if (currentAccount.RoleId.Equals(((int)RoleEnums.Manager + 1).ToString()))
                    {
                        context.Result = Redirect("/Manager");
                    }
                }
            }

        }

    }
}
