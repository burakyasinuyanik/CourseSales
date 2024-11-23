using CourseSales.Catalog.Api.Features.Categories.Dtos;
using CourseSales.Catalog.Api.Features.Courses.Dtos;

namespace CourseSales.Catalog.Api.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetCourseByIdHandler(AppDbContext context,IMapper mapper):IRequestHandler<GetCourseByIdQuery,ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(c=>c.Id==request.Id, cancellationToken);
           
            if (course is null)
                return ServiceResult<CourseDto>.Error("Kurs bulunamadı", $"{request.Id}'li kurs bulunamadı", HttpStatusCode.NotFound);
            var category = await context.Categories.FindAsync(course.CategoryId);
            course.Category = category;
            
            var courseAsDto = mapper.Map<CourseDto>(course);

            return ServiceResult<CourseDto>.SuccessAsOk(courseAsDto);
        }
    }


    public static class GetCourseByIdGroupEndPoint
    {
        public static RouteGroupBuilder GetCourseByIdItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:Guid}", async (IMediator mediator, Guid id) =>
            {
                var result = await mediator.Send(new GetCourseByIdQuery(id));

                return result.ToGenericResult();
            });

            return group;
        }
    }
}
