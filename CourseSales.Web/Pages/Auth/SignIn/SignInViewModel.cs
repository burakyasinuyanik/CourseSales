using CourseSales.Web.Pages.Auth.SignUp;
using System.ComponentModel.DataAnnotations;

namespace CourseSales.Web.Pages.Auth.SignIn
{
    public record SignInViewModel
    {
        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Mail bilgisi gereklidir.")]
        [EmailAddress(ErrorMessage = "Doğru bir email giriniz.")]
        public string? Email { get; init; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Şifre bilgisi zorunludur.")]
        public string? Password { get; init; }
        public static SignInViewModel Empty => new SignInViewModel
        {
            
            Email = string.Empty,
            Password = string.Empty
            
        };

        public static SignInViewModel GetExlampleModel => new SignInViewModel
        {
           
            Email = "burak@burak.com",
            Password = "password12*"
           
        };
    }
}
