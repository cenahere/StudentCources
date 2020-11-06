using Microsoft.AspNetCore.Http;
using System;

namespace PresentationLayer.ViewModels
{
    public class CoursesViewModel
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string CourseItem { get; set; }
        public IFormFile File { get; set; }
    }
}
