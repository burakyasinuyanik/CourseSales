using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Bus.Command
{
    public record class UploadCoursePictureCommand
        (
        Guid CourseId,
        byte[] Picture ,
        string FileName
    );

}
