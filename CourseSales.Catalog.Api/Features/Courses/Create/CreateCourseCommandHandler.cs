using CourseSales.Bus.Command;
using CourseSales.Shared.Services;

namespace CourseSales.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper,IPublishEndpoint publishEndpoint,IIdentityService identityService) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {

            var hasCategory = context.Categories.Any(x => x.Id == request.CategoryId);
            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("İlgili kategori bulunmamaktadır",
                    $"{request.CategoryId}'li kategori bulunmamaktadır",
                    HttpStatusCode.NotFound);

            }



            var newCourse = mapper.Map<Course>(request);
            newCourse.Feature = new Feature()
            {
                Duration = 12,//ilerleyen zamanda hesaplanacak
                EducatorFullName = "burak yasin",//tokenden okunacak
                Rating = 5//başlangıç 0
            };

            newCourse.Created = DateTime.Now;
            newCourse.Id = Guid.CreateVersion7();
            newCourse.UserId = identityService.GetUserId;
            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);
            if(request.Picture is not null)
            {
                using var stream = new MemoryStream();
               await request.Picture.CopyToAsync(stream, cancellationToken);
                var PictureAsByteArray= stream.ToArray();
                UploadCoursePictureCommand uploadCoursePictureCommand = new(newCourse.Id, PictureAsByteArray,request.Picture.FileName);
                await publishEndpoint.Publish(uploadCoursePictureCommand, cancellationToken);
            }
            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.CategoryId}");

        }
    }
}
