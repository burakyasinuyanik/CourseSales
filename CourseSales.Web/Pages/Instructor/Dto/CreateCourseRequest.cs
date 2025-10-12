namespace CourseSales.Web.Pages.Instructor.Dto
{
    public record class CreateCourseRequest(
            string Name,
            string Description,
            decimal Price,
            IFormFile? Picture,
            Guid CategoryId)
    {
    }
}
