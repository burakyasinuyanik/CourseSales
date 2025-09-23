using System.ComponentModel.DataAnnotations;

namespace CourseSales.Web.Pages.Auth.SignUp
{
    public record class SignUpViewModel(
        [Display(Name ="First Name:")] string FirstName,
        [Display(Name = "Last Name:")] string LastName,
        [Display(Name = "UserName:")] string UserName,
        [Display(Name = "Email:")] string Email,
        [Display(Name = "Password:")] string Password,
        [Display(Name = "Password Confirm:")] string PasswordConfirm)
    {
        public static SignUpViewModel Empty => new(
            FirstName: string.Empty,
            LastName: string.Empty,
            UserName: string.Empty,
            Email: string.Empty,
            Password: string.Empty,
            PasswordConfirm: string.Empty);
        public static SignUpViewModel GetExlampleModel=>
            new(
                FirstName: "burak",
                LastName: "yasin",
                UserName: "burakyasin",
                Email: "burak@burak.com",
                Password:"password12*",
                PasswordConfirm: "password12*");
    }



}
