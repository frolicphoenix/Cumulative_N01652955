using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Runtime.CompilerServices;
using Cumulative_N01652955.Models;
using MySql.Data.MySqlClient;

namespace Cumulative_N01652955.Controllers
{
    public class StudentDataController : ApiController
    {
        // Calling database context class -> Models/SchoolDbContext.cs
        private SchoolDbContext School = new SchoolDbContext();

        // GET: api/TeacherData/ListTeachers
        /// <summary>
        /// Returns a list of teachers
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers -> {["Teacher 1","Teacher 2","Teacher 3"]}</example>
        /// <returns>A list of teachers in the database</returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public List<Teacher> ListTeachers(string searchKey)
        {
            Debug.WriteLine("the api is getting the list of teachers..." + searchKey);

            //create a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection
            Connection.Open();

            //create sql command
            string query = "select *, DATE(hiredate) AS DateOnly from teachers WHERE teacherfname LIKE @key OR teacherlname LIKE @key OR hiredate LIKE @key OR salary LIKE @key;";

            //setting the sql query as command text
            MySqlCommand Command = Connection.CreateCommand();

            //defining @key value
            Command.Parameters.AddWithValue("@key", "%"+searchKey+"%");
            Command.Prepare();

            Command.CommandText = query;

            //execute the query
            MySqlDataReader ResultSet = Command.ExecuteReader();

            //gather information from the result set
            List<Teacher> teachers = new List<Teacher>();

            //loop through the results and add to the list
            while ( (ResultSet.Read()))
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal salary = Convert.ToDecimal(ResultSet["salary"]);

                //createing a new teacher object
                Teacher NewTeacher = new Teacher();

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFName = TeacherFName;
                NewTeacher.TeacherLName = TeacherLName;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;

                //add the teacher to the list
                teachers.Add(NewTeacher);
            }

            //close the connection
            Connection.Close();

            //return the result set
            return teachers;
    }
}
