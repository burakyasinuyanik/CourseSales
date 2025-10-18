using CourseSales.Web.Options;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CourseSales.Web.TagHelpers
{
    public class CourseThumbnailPictureTagHelper(MicroServiceOption microServiceOption) : TagHelper
    {
        public string? Src { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            var blankThumbnail = "/images/course-thumbnail-blank.jpg";
            
            if(string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankThumbnail);
            }else
            {
                var pictureUrl = $"{microServiceOption.File.BaseUrl}/{Src}";
                output.Attributes.SetAttribute("src", pictureUrl);
            }
            output.Attributes.SetAttribute("class", "card-img-top");
            output.Attributes.SetAttribute("style", "height:160px;object-fit:cover;");
            
        }
    }
}
