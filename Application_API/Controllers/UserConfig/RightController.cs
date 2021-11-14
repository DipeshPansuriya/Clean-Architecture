using Application_Command.Details_Command.UserConfig;
using Application_Command.Insert_Command.UserConfig;
using Application_Command.List_Command.UserConfig;
using Application_Genric;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application_API.Controllers.UserConfig
{
    public class RightController : BaseController<RightController>
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            Response res = await Mediator.Send(new Right_Lst_cmd());

            if (!res.ResponseStatus)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create([FromBody] Right_Inst_cmd inst_Cmd)
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

        [HttpPut]
        public async Task<ActionResult<Response>> Update([FromBody] Right_Upd_cmd upd_Cmd)
        {
            Response res = await Mediator.Send(upd_Cmd);

            if (!res.ResponseStatus)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Delete(int Id)
        {
            Response res = await Mediator.Send(new Right_Del_cmd { Id = Id });

            if (!res.ResponseStatus)
            {
                return BadRequest(res);
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetData(int Id)
        {
            Response res = await Mediator.Send(new Right_Dtl_cmd { Id = Id });

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