namespace CourseSales.Web.Options
{
    public class IdentityOption
    {
        public IdentityOptionsItem Admin { get; set; } = null!;
        public IdentityOptionsItem Web { get; set; } = null!;
    }
    public class IdentityOptionsItem
    {
        public required string Address { get; set; }
        public required string BaseAddress { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}
