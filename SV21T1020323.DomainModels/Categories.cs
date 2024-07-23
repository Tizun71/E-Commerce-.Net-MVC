using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DomainModels
{
    /// <summary>
    /// Categories Data
    /// </summary>
    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty ;
    }
}
