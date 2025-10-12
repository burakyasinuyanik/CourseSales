namespace CourseSales.Web.Pages.Instructor.Dto
{
    public record class UpdateCourseRequest(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string? ImageUrl,
        Guid CategoryId);
    
}
