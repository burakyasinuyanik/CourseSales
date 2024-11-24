using Asp.Versioning.Builder;
using CourseSales.Catalog.Api.Features.Courses.Create;
using CourseSales.Catalog.Api.Features.Courses.Delete;
using CourseSales.Catalog.Api.Features.Courses.GetAll;
using CourseSales.Catalog.Api.Features.Courses.GetAllByUserId;
using CourseSales.Catalog.Api.Features.Courses.GetById;
using CourseSales.Catalog.Api.Features.Courses.Update;

namespace CourseSales.Catalog.Api.Features.Courses
{
    public static class CourseEndPointExt
    {
        public static void AddCourseGroupEndPointExt(this WebApplication app,ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/courses")
                .WithTags("Courses")
                .CreateCourseGroupItemEndPoint()
                .GetAllCourseGroupItemEndPoint()
                .GetCourseByIdItemEndPoint()
                .UpdateCourseCommandItemEndPoint()
                .DeleteCourseByIdItemEndPoint()
                .GetCourseByUserIdItemEndPoint()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
