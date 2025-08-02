using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Shared.Services
{
    public class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
    {
        public Guid GetUserId
        {
            get
            {

                if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                    throw new UnauthorizedAccessException("Kullanıcı giriş gereklidir.");

                return Guid.Parse(httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);
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
