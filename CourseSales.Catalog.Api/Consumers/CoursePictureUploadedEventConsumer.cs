using CourseSales.Bus.Events;

namespace CourseSales.Catalog.Api.Consumers
{
    public class CoursePictureUploadedEventConsumer(IServiceProvider serviceProvider) : IConsumer<CoursePictureUploadedEvent>
    {
        public async Task Consume(ConsumeContext<CoursePictureUploadedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var course = dbContext.Courses.FirstOrDefault(c => c.Id == context.Message.CourseId);
            if (course == null) throw new Exception("Course not found");

            course.ImageUrl = context.Message.ImageUrl;
            dbContext.Courses.Update(course);
            await dbContext.SaveChangesAsync();

        }
    }
}
