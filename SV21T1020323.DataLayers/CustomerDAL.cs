using Dapper;
using Microsoft.Data.SqlClient;
using SV21T1020323.DomainModels;

namespace SV21T1020323.DataLayers
{
    public class CustomerDAL
    {
        private string connectionString = "";

        public CustomerDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Get All Customer Data.
        /// Query database and get data return to the function
        /// </summary>
        /// <returns></returns>
        public List<Customer> List()
        {
            List<Customer> list = new List<Customer>();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                var sql = @"SELECT * FROM Customers";
                list = connection.Query<Customer>(sql: sql, commandType: System.Data.CommandType.Text).ToList();

                connection.Close();
            }
            return list;
        }
    }
}

