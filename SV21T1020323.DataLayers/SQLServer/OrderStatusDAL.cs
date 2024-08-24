using Dapper;
using SV21T1020323.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DataLayers.SQLServer
{
    public class OrderStatusDAL : _BaseDAL, ICommonDAL<OrderStatus>
    {
        public OrderStatusDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(OrderStatus data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public OrderStatus? Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailValid(OrderStatus data)
        {
            throw new NotImplementedException();
        }

        public IList<OrderStatus> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<OrderStatus> data = new List<OrderStatus>();
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT * FROM OrderStatus";
                data = connection.Query<OrderStatus>(sql: sql, commandType: CommandType.Text).ToList();
                connection.Close();
            }
            return data;
        }

        public bool Update(OrderStatus data)
        {
            throw new NotImplementedException();
        }
    }
}
