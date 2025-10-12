using CourseSales.Web.Pages.Auth.SignIn;
using CourseSales.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseSales.Web.Pages.Auth
{
    public class SignInModel(SignInService signInService) : PageModel
    {
        [BindProperty] public SignInViewModel SignInViewModel { get; set; } = SignInViewModel.GetExlampleModel;

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await signInService.AuthenticateAsync(SignInViewModel);
            if(result.IsFail)
            {
                ModelState.AddModelError(string.Empty, result.Fail.Title);
                ModelState.AddModelError(string.Empty, result.Fail.Detail);
                return Page();
            }
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnGetSignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("/Index");
        }
    }
}
