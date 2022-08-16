namespace YourSoft.BLL.Models
{
    public class QueryParameters
    {
        private int _pageSize = 10;
        public int Page { get; set; } = 1;
        public string SortBy { get; set; } = "";
        public string OrderBy { get; set; } = "asc";
        public string Search { get; set; } = "";
        public string SearchBy { get; set; } = "";
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
