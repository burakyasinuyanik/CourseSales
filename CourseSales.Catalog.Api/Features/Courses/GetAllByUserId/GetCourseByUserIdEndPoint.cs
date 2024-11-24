using CourseSales.Catalog.Api.Features.Courses.Dtos;

namespace CourseSales.Catalog.Api.Features.Courses.GetAllByUserId
{
    public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

    public class GetCourseByIdHandler(AppDbContext context, IMapper mapper)
        : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync(cancellationToken);

            var courses = await context.Courses.Where(c => c.UserId == request.Id).ToListAsync(cancellationToken);
         
            if (courses is null)
                return ServiceResult<List<CourseDto>>.Error("Kurs bulunamadı", "kurs listesi boş",
                    HttpStatusCode.NotFound);
            
            
            courses.ForEach(c=>c.Category=categories.FirstOrDefault(x=>x.Id==c.CategoryId)!);


            var courseAsDto = mapper.Map<List<CourseDto>>(courses);

            return ServiceResult<List<CourseDto>>.SuccessAsOk(courseAsDto);
        }
    }


    public static class GetCourseByUserIdEndPoint
    {
        public static RouteGroupBuilder GetCourseByUserIdItemEndPoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:Guid}", async (IMediator mediator, Guid userId) =>
            {
                var result = await mediator.Send(new GetCourseByUserIdQuery(userId));

                return result.ToGenericResult();
            });

            return group;
        }

        
    }
}
