namespace CourseSales.Catalog.Api.Features.Courses.Delete
{
    public record DeleteCourseByIdCommand(Guid Id) : IRequestByServiceResult;

    public class DeleteCourseByIdHandler(AppDbContext context):IRequestHandler<DeleteCourseByIdCommand,ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseByIdCommand request, CancellationToken cancellationToken)
        {
            var hasCourse =
                await context.Courses.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
            if(hasCourse is null)
                return ServiceResult.ErrorNotFound();

            context.Courses.Remove(hasCourse);
           await context.SaveChangesAsync(cancellationToken);

           return ServiceResult.SuccessAsNoContent();
        }
    }

    public static class DeleteCourseByIdGroupEndPoint
    {
        public static RouteGroupBuilder DeleteCourseByIdItemEndPoint(this RouteGroupBuilder group)
        {

            group.MapDelete("/{id:Guid}", async (IMediator mediator,Guid id) =>
            {
                var result = await mediator.Send(new DeleteCourseByIdCommand(id));
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 0);
            return group;
        }
    }
}
