using SV21T1020323.DomainModels;

namespace SV21T1020323.Web.Models
{
    /// <summary>
    /// Đầu vào tìm kiếm dữ liệu để nhận dữ liệu dưới dạng phân trang
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 0;
        public string SearchValue { get; set; } = "";
    }

    public class OrderSearchInput : PaginationSearchInput
    {
        /// <summary>
        /// Trạng thái của đơn hàng cần tìm
        /// </summary>
        public int Status { get; set; } = 0;
        /// <summary>
        /// Khoảng thời gian cần tìm (chuỗi 2 giá trị ngày có dạng dd/MM/yyyy - dd/MM/yyyy)
        /// </summary>
        public string DateRange { get; set; } = "";
        /// <summary>
        /// Lấy thời điểm bắt đầu dựa vào DateRange
        /// </summary>
        public DateTime? FromTime
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DateRange))
                    return null;
                string[] times = DateRange.Split('-');
                if (times.Length == 2)
                {
                    DateTime? value = Converter.ToDateTime(times[0].Trim());
                    return value;
                }
                return null;
            }
        }
        /// <summary>
        /// Lấy thời điểm kết thúc dựa vào DateRange
        /// (thời điểm kết thúc phải là cuối ngày)
        /// </summary>
        public DateTime? ToTime
        {
            get
            {
                if (string.IsNullOrWhiteSpace(DateRange))
                    return null;
                string[] times = DateRange.Split('-');
                if (times.Length == 2)
                {
                    DateTime? value = Converter.ToDateTime(times[1].Trim());
                    if (value.HasValue)
                        value = value.Value.AddMilliseconds(86399998); //86399999
                    return value;
                }
                return null;
            }
        }
    }

    public class ProductSearchInput: PaginationSearchInput
    {
        public int CategoryId { get; set; } = 0;
        public int SupplierId { get; set; } = 0;
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 0;
    }
}
