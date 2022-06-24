namespace PL.Models
{
    /// <summary>
    /// This model for registration new user and get information by login 
    /// </summary>
    public class UserRegist
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
