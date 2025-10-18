using System.Security.Claims;

namespace CourseSales.Web.Services
{
    public class UserService(IHttpContextAccessor httpContextAccessor) 
    {
        public Guid GetUserId
        {
            get
            {

                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                    throw new UnauthorizedAccessException("Kullanıcı giriş gereklidir.");

                return Guid.Parse(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "sub")!.Value!);
            }
        }
        public string UserName
        {
            get
            {
                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                    throw new UnauthorizedAccessException("Kullanıcı giriş gereklidir.");


                return httpContextAccessor.HttpContext!.User.Identity!.Name!;
            }
        }

        public List<string> Roles
        {
            get
            {
                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                    throw new UnauthorizedAccessException("Kullanıcı giriş gereklidir.");
            
                return httpContextAccessor.HttpContext!.User.Claims.Where(x=>x.Type == ClaimTypes.Role).Select(x=>x.Value).ToList();    
            }

        }
    }
}
