using Cumulative_N01652955.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            //getting teacher data from the controller
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();

            // Views/Teacher/List.cshtml
            return View(Teachers);
        }

        //GET: Teacher/Show/{id} -> a webpage that shows an individual teacher's details
        public ActionResult Show(int id)
        {
            //getting teacher data from the controller
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            // Views/Teacher/Show.cshtml
            return View(SelectedTeacher);
        }

        //GET: Teacher/Create -> a webpage that will allow adding a new teacher
        public ActionResult Create()
        {
            // Views/Teacher/Create.cshtml
            return View();
        }

        //POST: Teacher/New(string TeacherFName, string TeacherLName, string EmployeeNumber, DateTime HireDate, decimal Salary) -> a webpage that will allow adding a new teacher
        [HttpPost]
        public ActionResult New(string TeacherFName, string TeacherLName, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            //debugging 
            Debug.WriteLine("Received teacherfname: " + TeacherFName);
            Debug.WriteLine("Received teacherlname: " + TeacherLName);
            Debug.WriteLine("Received employeenumber: " + EmployeeNumber);
            Debug.WriteLine("Received hiredate: " + HireDate);
            Debug.WriteLine("Received salary: " + Salary);

            //add the new teacher information to the database
            TeacherDataController controller = new TeacherDataController();

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFName = TeacherFName;
            NewTeacher.TeacherLName = TeacherLName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            //CREATE TEACHER
            controller.CreateTeacher(NewTeacher);

            //redirect to the list of teachers
            return RedirectToAction("List");
        }

        //GET: Teacher/DeleteConfirm/{id} -> a webpage that shows a form to confirm the deletion of a teacher
        public ActionResult DeleteConfirm(int id)
        {
            //getting teacher data from the controller
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            // Views/Teacher/DeleteConfirm.cshtml
            return View(SelectedTeacher);
        }

        //POST: Teacher/Delete/{id} -> a webpage that will delete a teacher and redirects to /Teacher/List
        public ActionResult Delete(int id)
        {
            //getting teacher data from the controller
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            //redirect to the list of teachers
            return RedirectToAction("List");
        }

    }
}