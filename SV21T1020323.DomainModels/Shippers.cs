using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DomainModels
{
    /// <summary>
    /// Shipper Data
    /// </summary>
    public class Shippers
    {
        public int ShipperId { get; set; }
        public string ShipperName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
