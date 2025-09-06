using CourseSales.Bus.Command;
using CourseSales.Bus.Events;
using MassTransit;
using Microsoft.Extensions.FileProviders;

namespace CourseSales.File.Api.Consumers
{
    public class UploadCoursePictureCommandConsumer(IServiceProvider serviceProvider) : IConsumer<UploadCoursePictureCommand>
    {
        public async Task Consume(ConsumeContext<UploadCoursePictureCommand> context)
        {  using (var scope = serviceProvider.CreateScope())
            {
                var fileProvider = serviceProvider.GetRequiredService<IFileProvider>();
                var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(context.Message.FileName)}";
                var upLoadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);
               await System.IO.File.WriteAllBytesAsync(upLoadPath, context.Message.Picture);
                var publishEndpoint = serviceProvider.GetRequiredService<IPublishEndpoint>();

                await publishEndpoint.Publish(new CoursePictureUploadedEvent(context.Message.CourseId, $"/files/{newFileName}"));

            }


            
        }
    }
}
