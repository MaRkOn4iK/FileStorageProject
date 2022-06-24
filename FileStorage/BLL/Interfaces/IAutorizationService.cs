using DAL.Entities;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for Auth service
    /// </summary>
    public interface IAutorizationService
    {
        /// <summary>
        /// Check if user with params is exist
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <returns>logged in user</returns>
        User Authenticate(string login, string password);
        /// <summary>
        /// Check if user with this login exist. If not exist save user to db
        /// </summary>
        /// <param name="login">User login</param>
        /// <param name="password">User password</param>
        /// <param name="name">User name</param>
        /// <param name="lastName">User lastname</param>
        /// <param name="email">User email</param>
        /// <returns>logged in user</returns>
        public User Registration(string login, string password, string name, string lastName, string email);
        /// <summary>
        /// Check if user with param exist
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User if exist</returns>
        User GetUserByLogin(string login);

    }
}
