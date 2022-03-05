using Application_Common;
using Microsoft.AspNetCore.Mvc;
using Registrations_Command.AdminOrganization.InsertUpdate;

namespace API_Registrations.Controllers
{
    public class AdminOrganizationController : BaseController<AdminOrganizationController>
    {
        [HttpPost]
        public async Task<ActionResult<Response>> CreateUpdate([FromBody] Adm_Org_InstUpd cmd)
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