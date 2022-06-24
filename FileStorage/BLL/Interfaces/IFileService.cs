using DAL.Entities;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for file service
    /// </summary>
    public interface IFileService
    {
    /// <summary>
    /// Get all files by user
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>Collection files</returns>
        IEnumerable<FullFileInfo> GetAllByUser(User user);
        /// <summary>
        /// Get all private files by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Collection files</returns>
        IEnumerable<FullFileInfo> GetAllPrivateByUser(User user);
        /// <summary>
        /// Get all public files
        /// </summary>
        /// <returns>Collection files</returns>
        IEnumerable<FullFileInfo> GetAllPublicFiles();
        /// <summary>
        /// Get file by id
        /// </summary>
        /// <param name="id">File id</param>
        /// <returns>File</returns>
        Task<FullFileInfo> GetByIdAsync(int id);
        /// <summary>
        /// Get file by name and user
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="user">User</param>
        /// <returns>File</returns>
        FullFileInfo GetByName(string fileName, User user);
        /// <summary>
        /// Add file to db
        /// </summary>
        /// <param name="model">New file</param>
        /// <returns>Task</returns>
        Task AddAsync(FullFileInfo model);
        /// <summary>
        /// Delete file by id
        /// </summary>
        /// <param name="modelId">File id</param>
        /// <returns>Task</returns>
        Task DeleteAsync(int modelId);
    }
}
