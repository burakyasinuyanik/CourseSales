using CourseSales.Catalog.Api.Features.Courses.Create;
using CourseSales.Catalog.Api.Features.Courses.Dtos;
using CourseSales.Catalog.Api.Features.Courses.Update;

namespace CourseSales.Catalog.Api.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<UpdateCourseCommand, Course>();
        }
    }
}
