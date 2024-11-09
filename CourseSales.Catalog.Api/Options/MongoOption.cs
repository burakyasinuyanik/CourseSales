﻿using System.ComponentModel.DataAnnotations;

namespace CourseSales.Catalog.Api.Options
{
    public class MongoOption
    {
       [Required] public string DataBase { get; set; } = default!;
        [Required] public string ConnectionString { get; set; } = default!;
    }
}
