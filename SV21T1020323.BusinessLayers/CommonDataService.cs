using SV21T1020323.DataLayers;
using SV21T1020323.DomainModels;

namespace SV21T1020323.BusinessLayers
{
    /// <summary>
    /// Connect database and fetch data
    /// </summary>
    public static class CommonDataService
    {
        private static readonly CustomerDAL customerDB;
        private static readonly CategoriesDAL categoriesDB;
        private static readonly SuppliersDAL suppliersDB;
        private static readonly ShippersDAL shippersDB;
        private static readonly EmployeesDAL employeesDB;

        static CommonDataService()
        {
            string connectionString = @"server=.;
                                        user id=sa;
                                        password=123456789;
                                        database=LiteCommerceDB_2023;
                                        TrustServerCertificate=true";
            customerDB = new CustomerDAL(connectionString);
            categoriesDB = new CategoriesDAL(connectionString);
            suppliersDB = new SuppliersDAL(connectionString);
            shippersDB = new ShippersDAL(connectionString);
            employeesDB = new EmployeesDAL(connectionString);
        }

        public static List<Customer> ListOfCustomers()
        {
            return customerDB.List();
        }

        public static List<Categories> ListOfCategories()
        {
            return categoriesDB.List();
        }

        public static List<Suppliers> ListOfSuppliers()
        {
            return suppliersDB.List();
        }

        public static List<Shippers> ListOfShippers()
        {
            return shippersDB.List();
        }

        public static List<Employees> ListOfEmployees()
        {
            return employeesDB.List();
        }
    }
}
