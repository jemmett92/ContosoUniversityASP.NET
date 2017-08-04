using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Data.Models
{
    public class Department
    {
        [Key]
        public int DeptID { get; set; }
        public string Title { get; set; }
    }
}
