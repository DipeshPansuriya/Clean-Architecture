using Application_Services;
using Microsoft.AspNetCore.Mvc;
using Registrations_Command.AdminOrganization.Insert;

namespace API_Registrations.Controllers
{
    public class AdminOrganizationController : BaseController<AdminOrganizationController>
    {
        [HttpPost]
        public async Task<ActionResult<Response>> Create([FromBody] Adm_Org_Inst inst_Cmd)
        {
            Response res = await Mediator.Send(inst_Cmd);

            if (!res.ResponseStatus)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }
    }
}