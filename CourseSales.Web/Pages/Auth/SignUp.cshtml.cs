using CourseSales.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CourseSales.Web.Pages.Auth
{
    public class SignUpModel(SignUpService signUpService) : PageModel
    {
        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.GetExlampleModel;


        public void OnGet()
        {
        }
        public async Task<IActionResult> OnpostAsync()
        {
            var result = await signUpService.CreateAccount(SignUpViewModel);
            if (result.IsFail)
            {
                ModelState.AddModelError(string.Empty, result.Fail.Title);
                if(!string.IsNullOrEmpty(result.Fail.Detail))
                {
                    ModelState.AddModelError(string.Empty, result.Fail.Title);

                }
                return Page();
            }
           
               return RedirectToPage("/Index");

            
        }
    }
}
