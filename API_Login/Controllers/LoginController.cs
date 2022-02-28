using API_Login.Handler;
using API_Login.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Login.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController<LoginController>
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            JWTHandler jWTHandler = new JWTHandler();

            bool user = jWTHandler.AuthenticateUser(login.Username, login.Password);

            if (user)
            {
                string tokenString = jWTHandler.GenerateJSONWebToken(login.Username, login.Password);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
    }
}