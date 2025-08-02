
namespace CourseSales.Shared.Services
{
    public class IdentityServiceFake:IIdentityService
    {
        public Guid GetUserId => Guid.Parse("edae7720-1651-4867-9e18-14b7ebb50c52");
        public string UserName => "Burak Yasin";

        public List<string> Roles => [];
    }
}
