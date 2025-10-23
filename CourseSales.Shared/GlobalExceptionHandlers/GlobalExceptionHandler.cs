using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Shared.GlobalExceptionHandlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Title = "Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                Type=exception.GetType().Name,
                Status = (int)HttpStatusCode.InternalServerError,
            }, cancellationToken);
            return true;
        }
    }
}
