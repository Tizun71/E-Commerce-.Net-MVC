using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DomainModels
{
    public class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string Unit{ get; set; }
        public long Price { get; set; }
        public string Photo {  get; set; }
        public bool isSelling { get; set; }
    }
}
