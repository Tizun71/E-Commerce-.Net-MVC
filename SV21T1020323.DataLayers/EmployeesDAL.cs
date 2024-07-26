using Dapper;
using Microsoft.Data.SqlClient;
using SV21T1020323.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DataLayers
{
    public class EmployeesDAL
    {
        private string connectionString = "";
        public EmployeesDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Get All Employees Data.
        /// Query database and get data return to the function
        /// </summary>
        /// <returns></returns>
        public List<Employees> List()
        {
            List<Employees> list = new List<Employees>();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var sql = @"SELECT * FROM Employees";
                list = connection.Query<Employees>(sql: sql, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return list;
        }
    }
}
