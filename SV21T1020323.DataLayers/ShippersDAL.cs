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
    public class ShippersDAL
    {
        private string connectionString = "";
        public ShippersDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Get All Shippers Data.
        /// Query database and get data return to the function
        /// </summary>
        /// <returns></returns>
        public List<Shippers> List()
        {
            List<Shippers> list = new List<Shippers>();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var sql = @"SELECT * FROM Shippers";
                list = connection.Query<Shippers>(sql: sql, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return list;
        }
    }
}
