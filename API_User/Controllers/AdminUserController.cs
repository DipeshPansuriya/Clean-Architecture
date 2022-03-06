using Application_Common;
using Microsoft.AspNetCore.Mvc;
using User_Command.AdminUser.Delete;
using User_Command.AdminUser.InsertUpdate;
using User_Command.AdminUser.List;
using User_Command.AdminUser.Select;

namespace API_User.Controllers
{
    public class AdminUserController : BaseController<AdminUserController>
    {
        [HttpPost]
        public async Task<ActionResult<Response>> CreateUpdate([FromBody] Adm_User_InstUpd cmd)
        {
            Response res = new Response();
            if (cmd != null)
            {
                res = await Mediator.Send(cmd);

                if (res.ResponseStatus)
                {
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

        [HttpGet]
        public async Task<ActionResult<Response>> List([FromBody] Adm_User_Lst cmd)
        {
            Response res = new Response();
            if (cmd != null)
            {
                res = await Mediator.Send(cmd);

                if (res.ResponseStatus)
                {
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

        [HttpGet]
        public async Task<ActionResult<Response>> Select([FromBody] Adm_User_Select cmd)
        {
            Response res = new Response();
            if (cmd != null)
            {
                res = await Mediator.Send(cmd);

                if (res.ResponseStatus)
                {
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

        [HttpPut]
        public async Task<ActionResult<Response>> Delete([FromBody] Adm_User_Del cmd)
        {
            Response res = new Response();
            if (cmd != null)
            {
                res = await Mediator.Send(cmd);

                if (res.ResponseStatus)
                {
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