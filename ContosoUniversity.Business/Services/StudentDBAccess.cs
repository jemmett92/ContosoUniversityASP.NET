using ContosoUniversity.Business;
using ContosoUniversity.Business.Contracts.Contracts;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.Data.Models;
using ContosoUniversity.ViewModels;


namespace ContosoUniversity.Business.Services
{
    public class StudentDBAccess : IStudentDBAccess
    {
        private SchoolContext db = new SchoolContext();

        public IQueryable<StudentVM> GetStudents()
        {
            var StudentSet = db.Students.ToList();
            var students = StudentSet.Select(x => new StudentVM
            {
                ID = x.ID,
                EnrollmentDate = x.EnrollmentDate,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,
                CourseNames = x.Enrollments.Select(y => y.Course.Title)
            });
            return students.AsQueryable();
        }
    

        public void Delete(int id)
        {
            // Model Find Student
            var student = db.Students.FirstOrDefault(x => x.ID == id);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }

        public StudentVM FindStudents(int? id)
        {
            // Model Find Student
            var student = db.Students.FirstOrDefault(x => x.ID == id);
            return new StudentVM
            {
                ID = student.ID,
                EnrollmentDate = student.EnrollmentDate,
                FirstMidName = student.FirstMidName,
                LastName = student.LastName,
                CourseNames = student.Enrollments.Select(y => y.Course.Title),
                Grade = student.Enrollments.Select(z => z.Grade),
                Enrollments = student.Enrollments.Select(q => new EnrollmentVM
                {
                    CourseNames = q.Course.Title,
                    Grade = q.Grade.GetValueOrDefault().ToString()
                })
            };
        }

        public IQueryable<StudentsNamesByCourseGroup> GetStudentNames()
        {
            var StudentSet = db.Students.ToList();
            var courseStudents = StudentSet.Select(x => new StudentsNamesByCourseGroup
            {
                ID = x.ID,
                FirstMidName = x.FirstMidName,
                LastName = x.LastName,
                //CourseNames = x.Enrollments.Select(y => y.Course.Title),
                Enrollments = x.Enrollments.Select(y => new EnrollmentVM
                {
                    CourseNames = y.Course.Title
                })
            });
            return courseStudents.AsQueryable();
        }
        public IQueryable<EnrollmentDateGroup> GetCourseStudents(string courseTitle)
        {
            //enrollemts where course.title == course. select(x. player)
            //stuents.select 
            var StudentCourses = db.Enrollments.Where(x => x.Course.Title == courseTitle).Select(x => new EnrollmentDateGroup
            {
                Firstname = x.Student.FirstMidName,
                LastName = x.Student.LastName,
                CourseName = courseTitle
            });
            return StudentCourses;
        }

        public IQueryable<EnrollmentDateGroup> GetAboutStudents(DateTime? enrollmentDate)
        {
            var aboutstats = db.Students.Where(x => x.EnrollmentDate == enrollmentDate).Select(x => new EnrollmentDateGroup
            {
                Firstname = x.FirstMidName,
                LastName = x.LastName,
                EnrollmentDate = enrollmentDate
            });
            return aboutstats;
        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void AddStudent(StudentVM student)
        {
            Student s = new Student
            {
                ID = student.ID,
                LastName = student.LastName,
                FirstMidName = student.FirstMidName,
                EnrollmentDate = student.EnrollmentDate
            };
            db.Students.Add(s);
            db.SaveChanges();
        }

        public void AddDept(DepartmentNameGroup Department)
        {
            Department s = new Department
            {
                DeptID = Department.DeptID,
                Title = Department.Title,
            };
            db.Departments.Add(s);
            db.SaveChanges();
        }

        public void AddInstructor(InstructorNameGroup Instructor)
        {
            Instructor s = new Instructor
            {
                LastName = Instructor.LastName,
                CourseTitle = Instructor.CourseTitle,
            };
            db.Instructor.Add(s);
            db.SaveChanges();
        }

        public void AddCourse(CourseNameGroup Course)
        {
            Course s = new Course
            {
                Title = Course.Title              
            };
            db.Courses.Add(s);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IQueryable<InstructorNameGroup> GetInstructors()
        {
            var instructors = db.Instructor.GroupBy(x => new { x.CourseTitle }).Select(x => new InstructorNameGroup
            {
                LastName = x.Select(y => y.LastName).FirstOrDefault(),
                CourseTitle = x.Select(y => y.CourseTitle).FirstOrDefault()
            });
            return instructors;
        }

        public IQueryable<CourseNameGroup> GetCourses()
        {
            var courses = db.Enrollments.GroupBy(x => new { x.CourseID }).Select(x => new CourseNameGroup
            {
                StudentCount = x.Count(),
                Title = x.Select(y => y.Course.Title).FirstOrDefault(),
                
            });
            return courses;
        }

        public IQueryable<DepartmentNameGroup> GetDepartments()
        {
            var departments = db.Departments.GroupBy(x => new { x.DeptID }).Select(x => new DepartmentNameGroup
            {
                DeptID = x.Select(y => y.DeptID).FirstOrDefault(),
                Title = x.Select(y => y.Title).FirstOrDefault()
            });
            return departments;
        }

        public IQueryable<EnrollmentDateGroup> GetAbout()
        {
            IQueryable<EnrollmentDateGroup> about = from student in db.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };
            return about;
        }
       
    }
}
