using System.Diagnostics;
using System.Linq;
using BusinessLayer;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using PresentationLayer.ViewModels;

namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Student> _student;

        public IUnitOfWork<Course> _courses { get; }

        public HomeController(IUnitOfWork<Student> student , IUnitOfWork<Course> courses)
        {
            _student = student;
            _courses = courses;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Student = _student.Entity.GetAll().First(),
                Courses = _courses.Entity.GetAll().ToList()
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
