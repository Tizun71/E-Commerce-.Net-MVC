using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV21T1020323.DomainModels
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? AcceptTime { get; set; }

        public int CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerContactName { get; set; } = string.Empty;
        public string CustomerAddress {  get; set; } = string.Empty;    
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail {  get; set; } = string.Empty;

        public string DeliveryProvince { get; set; } = string.Empty;
        public string DeliveryAddress { get; set; } = string.Empty;

        public int? EmployeeID { get; set; }
        public string EmployeeName { get; set; } = string.Empty;

        public int? ShipperID { get; set; }
        public string ShipperName { get; set;} = string.Empty;
        public string ShipperPhone  { get; set;} = string.Empty;
        
        public DateTime? ShippedTime { get; set; }
        public DateTime? FinishedTime { get; set; }
        public int Status { get; set; }

        public string StatusDescription
        {
            get
            {
                switch (Status)
                {
                    case Constants.ORDER_INIT:
                        return "Đơn hàng mới. Đang chờ duyệt";
                    case Constants.ORDER_ACCEPTED:
                        return "Đơn đã chấp nhận. Đang chờ giao hàng";
                    case Constants.ORDER_SHIPPING:
                        return "Đơn hàng đã được giao";
                    case Constants.ORDER_FINISHED:
                        return "Đơn hàng đã hoàn tất";
                    case Constants.ORDER_CANCEL:
                        return "Đơn hàng đã bị hủy";
                    case Constants.ORDER_REJECTED:
                        return "Đơn hàng bị tự chối";
                    default:
                        return "";
                }
            }
        }
    }
}
