using SV21T1020323.DataLayers;
using SV21T1020323.DataLayers.SQLServer;
using SV21T1020323.DomainModels;
using SV21T1020323.BusinessLayers;

namespace SV21T1020323.BusinessLayers
{
    public static class UserAccountService
    {
        private static readonly IUserAccountDAL employeeAccountDB;

        static UserAccountService()
        {
            employeeAccountDB = new EmployeeAccountDAL(Configuration.ConnectionString);
        }

        public static UserAccount? Authorize(string userName, string password)
        {
            return employeeAccountDB.Authorize(userName, password);
        }

        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
        }

        public static bool IsOldPassword(string userName, string oldPassword)
        {
            return employeeAccountDB.IsOldPassword(userName, oldPassword);
        }

    }
}
