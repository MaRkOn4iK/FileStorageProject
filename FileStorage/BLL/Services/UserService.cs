using BLL.Interfaces;
using BLL.Validation.Exceptions;
using BLL.Validation.UserValidators;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// Service for operations with users
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Add user to db
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>Task</returns>
        /// <exception cref="FileStorageException">if model or login or password is null</exception>
        public async Task AddAsync(User model)
        {



            try
            {
                if (UserValidator.RegistrationValidation(model.Login, model.Password, model.Name, model.LastName, model.Email, _unitOfWork))
                {
                    _unitOfWork.UserRepository.Add(model);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Change all information of user
        /// </summary>
        /// <param name="oldLogin">User login</param>
        /// <param name="newLogin">New user login</param>
        /// <param name="newPassword">New user password</param>
        /// <param name="newName">New user name</param>
        /// <param name="newLastName">New user lastname</param>
        /// <param name="newEmail">New user email</param>
        /// <returns>Task</returns>
        /// <exception cref="FileStorageException">if one of params invalid</exception>
        public async Task ChangeAll(string oldLogin, string newLogin, string newPassword, string newName, string newLastName, string newEmail)
        {
            var user = _unitOfWork.UserRepository.GetAll().Where(res => res.Login == oldLogin).FirstOrDefault();
            if (user != null && UserValidator.UserInformationChangedValidator(oldLogin, newLogin, newPassword, newName, newLastName, newEmail, _unitOfWork))
            {
                user.Login = newLogin;
                user.Password = newPassword;
                user.Name = newName;
                user.LastName = newLastName;
                user.Email = newEmail;
                await this.UpdateAsync(user);
            }
            else throw new AuthException("This login is not exist");

        }
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="modelId"User id></param>
        /// <returns>Task</returns>
        /// <exception cref="FileStorageException">if something go wrong</exception>
        public async Task DeleteAsync(int modelId)
        {
            try
            {
                _unitOfWork.UserRepository.DeleteById(modelId);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Delete user by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Task</returns>
        /// <exception cref="FileStorageException">If user with with login is not registred</exception>
        public async Task DeleteAsyncByLogin(string login)
        {
            try
            {
                var tmp = _unitOfWork.UserRepository.GetAll();

                var modelId = tmp.Where(res => res.Login == login).Select(res => res.Id).FirstOrDefault();
                if (modelId != default)
                {
                    var allFiles = _unitOfWork.FullFileInfoRepository.GetAll();
                    List<FullFileInfo> files = new List<FullFileInfo>();
                    foreach (var item in allFiles)
                    {
                        if (modelId == item.User.Id)
                            files.Add(item);
                    }
                    foreach (var item in files)
                    {
                        _unitOfWork.FullFileInfoRepository.DeleteById(item.Id);
                    }
                    _unitOfWork.UserRepository.DeleteById(modelId);
                    await _unitOfWork.SaveAsync();
                }
                else throw new AuthException("This login is not registred");
            }
            catch (FileStorageException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Collection of user</returns>
        /// <exception cref="FileStorageException">If something go wrong</exception>
        public IEnumerable<User> GetAll()
        {
            try
            {
                return _unitOfWork.UserRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>User</returns>
        /// <exception cref="FileStorageException">if something go wrong</exception>
        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _unitOfWork.UserRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>Task</returns>
        /// <exception cref="FileStorageException">If one of the user params is invalid</exception>
        public async Task UpdateAsync(User model)
        {

            try
            {
                    _unitOfWork.UserRepository.Update(model);
                    await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new FileStorageException(ex.Message);
            }
        }
    }
}
