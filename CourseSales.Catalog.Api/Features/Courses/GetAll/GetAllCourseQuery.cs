using CourseSales.Catalog.Api.Features.Courses.Dtos;

namespace CourseSales.Catalog.Api.Features.Courses.GetAll
{
    public class GetAllCourseQuery:IRequestByServiceResult<List<CourseDto>>
    {
    }

    public class GetAllCourseHandler(AppDbContext context,IMapper mapper) : IRequestHandler<GetAllCourseQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.ToListAsync(cancellationToken);
            var category = await context.Categories.ToListAsync(cancellationToken);
            courses.ForEach(i=>i.Category=category.First(c=>c.Id==i.CategoryId));
            var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
        }

      
    }

    public static class GetAllCourseEndPoint
    {
        public static RouteGroupBuilder GetAllCourseGroupItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllCourseQuery());
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 0);

            return group;
        }
    }
}
