namespace YourSoft.BLL.Models
{
    public class QueryParameters
    {
        private int _pageSize = 10;
        public int Page { get; set; } = 1;
        public string SortBy { get; set; } = "Id";
        public string OrderBy { get; set; } = "ASC";
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}
