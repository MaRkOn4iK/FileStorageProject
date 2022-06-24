using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PL.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PL.Controllers
{
    /// <summary>
    /// Controller for login and registration
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IAutorizationService _authorizationService;
        public LoginController(IConfiguration config, IAutorizationService authorizationService)
        {
            _config = config;
            _authorizationService = authorizationService;
        }
        /// <summary>
        /// Login method by userLogin model
        /// </summary>
        /// <param name="userLogin">User model for login</param>
        /// <returns>Jwt token if success and user not found if not</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {

                var user = _authorizationService.Authenticate(userLogin.Username, userLogin.Password);
                if (user != null)
                {
                    var token = Generate(user);
                    return Ok(token);
                }
            }
            catch (Exception)
            {

                return NotFound("User not found");
            }
            return NotFound("User not found");
        }
        /// <summary>
        /// Registrarion method by userRegist model
        /// </summary>
        /// <param name="userRegist">User model for registration</param>
        /// <returns>Ok if success and not found if not</returns>
        [AllowAnonymous]
        [HttpPost("Regist")]
        public IActionResult Registration([FromBody] UserRegist userRegist)
        {
            try
            {
                _authorizationService.Registration(userRegist.Username, userRegist.Password, userRegist.Name, userRegist.LastName, userRegist.Email);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return NotFound($"Sorry but : {ex.Message}");
            }
        }
        /// <summary>
        /// Method which generate jwt token 
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Token in string format</returns>
        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Name),
                new Claim(ClaimTypes.Surname, user.LastName),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
