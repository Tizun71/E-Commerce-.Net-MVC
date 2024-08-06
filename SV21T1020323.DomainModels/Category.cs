namespace SV21T1020323.DomainModels
{
    /// <summary>
    /// Thông tin loại hàng
    /// </summary>
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
