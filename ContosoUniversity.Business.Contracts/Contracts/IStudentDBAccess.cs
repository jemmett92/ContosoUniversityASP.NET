using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.ViewModels.ViewModels;

namespace ContosoUniversity.Business.Contracts.Contracts
{
    public interface IStudentDBAccess
    {
        IQueryable<EnrollmentDateGroup> GetAboutStudents(DateTime? enrollmentDate);
        IQueryable<StudentsNamesByCourseGroup> GetStudentNames();
        IQueryable<EnrollmentDateGroup> GetCourseStudents(string coursetitle);
        IQueryable<StudentVM> GetStudents();
        IQueryable<InstructorNameGroup> GetInstructors();
        IQueryable<CourseNameGroup> GetCourses();
        IQueryable<DepartmentNameGroup> GetDepartments();
        IQueryable<EnrollmentDateGroup> GetAbout();
        void Delete(int id);
        StudentVM FindStudents(int? id);
        void SaveChanges();
        void AddStudent(StudentVM student);
        void AddDept(DepartmentNameGroup department);
        void Dispose();
        void AddInstructor(InstructorNameGroup instructor);
        void AddCourse(CourseNameGroup course);
    }
}
