using Application_Common;
using Generic_Command.InsertUpdate.Prod_InstUpd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Generic.Controllers
{
    [AllowAnonymous]
    public class ProductController : BaseController<ProductController>
    {
        [HttpPost]
        public async Task<ActionResult<Response>> CreateUpdate([FromBody] Prod_InstUpd cmd)
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