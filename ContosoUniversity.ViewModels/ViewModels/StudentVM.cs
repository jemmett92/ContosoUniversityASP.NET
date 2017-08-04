using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels.ViewModels
{
    public class StudentVM
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public IEnumerable<Grade?> Grade { get; set; }
        public IEnumerable<string> CourseNames { get; set; }
        public virtual IEnumerable<EnrollmentVM> Enrollments { get; set; }
       

    }
}
