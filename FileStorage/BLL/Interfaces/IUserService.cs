using DAL.Entities;

namespace BLL.Interfaces
{
    /// <summary>
    /// Interface for user service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// return collection all of users
        /// </summary>
        /// <returns>Collection all of users</returns>
        IEnumerable<User> GetAll();
        /// <summary>
        /// Check is in db a user with id?
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>User if exist</returns>
        Task<User> GetByIdAsync(int id);
        /// <summary>
        /// Delete user by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>Task</returns>
        Task DeleteAsyncByLogin(string login);

        /// <summary>
        /// Change user info
        /// </summary>
        /// <param name="oldLogin">User login</param>
        /// <param name="newLogin">New user login</param>
        /// <param name="newPassword">New user password</param>
        /// <param name="newName">New user name</param>
        /// <param name="newLastName">New user lastname</param>
        /// <param name="newEmail">new user email</param>
        /// <returns>Task</returns>
        Task ChangeAll(string oldLogin, string newLogin, string newPassword, string newName, string newLastName, string newEmail);
        /// <summary>
        /// Add to db user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>Task</returns>
        Task AddAsync(User model);
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">User</param>
        /// <returns>Task</returns>
        Task UpdateAsync(User model);
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="modelId">User id</param>
        /// <returns>Task</returns>
        Task DeleteAsync(int modelId);
    }
}
