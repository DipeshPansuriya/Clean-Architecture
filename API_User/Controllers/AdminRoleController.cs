using Application_Common;
using Microsoft.AspNetCore.Mvc;
using User_Command.AdminRole.Delete;
using User_Command.AdminRole.InsertUpdate;
using User_Command.AdminRole.List;
using User_Command.AdminRole.Select;

namespace API_Users.Controllers
{
    public class AdminRoleController : BaseController<AdminRoleController>
    {
        [HttpPost]
        public async Task<ActionResult<Response>> CreateUpdate([FromBody] Adm_Role_InstUpd cmd)
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
        public async Task<ActionResult<Response>> List([FromBody] Adm_Role_Lst cmd)
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
        public async Task<ActionResult<Response>> Select([FromBody] Adm_Role_Select cmd)
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
        public async Task<ActionResult<Response>> Delete([FromBody] Adm_Role_Del cmd)
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