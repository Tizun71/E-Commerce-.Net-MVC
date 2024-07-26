using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DomainModels
{
    /// <summary>
    /// Employee Data
    /// </summary>
    public class Employees
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public bool isWorking { get; set; } = false;
    }
}
