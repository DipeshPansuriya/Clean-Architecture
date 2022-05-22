using Application_Common;
using Microsoft.AspNetCore.Mvc;
using User_Command.AdminBranch.Delete;
using User_Command.AdminBranch.InsertUpdate;
using User_Command.AdminBranch.List;
using User_Command.AdminBranch.Select;

namespace API_User.Controllers
{
    public class AdminBranchController : BaseController<AdminBranchController>
    {
        [HttpPost]
        public async Task<ActionResult<Response>> CreateUpdate([FromBody] Adm_Bran_InstUpd cmd)
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
        public async Task<ActionResult<Response>> List([FromBody] Adm_Bran_Lst cmd)
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
        public async Task<ActionResult<Response>> Select([FromBody] Adm_Bran_Select cmd)
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
        public async Task<ActionResult<Response>> Delete([FromBody] Adm_Bran_Del cmd)
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