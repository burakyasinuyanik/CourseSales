using CourseSales.Catalog.Api.Features.Courses.Create;

namespace CourseSales.Catalog.Api.Features.Courses
{
    public static class CourseEndPointExt
    {
        public static void AddCourseGroupEndPointExt(this WebApplication app)
        {
            app.MapGroup("api/courses")
                .WithTags("Courses")
                .CreateCourseGroupItemEndPoint();
        }
    }
}
