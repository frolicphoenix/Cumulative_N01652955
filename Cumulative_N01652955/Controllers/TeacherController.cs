using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cumulative_N01652955.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/List -> a webpage that shows a list of teachers
        public ActionResult List()
        {
            // Views/Teacher/List.cshtml
            return View();
        }

        //GET: Teacher/Show/{id} -> a webpage that shows an individual teacher's details
        public ActionResult Show()
        {
            // Views/Teacher/Show.cshtml
            return View();
        }
    }
}