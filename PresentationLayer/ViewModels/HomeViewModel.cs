using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class HomeViewModel
    {
        public Student  Student { get; set; }
        public IList<Course> Courses { get; set; }
    }
}
