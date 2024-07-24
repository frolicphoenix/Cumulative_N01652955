using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Runtime.CompilerServices;
using Cumulative_N01652955.Models;
using MySql.Data.MySqlClient;

namespace Cumulative_N01652955.Controllers
{
    public class TeacherDataController : ApiController
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
        public List<Teacher> ListTeachers()
        {
            Debug.WriteLine("the api is getting the list of teachers...");

            //create a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection
            Connection.Open();

            //create sql command
            string query = "select * from teachers";

            //setting the sql query as command text
            MySqlCommand Command = Connection.CreateCommand();
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

        /*//GOAL: To find a spcific teacher from the database using teacherid
        /// <summary>
        /// Receive a teacher id and provide the information of the teacher
        /// </summary>
        /// <param name="id">
        /// Primary key of teacher in the database (teacherid)
        /// </param>
        /// <returns>
        /// GET api/TeacherData/FindTeacher/{id} -> {Teacher Object}
        /// </returns>
        /// <example>
        /// GET api/TeacherData/FindTeacher/4 -> {"TeacherId":"1","TeacherFName":"Alexander",
        ///                                       "TeacherLName":"Bennett","EmployeeNumber":"T456",
        ///                                       "HireDate":"2020-04-06","salary":"55.56"}
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {

            Debug.WriteLine("the api is finding the teacher with id = " + id);

            //create a connection
            MySqlConnection Connection = School.AccessDatabase();

            //Open the connection
            Connection.Open();

            //create sql command
            string query = "SELECT * FROM teachers WHERE teacherid= " + id;

            //setting the sql query as command text
            MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = query;

            //execute the query
            MySqlDataReader ResultSet = Command.ExecuteReader();

            //gather information from the result set
            Teacher SelectedTeacher = new Teacher();

            //loop through the results and add to the list
            while ((ResultSet.Read()))
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                SelectedTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
            }

            //close the connection
            Connection.Close();

            //return the result set
            return SelectedTeacher;

        }*/

    }
}
