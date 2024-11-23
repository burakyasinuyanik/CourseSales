using CourseSales.Catalog.Api.Features.Courses.Create;
using CourseSales.Catalog.Api.Features.Courses.GetAll;
using CourseSales.Catalog.Api.Features.Courses.GetById;
using CourseSales.Catalog.Api.Features.Courses.Update;

namespace CourseSales.Catalog.Api.Features.Courses
{
    public static class CourseEndPointExt
    {
        public static void AddCourseGroupEndPointExt(this WebApplication app)
        {
            app.MapGroup("api/courses")
                .WithTags("Courses")
                .CreateCourseGroupItemEndPoint()
                .GetAllCourseGroupItemEndPoint()
                .GetCourseByIdItemEndPoint()
                .UpdateCourseCommandItemEndPoint();
        }
    }
}
