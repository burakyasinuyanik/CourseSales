using System.ComponentModel.DataAnnotations;

namespace CourseSales.Web.Pages.Auth.SignUp
{
    public record class SignUpViewModel
    {
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "Ad bilgisi gereklidir.")]
        public string? FirstName { get; init; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Soyad bilgisi gereklidir.")]
        public string? LastName { get; init; }

        [Display(Name = "UserName:")]
        [Required(ErrorMessage = "Kullanıcı bilgisi gereklidir.")]
        public string? UserName { get; init; }

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Mail bilgisi gereklidir.")]
        [EmailAddress(ErrorMessage = "Doğru bir email giriniz.")]
        public string? Email { get; init; }

        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Şifre bilgisi zorunludur.")]
        public string? Password { get; init; }

        [Display(Name = "Password Confirm:")]
        [Required(ErrorMessage = "Doğrulama şifre bilgisi zorunludur.")]
        [Compare(nameof(Password), ErrorMessage = "Şifre bilgisi eşleşmedi")]
        public string? PasswordConfirm { get; init; }

        public static SignUpViewModel Empty => new SignUpViewModel
        {
            FirstName = string.Empty,
            LastName = string.Empty,
            UserName = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            PasswordConfirm = string.Empty
        };

        public static SignUpViewModel GetExlampleModel => new SignUpViewModel
        {
            FirstName = "burak",
            LastName = "yasin",
            UserName = "burakyasin",
            Email = "burak@burak.com",
            Password = "password12*",
            PasswordConfirm = "password12*"
        };
    }



}
