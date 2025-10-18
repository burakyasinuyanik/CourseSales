namespace CourseSales.Web.Options
{
    public class MicroServiceOption
    {
        public required MicroServiceOptionItem Catalog { get; set; }
        public required MicroServiceOptionItem File { get; set; }
    }
    public class MicroServiceOptionItem
    {
        public required string BaseUrl { get; set; }
       
    }
}
