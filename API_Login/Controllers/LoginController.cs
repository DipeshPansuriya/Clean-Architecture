using API_Login.Handler;
using Application_Common;
using Login_Command.List;
using Login_Command.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Login.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController<LoginController>
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Response>> Login([FromBody] LoginDetails_Lst cmd)
        {
            Response res = new Response();
            if (cmd != null)
            {
                res = await Mediator.Send(cmd);
                if (res.ResponseStatus)
                {
                    JWTHandler jWTHandler = new JWTHandler();

                    string tokenString = jWTHandler.GenerateJSONWebToken();

                    UserInfo data = GenericFunction.ObjectToClass<UserInfo>(res.ResponseObject);
                    data.Token = tokenString;
                    res.ResponseObject = data;
                    return Ok(res);
                }
            }
            else
            {
                res.StatusCode = System.Net.HttpStatusCode.NotAcceptable;
                res.ResponseStatus = false;
                res.ResponseObject = "Command is null";
            }
            return BadRequest(res);
        }
    }
}