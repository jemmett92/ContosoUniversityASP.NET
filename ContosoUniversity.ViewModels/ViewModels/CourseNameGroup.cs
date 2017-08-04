using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.ViewModels
{
    public class CourseNameGroup
    {        
        public string Title { get; set; }
        public int StudentCount { get; set; }
        public int CourseId { get; set; }

    }
}