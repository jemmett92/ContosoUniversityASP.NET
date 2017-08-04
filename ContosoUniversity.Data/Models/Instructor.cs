using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Models
{
    public class Instructor
    {
        [Key]
        public string LastName { get; set; }
        public string CourseTitle { get; set; }



    }
}
