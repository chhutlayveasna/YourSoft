namespace YourSoft.BLL.Models
{
    public class ResponseResult<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
