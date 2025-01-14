﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DomainModels
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Photo { get; set; } = string.Empty;
        public bool IsSelling { get; set; }
    }
}
