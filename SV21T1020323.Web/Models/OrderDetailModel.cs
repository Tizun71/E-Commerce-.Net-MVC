using SV21T1020323.DomainModels;

namespace SV21T1020323.Web.Models
{
    public class OrderDetailModel
    {
        public Order Order { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
