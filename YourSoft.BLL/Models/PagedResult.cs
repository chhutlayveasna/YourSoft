namespace YourSoft.BLL.Models
{
    public class PagedResult<T> : Response<T>
    {
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }

    }
}
