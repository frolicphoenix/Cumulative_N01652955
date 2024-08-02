using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative_N01652955.Models
{
    public class Student
    {

        //class which describes what a student is

        public int StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public string StudentNumber { get; set; }
        public DateTime EnrolDate { get; set; }

    }
}