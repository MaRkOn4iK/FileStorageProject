namespace BLL.Validation.Exceptions
{
    /// <summary>
    /// FileStorageException class
    /// </summary>
    public class FileStorageException : Exception
    {
        /// <summary>
        /// Ctor with single param 
        /// </summary>
        /// <param name="message">Exception message</param>
        public FileStorageException(string message) : base(message)
        {

        }
    }
}
