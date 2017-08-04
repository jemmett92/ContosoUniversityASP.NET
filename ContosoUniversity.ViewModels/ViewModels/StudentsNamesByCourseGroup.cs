﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.ViewModels.ViewModels
{
    public class StudentsNamesByCourseGroup
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public string CourseNames { get; set; }
        public virtual IEnumerable<EnrollmentVM> Enrollments { get; set; }
    }
}
