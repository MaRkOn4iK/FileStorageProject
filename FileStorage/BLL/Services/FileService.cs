using BLL.Interfaces;
using BLL.Validation.Exceptions;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// Service for operation with files
    /// </summary>
    public class FileService : IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Add new file to db
        /// </summary>
        /// <param name="model">New file</param>
        /// <returns>Task</returns>
        /// <exception cref="FileStorageException">if file is null</exception>
        public async Task AddAsync(FullFileInfo model)
        {
            if (model == null)
            {
                throw new FileStorageException("model is null");
            }

            try
            {
                _unitOfWork.FullFileInfoRepository.Add(model);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Delete file by id
        /// </summary>
        /// <param name="modelId">File id</param>
        /// <returns>Task</returns>
        /// <exception cref="FileStorageException">If something go wrong</exception>
        public async Task DeleteAsync(int modelId)
        {
            try
            {
                _unitOfWork.FullFileInfoRepository.DeleteById(modelId);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Get all files by user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Collection of files</returns>
        /// <exception cref="FileStorageException">If something go wrong</exception>
        public IEnumerable<FullFileInfo> GetAllByUser(User user)
        {
            try
            {
                List<FullFileInfo> result = new();
                IEnumerable<FullFileInfo>? tmpList = _unitOfWork.FullFileInfoRepository.GetAll();
                foreach (FullFileInfo? item in tmpList)
                {
                    if (item.User.Login == user.Login)
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Get all private files by user
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Collection of files</returns>
        /// <exception cref="FileStorageException">If something go wrong</exception>
        public IEnumerable<FullFileInfo> GetAllPrivateByUser(User user)
        {
            try
            {
                List<FullFileInfo> result = new();
                IEnumerable<FullFileInfo>? tmpList = _unitOfWork.FullFileInfoRepository.GetAll();
                foreach (FullFileInfo? item in tmpList)
                {
                    if (item.User.Login == user.Login && item.FileSecureLevel.SecureLevelName == "private")
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Get all public files
        /// </summary>
        /// <returns>Collection of files</returns>
        /// <exception cref="FileStorageException">If something go wrong</exception>
        public IEnumerable<FullFileInfo> GetAllPublicFiles()
        {
            try
            {
                List<FullFileInfo> result = new();
                IEnumerable<FullFileInfo>? tmpList = _unitOfWork.FullFileInfoRepository.GetAll();
                foreach (FullFileInfo? item in tmpList)
                {
                    if (item.FileSecureLevel.SecureLevelName == "public")
                    {
                        result.Add(item);
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Get file by id
        /// </summary>
        /// <param name="id">File id</param>
        /// <returns>File</returns>
        /// <exception cref="FileStorageException">If something go wrong</exception>
        public async Task<FullFileInfo> GetByIdAsync(int id)
        {
            try
            {
                return await _unitOfWork.FullFileInfoRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Get file by file name and user as owner 
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="user">User</param>
        /// <returns>File</returns>
        /// <exception cref="FileStorageException">If file with this name is not exist</exception>
        public FullFileInfo GetByName(string fileName, User user)
        {
            try
            {
                var list = this.GetAllByUser(user);
                foreach (FullFileInfo item in list)
                {
                    if (item.File.FileName == fileName)
                    {
                        return item;
                    }
                }
                throw new FileStorageException("File not found");
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
    }
}
