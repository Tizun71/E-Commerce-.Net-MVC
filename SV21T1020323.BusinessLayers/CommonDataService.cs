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
        private static readonly CustomerDAL customerDB;

        static CommonDataService()
        {
            string connectionString = @"server=.;
                                        user id=sa;
                                        password=123456789;
                                        database=LiteCommerceDB_2023;
                                        TrustServerCertificate=true";
            customerDB = new CustomerDAL(connectionString);
        }

        public static List<Customer> ListOfCustomers()
        {
            return customerDB.List();
        }
    }
}
