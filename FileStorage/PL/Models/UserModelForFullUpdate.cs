namespace PL.Models
{
    /// <summary>
    /// This model for update user information by his login
    /// </summary>
    public class UserModelForFullUpdate
    {
        public string? Username { get; set; }
        public string? NewUsername { get; set; }
        public string? NewPassword { get; set; }
        public string? NewName { get; set; }
        public string? NewLastName { get; set; }
        public string? NewEmail { get; set; }
    }
}
