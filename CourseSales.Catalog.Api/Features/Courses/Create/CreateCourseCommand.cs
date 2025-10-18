namespace CourseSales.Catalog.Api.Features.Courses.Create
{
    public record CreateCourseCommand
        : IRequestByServiceResult<Guid>
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public decimal Price { get; init; }
        public IFormFile? Picture { get; init; }
        public Guid CategoryId { get; init; }
    }



}
