using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.Models;

namespace PL.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IAutorizationService _autorizationService;
        public UserController(IUserService userService, IAutorizationService autorizationService)
        {
            _userService = userService;
            _autorizationService = autorizationService;
        }
        /// <summary>
        /// Delete user by login
        /// </summary>
        /// <param name="Username">User login</param>
        /// <returns>Ok if success and bad request if not</returns>
        [HttpDelete("DeleteAccount/{Username}")]
        [Authorize]
        public IActionResult DeleteUser(string Username)
        {
            try
            {
                _userService.DeleteAsyncByLogin(Username);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Change information about user by login
        /// </summary>
        /// <param name="user">User model for update</param>
        /// <returns>Ok if success and bad request if not</returns>
        [HttpPost("ChageAllInfo")]
        [Authorize]
        public IActionResult ChageAllInfo([FromBody] UserModelForFullUpdate user)
        {
            try
            {
                _userService.ChangeAll(user.Username, user.NewUsername, user.NewPassword, user.NewName, user.NewLastName, user.NewEmail);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Get information about user by his login
        /// </summary>
        /// <param name="Username">User login</param>
        /// <returns>Ok if success and bad request if not</returns>
        [HttpGet("GetInfo/{Username}")]
        [Authorize]
        public IActionResult GetInfoByLogin(string Username)
        {
            try
            {
                var tmp = _autorizationService.GetUserByLogin(Username);
                return Ok(new UserRegist { Username = tmp.Login, Password = tmp.Password, Email = tmp.Email, Name = tmp.Name, LastName = tmp.LastName });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
