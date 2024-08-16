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

        //GOAL: To find a spcific teacher from the database using teacherid
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
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {
            //SQL command
            string query = "SELECT * FROM teachers WHERE teacherid =" + TeacherId;


            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();

            MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = query;

            MySqlDataReader Reader = Command.ExecuteReader();

            Teacher teachers = new Teacher();
            //result set of 1 
            while (Reader.Read())
            {
                teachers.TeacherId = Convert.ToInt32(Reader["teacherid"]);
                teachers.TeacherFName = Reader["teacherfname"].ToString();
                teachers.TeacherLName = Reader["teacherlname"].ToString();
                teachers.EmployeeNumber = Reader["employeenumber"].ToString();
                teachers.HireDate = Convert.ToDateTime(Reader["hiredate"]);
                teachers.Salary = Convert.ToDecimal(Reader["salary"]);
                
            }
            Connection.Close();

            return teachers;

        }

        /// <summary>
        /// Recieve new teacher information and add it to the database
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// POST: api/TeacherData/CreateTeacher -> null
        /// POST: CONTENT / REQUEST BODY:
        /// {
        ///     "TeacherFName":"John",
        ///     "TeacherLName":"Doe",
        ///     "EmployeeNumber":"T123",
        ///     "HireDate":"2020-04-06",
        ///     "Salary":"55.56"
        /// } -> null
        /// </example>
        /// <example>
        /// curl -d @teacher.json "{\"TeacherFName\":\"John\",\"TeacherLName\":\"Doe\",\"EmployeeNumber\":\"T123\",\"HireDate\":\"2020-04-06\",\"Salary\":\"55.56\"}" -H "Content-Type: application/json"
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/CreateTeacher")]
        public void CreateTeacher([FromBody]Teacher NewTeacher)
        {
            //create a connection
            MySqlConnection Connection = School.AccessDatabase();

            //open database
            Connection.Open();

            //create sql command
            string query = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate,salary) VALUES (@FName, @LName, @ENumber, @HDate, @Slry)";

            //set command text
            MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = query;

            Command.Parameters.AddWithValue("@FName", NewTeacher.TeacherFName);
            Command.Parameters.AddWithValue("@LName", NewTeacher.TeacherLName);
            Command.Parameters.AddWithValue("@ENumber", NewTeacher.EmployeeNumber);
            Command.Parameters.AddWithValue("@HDate", NewTeacher.HireDate);
            Command.Parameters.AddWithValue("@Slry", NewTeacher.Salary);

            Command.Prepare();
            //execute the query
            Command.ExecuteNonQuery();
            //add the new teacher to the database

            //close the connection
            Connection.Close();

            /*return "the new teacher information is "+NewTeacher.TeacherFName+" and "+NewTeacher.EmployeeNumber+"";*/
        }

        /// <summary>
        /// This method receives teacher information and updates the teacher information in the database
        /// </summary>
        /// <param name="TeacherId"> the primary key of "teachers" table
        /// </param>
        /// <returns>
        /// </returns>
        /// <example>
        /// POST api/TeacherData/DeleteTeacher/1 -> null
        /// </example>
        /// <example>
        /// curl -d @teacher.json "{\"TeacherFName\":\"John\",\"TeacherLName\":\"Doe\",\"EmployeeNumber\":\"T123\",\"HireDate\":\"2020-04-06\",\"Salary\":\"55.56\"}" -H "Content-Type: application/json"
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/DeleteTeacher/{TeacherId}")]
        public void DeleteTeacher(int TeacherId)
        {
            
                //create a connection
                MySqlConnection Connection = School.AccessDatabase();

                //open database
                Connection.Open();

                //create sql command
                string query = "delete from teachers where teacherid=@id";

                //set command text
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = query;

                Command.Parameters.AddWithValue("@id", TeacherId);
                Command.Prepare();

                //execute the query
                Command.ExecuteNonQuery();
                //add the new teacher to the database
            
                 //close the connection
                 Connection.Close();

           
        }

        /// <summary>
        /// Receives teachers information and updates it in the database
        /// </summary>
        /// <param name="TeacherId"> teacher id to update </param>
        /// <param name="TeacherInfo"> the information of the individual teacher </param>
        /// <example>
        /// POST: api/TeacherData/UpdateTeacher/{teacherid}
        /// FORM DATA: 
        /// {
        ///      "TeacherFName":"Jane",
        ///      "TeacherLName":"Doe",
        ///      "EmployeeNumber":"A123",
        ///      "HireDate":""10/11/2024",
        ///      "Salary":"200"
        /// }
        /// </example>
        [Route("api/TeacherData/UpdateTeacher/{TeacherId}")]
        [HttpPost]
        public void UpdateTeacher( int TeacherId, [FromBody]Teacher TeacherInfo)
        {

            MySqlConnection Connection = School.AccessDatabase();
            Connection.Open();

            string query = "UPDATE teachers SET TeacherFName=@fname, TeacherLName=@lname WHERE teacherid=@id";
            MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = query;

            Command.Parameters.AddWithValue("@id", TeacherId);
            Command.Parameters.AddWithValue("@fname", TeacherInfo.TeacherFName);
            Command.Parameters.AddWithValue("@lname", TeacherInfo.TeacherLName);

            Command.Prepare();


            Command.ExecuteNonQuery();
            Connection.Close();

        }

    }
}
