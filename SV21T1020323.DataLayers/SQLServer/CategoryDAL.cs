using Dapper;
using SV21T1020323.DomainModels;
using System.Data;
using System.Diagnostics;

namespace SV21T1020323.DataLayers.SQLServer
{
    public class CategoryDAL : _BaseDAL, ICommonDAL<Category>
    {
        public CategoryDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Category data)
        {
            int id = 0;

            using (var connection = OpenConnection())
            {
                var sql = @"INSERT INTO Categories(CategoryName, Description)
                            VALUES(@CategoryName, @Description);
                            SELECT @@IDENTITY";
                var parameters = new
                {
                    CategoryName = data.CategoryName ?? "",
                    Description = data.Description ?? "",
                };
                connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                            SELECT COUNT(*)
                            FROM Categories
                            WHERE (CategoryName like @searchValue)
                            ";
                var parameters = new
                {
                    searchValue = $"%{searchValue}%",
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"DELETE FROM Categories WHERE CategoryId = @CategoryId";
                var parameters = new
                {
                    CategoryId = id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Category? Get(int id)
        {
            Category? data = null;

            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM Categories WHERE CategoryId = @CategoryId";
                var parameters = new
                {
                    CategoryId = id 
                };
                data = connection.QueryFirstOrDefault<Category>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool InUsed(int id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"IF EXISTS(SELECT * FROM Products WHERE CategoryId = @CategoryId)
                                SELECT 1
                            ELSE
                                SELECT 0";
                var parameters = new
                {
                    CategoryId = id
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public IList<Category> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Category> data = new List<Category>();
            using (var connection = OpenConnection())
            {
                var sql = @"
                            SELECT *
                            FROM
                            (
	                            SELECT *, ROW_NUMBER() OVER (ORDER BY CategoryName) AS RowNumber
	                            FROM Categories
	                            WHERE CategoryName like @searchValue
                            ) AS t
                            WHERE	@pageSize = 0
		                            OR RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize
                            ORDER BY RowNumber
                            ";
                var parameters = new
                {
                    page,
                    pageSize,
                    searchValue = $"%{searchValue}%"
                };
                data = connection.Query<Category>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public bool Update(Category data)
        {
            bool result = false;

            using (var connection = OpenConnection())
            {
                var sql = @"UPDATE Categories
                            SET CategoryName = @CategoryName,
                                Description = @Description
                            WHERE CategoryId = @CategoryId
                            ";
                var parameters = new
                {
                    CategoryId = data.CategoryID,
                    CategoryName = data.CategoryName ?? "",
                    Description = data.Description ?? ""
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
