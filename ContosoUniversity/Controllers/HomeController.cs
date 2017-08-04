using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.ViewModels;
using ContosoUniversity.ViewModels.ViewModels;
using ContosoUniversity.Business.Contracts.Contracts;
using ContosoUniversity.Business.Services;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        //private IStudentDBAccess sdba = new StudentDBAccess();

        private readonly IStudentDBAccess Istudent;

        public HomeController(IStudentDBAccess isdba)
        {
            this.Istudent = isdba;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDept([Bind(Include = "DeptID, Title")]DepartmentNameGroup d)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Istudent.AddDept(d);
                    return RedirectToAction("Departments");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(d);
        }

        public ActionResult CreateDept()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInstructor([Bind(Include = "LastName, CourseTitle")]InstructorNameGroup i)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    Istudent.AddInstructor(i);
                    return RedirectToAction("Instructors");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(i);
        }

        public ActionResult CreateInstructor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse([Bind(Include = "Title")]CourseNameGroup c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Istudent.AddCourse(c);
                    return RedirectToAction("Courses");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(c);
        }

        public ActionResult CreateCourse()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<EnrollmentDateGroup> data = Istudent.GetAbout();
            return View(data.ToList());
        }

        public ActionResult Courses()
        {
            var courses = Istudent.GetCourses();
            return View(courses.ToList());
        }

        public ActionResult Instructors()
        {

            var instructors = Istudent.GetInstructors();
            return View(instructors.ToList());
        }

        public ActionResult Departments()
        {
            var departments = Istudent.GetDepartments();
            return View(departments.ToList());
        }

        public ActionResult Names()
        {
            var courseStudentNames = Istudent.GetStudentNames();
            return View(courseStudentNames.ToList());
        }

        public ActionResult CourseStudents(string courseTitle)
        {         
            var coursestudents = Istudent.GetCourseStudents(courseTitle);
            return View(coursestudents.ToList());
        }

        public ActionResult AboutStats(DateTime? enrollmentDate)
        {
            var aboutstudents = Istudent.GetAboutStudents(enrollmentDate);
            return View(aboutstudents.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            Istudent.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}