using CourseSales.Web.Pages.Auth.SignIn;
using CourseSales.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseSales.Web.Pages.Auth
{
    public class SignInModel : PageModel
    {
        [BindProperty] public SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExlampleModel;

        public void OnGet()
        {
        }
    }
}
