namespace CourseSales.Web.Pages.Instructor.Dto
{
    public record CourseDto(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        Guid UserId,
        CategoryDto Category,
        FeatureDto Feature);

}
