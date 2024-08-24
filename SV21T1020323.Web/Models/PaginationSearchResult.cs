using SV21T1020323.DomainModels;

namespace SV21T1020323.Web.Models
{
    /// <summary>
    /// Lớp cơ sở cho kết quả tìm kiếm, hiển thị dữ liệu dưới dạng phân trang
    /// </summary>
    public class PaginationSearchResult
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; }
        public string SearchValue { get; set; } = "";
        public int RowCount { get; set; } = 0;
        public int PageCount
        {
            get
            {
                if (PageSize <= 0)
                    return 1;

                int n = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                {
                    n++;
                }
                return n;
            }
        }

    }
    /// <summary>
    /// Kết quả tìm kiếm khách hàng
    /// </summary>
    public class CustomerSearchResult: PaginationSearchResult
    {
        public List<Customer> Data { get; set; }
    }
    /// <summary>
    /// Kết quả tìm kiếm nhân viên
    /// </summary>
    public class EmployeeSearchResult : PaginationSearchResult
    {
        public List<Employee> Data { get; set; }
    }
    /// <summary>
    /// Kết quả tìm kiếm nhà cung cấp
    /// </summary>
    public class SupplierSearchResult : PaginationSearchResult
    {
        public List<Supplier> Data { get; set; }
    }
    /// <summary>
    /// Kết quả tìm kiếm người giao hàng
    /// </summary>
    public class ShipperSearchResult : PaginationSearchResult
    {
        public List<Shipper> Data { get; set; }
    }
    /// <summary>
    /// Kết quả tìm kiếm loại hành
    /// </summary>
    public class CategorySearchResult : PaginationSearchResult
    {
        public List<Category> Data { get; set; }

    }

    public class ProductSearchResult : PaginationSearchResult
    {
        public int CategoryId { get; set; } = 0;
        public int SupplierId { get; set; } = 0;
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 0;
        public required List<Product> Data { get; set; }
    }

    public class OrderSearchResult : PaginationSearchResult
    {
        public int Status {  get; set; } = 0;
        public string TimeRange { get; set; } = "";
        public List<Order> Data { get; set; } = new List<Order>();
    }
}
