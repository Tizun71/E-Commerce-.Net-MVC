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

    public class EmployeeSearchResult : PaginationSearchResult
    {
        public List<Employee> Data { get; set; }
    }
}
