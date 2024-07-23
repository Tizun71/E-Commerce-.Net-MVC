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
    public class CategoriesDAL
    {
        private string connectionString = "";
        public CategoriesDAL(string connectionString) {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Get All Categories Data.
        /// Query database and get data return to the function
        /// </summary>
        /// <returns></returns>
        public List<Categories> List()
        {
            List<Categories> list = new List<Categories>();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var sql = @"SELECT * FROM Categories";
                list = connection.Query<Categories>(sql: sql, commandType:System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return list;
        }
    }
}
