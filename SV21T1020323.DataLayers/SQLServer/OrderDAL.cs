using Azure;
using Dapper;
using SV21T1020323.DomainModels;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SV21T1020323.DataLayers.SQLServer
{
    public class OrderDAL : _BaseDAL, IOrderDAL
    {
        public OrderDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Order data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"INSERT INTO Orders(CustomerId, OrderTime, DeliveryProvince, DeliveryAddress, EmployeeID, Status)
                            VALUES(@CustomerID, GETDATE(), @DeliveryProvince, @DeliveryAddress, @EmployeeID, @Status);
                            SELECT @@IDENTITY";
                var parameters = new
                {
                    CustomerID = data.CustomerID,
                    DeliveryProvince = data.DeliveryProvince ?? "",
                    DeliveryAddress = data.DeliveryAddress ?? "",
                    EmployeeID = data.EmployeeID,
                    Status = data.Status,
                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public int Count(int status = 0, DateTime? fromTime = null, DateTime? toTime = null, string searchValue = "")
        {
            int count = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT count(*)
                            FROM Orders as o
                            LEFT JOIN Customers as c on o.CustomerID = c.CustomerID
                            LEFT JOIN Employees as e on o.EmployeeID = e.EmployeeID
                            LEFT JOIN Shippers as s on o.ShipperID = s.ShipperID
                            WHERE   (@Status = 0 or o.Status = @Status)
                                AND (@FromTime is null or o.OrderTime >= @FromTime)
                                AND (@ToTime is null or o.OrderTime <= @ToTime)
                                AND (@SearchValue = N''
                                    OR c.CustomerName like @SearchValue
                                    OR e.FullName like @SearchValue
                                    OR s.ShipperName like @SearchValue)";
                var parameters = new
                {
                    Status = status,
                    FromTime = fromTime,
                    ToTime = toTime,
                    SearchValue = $"%{searchValue}%"
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int orderID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"DELETE FROM OrderDetails WHERE OrderID = @OrderID;
                            DELETE FROM Orders WHERE OrderID = @OrderID";
                var parameters = new
                {
                    OrderID = orderID,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool DeleteDetail(int orderID, int productID)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"DELETE FROM OrderDetails
                            WHERE OrderID = @OrderID AND ProductID = @ProductID";
                var parameters = new
                {
                    OrderID = orderID,
                    ProductID = productID,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Order? Get(int orderID)
        {
            Order? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT o.*,
                                    c.CustomerName,
                                    c.ContactName as CustomerContactName,
                                    c.Address as CustomerAddress,
                                    c.Phone as CustomerPhone,
                                    c.Email as CustomerEmail,
                                    e.FullName as EmployeeName,
                                    s.ShipperName,
                                    s.Phone as ShipperPhone
                            FROM Orders as o
                            LEFT JOIN Customers as c on o.CustomerID = c.CustomerID
                            LEFT JOIN Employees as e on o.EmployeeID = e.EmployeeID
                            LEFT JOIN Shippers as s on o.ShipperID = s.ShipperID
                            WHERE o.OrderID = @OrderID";
                var parameters = new
                {
                    OrderID = orderID,
                };
                data = connection.QueryFirstOrDefault<Order>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public OrderDetail? GetDetail(int orderID, int productID)
        {
            OrderDetail? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT od.*, p.ProductName, p.Photo, p.Unit
                            FROM OrderDetails as od
                            JOIN Products as p on od.ProductID = p.ProductID
                            WHERE od.OrderID = @OrderID and od.ProductID = @ProductID";
                var parameters = new
                {
                    OrderID = orderID,
                    ProductID = productID,
                };
                data = connection.QueryFirstOrDefault<OrderDetail>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public List<Order> List(int page = 1, int pageSize = 0, int status = 0, DateTime? fromTime = null, DateTime? toTime = null, string searchValue = "")
        {
            List<Order> list = new List<Order>();
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"WITH CTE AS
                            (
                                SELECT row_number() over(order by o.OrderTime desc) as RowNumber,
                                        o.*,
                                        c.CustomerName,
                                        c.ContactName as CustomerContactName,
                                        c.Address as CustomerAddress,
                                        c.Phone as CustomerPhone,
                                        c.Email as CustomerEmail,
                                        e.FullName as EmployeeName,
                                        s.ShipperName,
                                        s.Phone as ShipperPhone
                                FROM Orders as o
                                LEFT JOIN Customers as c on o.CustomerID = c.CustomerID
                                LEFT JOIN Employees as e on o.EmployeeID = e.EmployeeID
                                LEFT JOIN Shippers as s on o.ShipperID = s.ShipperID
                                WHERE   (@Status = 0 or o.Status = @Status)
                                    AND (@FromTime is null or o.OrderTime >= @FromTime)
                                    AND (@ToTime is null or o.OrderTime <= @ToTime)
                                    AND (@SearchValue = N''
                                        OR c.CustomerName like @SearchValue
                                        OR e.FullName like @SearchValue
                                        OR s.ShipperName like @SearchValue)
                            )
                            SELECT * FROM cte
                            WHERE (@PageSize = 0)
                                OR (RowNumber between (@Page - 1) * @PageSize + 1 and @Page * @PageSize)
                            ORDER BY RowNumber";
                var parameters = new
                {
                    Page = page,
                    PageSize = pageSize,
                    Status = status,
                    FromTime = fromTime,
                    ToTime = toTime,
                    SearchValue = $"%{searchValue}%"
                };

                list = connection.Query<Order>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
                connection.Close();

            }
            return list;
        }

        public IList<OrderDetail> ListDetails(int orderID)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            using (var connection = OpenConnection())
            {
                var sql = @"SELECT od.*, p.ProductName, p.Photo, p.Unit
                            FROM OrderDetails as od
                            JOIN Products as p on od.ProductID = p.ProductID
                            WHERE od.OrderID = @OrderID";
                var parameters = new
                {
                    OrderID = orderID,
                };

                list = connection.Query<OrderDetail>(sql: sql, param: parameters, commandType: CommandType.Text).ToList();
                connection.Close();

            }
            return list;
        }

        public bool SaveDetail(int orderID, int productID, int quantity, decimal salePrice)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"IF EXISTS (SELECT * FROM OrderDetails
                            WHERE OrderID = @OrderID and ProductID = @ProductID)
                            UPDATE OrderDetails
                            SET Quantity = @Quantity,
                                SalePrice = @SalePrice
                            WHERE OrderID = @OrderID AND ProductID = @ProductID
                            ELSE
                            INSERT INTO OrderDetails(OrderID, ProductID, Quantity, SalePrice)
                            VALUES(@OrderID, @ProductID, @Quantity, @SalePrice)";
                var parameters = new
                {
                    OrderID =orderID,
                    ProductID = productID,
                    Quantity = quantity,
                    SalePrice = salePrice,
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public bool Update(Order data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"UPDATE Orders
                            SET CustomerID = @CustomerID,
                                OrderTime = @OrderTime,
                                DeliveryProvince = @DeliveryProvince,
                                DeliveryAddress = @DeliveryAddress,
                                EmployeeID = @EmployeeID,
                                AcceptTime = @AcceptTime,
                                ShipperID = @ShipperID,
                                ShippedTime = @ShippedTime,
                                FinishedTime = @FinishedTime,
                                Status = @Status
                                where OrderID = @OrderID";
                var parameters = new
                {
                    OrderID = data.OrderID,
                    CustomerID = data.CustomerID,
                    OrderTime = data.OrderTime,
                    DeliveryProvince = data.DeliveryProvince,
                    DeliveryAddress = data.DeliveryAddress,
                    EmployeeID = data.EmployeeID,
                    AcceptTime = data.AcceptTime,
                    ShipperID = data.ShipperID,
                    ShippedTime = data.ShippedTime,
                    FinishedTime = data.FinishedTime,
                    Status = data.Status,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
