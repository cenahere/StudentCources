using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataAccessLayer;
using PresentationLayer.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BusinessLayer.Interfaces;

namespace PresentationLayer.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IHostingEnvironment _hosting;
        private readonly IUnitOfWork<Course> _courses;

        public CoursesController(IHostingEnvironment hosting , IUnitOfWork<Course> courses)
        {
            // للوصول لملفات الاستضافة
            _hosting = hosting;
            _courses = courses;
        }

        // GET: Courses
        public IActionResult Index()
        {
            return View(_courses.Entity.GetAll());
        }

        // GET: Courses/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courses.Entity.GetById(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoursesViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\Course");
                    string fullPah = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPah, FileMode.Create));
                }
                Course course = new Course
                {
                    CourseName = model.CourseName,
                    Description = model.Description,
                    CourseItem = model.File.FileName
                };
                _courses.Entity.Add(course);
                _courses.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courses.Entity.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            CoursesViewModel coursesViewModel = new CoursesViewModel
            {
                Id = course.Id,
                Description = course.Description,
                CourseItem = course.CourseItem,
                CourseName = course.CourseName

            };


            return View(coursesViewModel);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,CoursesViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\Course");
                        string fullPah = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPah, FileMode.Create));
                    }
                    Course course = new Course
                    {
                        Id=model.Id,
                        CourseName = model.CourseName,
                        Description = model.Description,
                        CourseItem = model.File.FileName
                    };
                    _courses.Entity.Update(course);
                    _courses.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Courses/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = _courses.Entity.GetById(id);
            if (course == null)
            {
                return NotFound();
            }


            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _courses.Entity.Delete(id);
            _courses.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return _courses.Entity.GetAll().Any(x => x.Id == id);
        }
    }
}
