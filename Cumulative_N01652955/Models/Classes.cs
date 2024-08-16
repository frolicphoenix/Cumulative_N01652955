using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cumulative_N01652955.Models
{
    public class Classes
    {

        //class which describes all classes

        public int ClassId { get; set; }
        public string ClassCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string ClassName { get; set; }

        //FK TeacherId
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

    }
}