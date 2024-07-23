using Dapper;
using Microsoft.Data.SqlClient;
using SV21T1020323.DomainModels;


namespace SV21T1020323.DataLayers
{
    public class SuppliersDAL
    {
        private string connectionString = "";
        public SuppliersDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        /// <summary>
        /// Get All Suppliers Data.
        /// Query database and get data return to the function
        /// </summary>
        /// <returns></returns>
        public List<Suppliers> List()
        {
            List<Suppliers> list = new List<Suppliers>();
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var sql = @"SELECT * FROM Suppliers";
                list = connection.Query<Suppliers>(sql: sql, commandType: System.Data.CommandType.Text).ToList();
                connection.Close();
            }
            return list;
        }
    }
}
