namespace YourSoft.BLL.Models
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<T> Data { get; set; }
    }
}
