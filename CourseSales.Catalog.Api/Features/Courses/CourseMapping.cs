using CourseSales.Catalog.Api.Features.Courses.Create;

namespace CourseSales.Catalog.Api.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
        }
    }
}
