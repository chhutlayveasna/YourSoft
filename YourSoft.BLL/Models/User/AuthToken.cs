namespace YourSoft.BLL.Models.User
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
