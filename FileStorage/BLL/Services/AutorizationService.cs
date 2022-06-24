using BLL.Interfaces;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    /// <summary>
    /// Service for authenticate
    /// </summary>
    public class AutorizationService : IAutorizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AutorizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Check if user with params as login and password is exist
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns>User if exist and throw exception if not</returns>
        /// <exception cref="FileStorageException">If user with params is not registred</exception>
        public User Authenticate(string login, string password)
        {
            var tmpList = _unitOfWork.UserRepository.GetAll();
            var currentUser = tmpList.FirstOrDefault(o => o.Login.ToLower() == login.ToLower() && o.Password == password);

            if (currentUser != null)
            {
                return currentUser;
            }

            throw new FileStorageException("Incorrect login or password");
        }
        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>user if exist and throw exception if not</returns>
        /// <exception cref="FileStorageException">if user with this login is not exist</exception>
        public User GetUserByLogin(string login)
        {
            var tmpList = _unitOfWork.UserRepository.GetAll();
            var currentUser = tmpList.FirstOrDefault(o => o.Login.ToLower() == login.ToLower());

            if (currentUser != null)
            {
                return currentUser;
            }

            throw new FileStorageException("Incorrect login");
        }
        /// <summary>
        /// Registration new user
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <param name="name">User name</param>
        /// <param name="lastName">User lastname</param>
        /// <param name="email">User email</param>
        /// <returns>User if success and throw exception if not</returns>
        /// <exception cref="FileStorageException">If one of the params is invalid</exception>
        public User Registration(string login, string password, string name, string lastName, string email)
        {
            var tmpList = _unitOfWork.UserRepository.GetAll();
            foreach (var item in tmpList)
            {
                if (item.Login == login)
                    throw new FileStorageException("This login is taken");
            }
            if (string.IsNullOrEmpty(login) || login.Length < 3)
                throw new FileStorageException("This login is invalid");
            if (string.IsNullOrEmpty(password) || password.Length < 5)
                throw new FileStorageException("This unreliable is invalid");
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName))
                throw new FileStorageException("Please input correct Name and Last Name");
            if (email == null || email.Length < 10 || !email.Contains('@'))
                throw new FileStorageException("This email is invalid");
            var user = new User { Login = login, Password = password, Email = email, Name = name, LastName = lastName };
            _unitOfWork.UserRepository.Add(user);
            _unitOfWork.SaveAsync();
            return user;
        }
    }
}
