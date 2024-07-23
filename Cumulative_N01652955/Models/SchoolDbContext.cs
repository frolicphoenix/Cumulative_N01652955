using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;

namespace Cumulative_N01652955.Models
{
    public class SchoolDbContext
    {

        // readonly "secret" properties
        // only SchoolDbContext can use them
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        // connection string to connect to the database
        protected static string ConnectionString
        {
            get
            {
                // return the connection string
                return "server=" + Server
                    + ";user=" + User
                    + ";database=" + Database
                    + ";port=" + Port
                    + ";password=" + Password;
                    /*+ ";convert zero datetime = True";*/
            }
        }

        // method to get the connection to the database
        /// <summary>
        /// Returns a connections to the school database
        /// </summary>
        /// <returns> A MySqlConnection Object </returns>
        /// <example>
        /// private SchoolDbContext School = new SchoolDbContext();
        /// MySqlConnection Conn = School.AccessDatabase();
        /// </example>
        public MySqlConnection AccessDatabase()
        {
            // instantiate the class MySqlConnection
            return new MySqlConnection(ConnectionString);
        }


    }
}