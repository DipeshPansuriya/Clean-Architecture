using Application_Common;
using Generic_Command.InsertUpdate.Menu_InstUpd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Generic.Controllers
{
    [AllowAnonymous]
    public class MenuController : BaseController<MenuController>
    {
        [HttpPost]
        public async Task<ActionResult<Response>> CreateUpdate([FromBody] Menu_InstUpd cmd)
        {
            Response res = new Response();

            res = await Mediator.Send(cmd);

            if (res.ResponseStatus)
            {
                return Ok(res);
            }

            return BadRequest(res);
        }
    }
}