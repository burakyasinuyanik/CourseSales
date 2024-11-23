using CourseSales.Catalog.Api.Features.Courses.Dtos;
using CourseSales.Shared.Filters;

namespace CourseSales.Catalog.Api.Features.Courses.Update
{
    public record UpdateCourseCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string? ImageUrl,
        Guid CategoryId,
        FeatureDto Feature
    ) : IRequestByServiceResult;

    public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCourse = await context.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (hasCourse is null)
                return ServiceResult.ErrorNotFound();

            var updateCourse = mapper.Map<Course>(request);
            context.Courses.Update(updateCourse);
            await context.SaveChangesAsync(cancellationToken);


            return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class UpdateCourseCommandGroupEndPoint
    {
        public static RouteGroupBuilder UpdateCourseCommandItemEndPoint(this RouteGroupBuilder group)
        {

            group.MapPut("/", async (UpdateCourseCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }

    }
}
