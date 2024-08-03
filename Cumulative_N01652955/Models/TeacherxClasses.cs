using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative_N01652955.Models
{
    public class TeacherxClasses
    {
        //class which describes what a teacher is
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }

        //what classes does a teacher teach?
        public int ClassId { get; set; }
        public string ClassName { get; set; }

    }
}