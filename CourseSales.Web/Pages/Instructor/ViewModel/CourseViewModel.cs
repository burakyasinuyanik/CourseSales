﻿namespace CourseSales.Web.Pages.Instructor.ViewModel
{
    public record class CourseViewModel(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string ImageUrl,
        string CategoryName,
        int Duration,
        float Rating)
    {
        public string TruncateDescription(int maxLength)
        {
            if (Description.Length <= maxLength) return Description;
            return Description.Substring(0, maxLength) + "...";
        }
    }
}
