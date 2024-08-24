using SV21T1020323.DataLayers;
using SV21T1020323.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.BusinessLayers
{
    public static class CommonDataService
    {
        static readonly ICommonDAL<Province> provinceDB;
        static readonly ICommonDAL<Customer> customerDB;
        static readonly ICommonDAL<Category> categoryDB;
        static readonly ICommonDAL<Supplier> supplierDB;
        static readonly ICommonDAL<Shipper> shipperDB;
        static readonly ICommonDAL<Employee> employeeDB;
        static readonly ICommonDAL<OrderStatus> orderStatusDB;

        static CommonDataService()
        {
            string connectionString = Configuration.ConnectionString;

            provinceDB = new DataLayers.SQLServer.ProvinceDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            employeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            orderStatusDB = new DataLayers.SQLServer.OrderStatusDAL(connectionString);
        }

        public static List<OrderStatus> ListOfOrderStatuses()
        {
            return orderStatusDB.List().ToList();
        }


        //Province
        /// <summary>
        /// Lấy danh sách toàn bộ tỉnh/thành
        /// </summary>
        /// <returns></returns>
        public static List<Province> ListOfProvinces()
        {
            return provinceDB.List().ToList();
        }

        //Customer
        /// <summary>
        /// Lấy danh sách khách hàng (tìm kiếm, phân trang)
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Lấy danh sách khách hàng (tìm kiếm, không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue = "")
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Lấy thông tin của 1 khách hàng dựa vào mã khách hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer? GetCustomer(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return customerDB.Get(id);
        }

        /// <summary>
        /// Bổ sung 1 khách hàng mới. Hàm trả về id của khách hàng được bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của 1 khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data) 
        {
            return customerDB.Update(data);
        }

        /// <summary>
        /// Xóa 1 khách hàng dựa vào mã khách hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int id)
        {
            return customerDB.Delete(id);
        }

        /// <summary>
        /// Kiểm tra khách hàng có mã id hiện có dữ liệu liên quan hay không ?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedCustomer(int id)
        {
            return customerDB.InUsed(id);
        }

        //Category
        /// <summary>
        /// Lấy danh sách loại hàng ( tìm kiếm, phân trang)
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Lấy danh sách loại hàng (tìm kiếm, không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(string searchValue = "")
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Lấy thông tin của 1 loại hàng dựa vào mã loại hàng 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Category? GetCategory(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return categoryDB.Get(id);
        }

        /// <summary>
        /// Bổ sung 1 loại hàng mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của 1 loại hàng 
        /// </summary>
        /// <param name="id"></param>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// Xóa 1 loại hàng dựa vào mã loại hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int id)
        {
            return categoryDB.Delete(id);
        }

        /// <summary>
        /// Kiểm tra xem loại hàng có mã id có liên quan đến dữ liệu khác hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedCategory(int id)
        {
            return categoryDB.InUsed(id);
        }

        //Supplier
        /// <summary>
        /// Lấy danh sách nhà cung cấp ( tìm kiếm, phân trang)
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp (tìm kiếm, không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers (string searchValue = "")
        {
            return supplierDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Lấy thông tin của 1 nhà cung cấp dựa vào mã nhà cung cấp 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Supplier? GetSupplier(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return supplierDB.Get(id);
        }

        /// <summary>
        /// Bổ sung 1 nhà cung cấp mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của 1 nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);
        }

        /// <summary>
        /// Xóa 1 nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int id)
        {
            return supplierDB.Delete(id);
        }

        /// <summary>
        /// Kiểm tra xem nhà cung cấp có mã id có liên quan đến dữ liệu khác hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedSupplier(int id)
        {
            return supplierDB.InUsed(id);
        }

        //Shipper
        /// <summary>
        /// Lấy danh sách hãng giao hàng( tìm kiếm, phân trang)
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Lấy danh sách hãng giao hàng (tìm kiếm, không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(string searchValue = "")
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Lấy thông tin của 1 hãng giao hàng dựa vào mã hãng giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Shipper? GetShipper(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return shipperDB.Get(id);
        }

        /// <summary>
        /// Bổ sung 1 hãng giao hàng mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của 1 hãng giao hàng
        /// </summary>
        /// <param name="id"></param>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa 1 hãng giao hàng dựa vào mã hãng giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int id)
        {
            return shipperDB.Delete(id);
        }

        /// <summary>
        /// Kiểm tra xem hãng giao hàng có id có liên quan đến dữ liệu khác hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedShipper(int id)
        {
            return shipperDB.InUsed(id);
        }

        //Employee
        /// <summary>
        /// Lấy danh sách nhân viên( tìm kiếm, phân trang)
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Lấy danh sách nhân viên (tìm kiếm, không phân trang)
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(string searchValue = "")
        {
            return employeeDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Lấy thông tin của 1 hãng giao hàng dựa vào mã hãng giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Employee? GetEmployee(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            return employeeDB.Get(id);
        }

        /// <summary>
        /// Bổ sung 1 nhân viên mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của 1 nhân viên
        /// </summary>
        /// <param name="id"></param>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// Xóa 1 nhân viên dựa vào mã hãng giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int id)
        {
            return employeeDB.Delete(id);
        }

        /// <summary>
        /// Kiểm tra xem nhân viên có id có liên quan đến dữ liệu khác hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUsedEmployee(int id)
        {
            return employeeDB.InUsed(id);
        }

        /// <summary>
        /// Kiểm tra xem email đã xuất hiện trong danh sách nhân viên chưa?
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsEmailEmployeeDuplicate(Employee data)
        {
            return employeeDB.IsEmailValid(data);
        }
    }
}
