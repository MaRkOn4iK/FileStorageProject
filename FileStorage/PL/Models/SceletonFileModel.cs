namespace PL.Models
{
    /// <summary>
    /// This class is a model for file transfer to the client side without array of bytes
    /// </summary>
    public class SceletonFileModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? CreateDate { get; set; }
        public string? FileName { get; set; }
        public int Size { get; set; }
        public string? FileSecureLevel { get; set; }

    }
}
