using Application_Command.Details_Command.UserConfig;
using Application_Command.Insert_Command.UserConfig;
using Application_Command.List_Command.UserConfig;
using Application_Domain;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Application_API.Controllers.UserConfig
{
    public class RightController : BaseController<RightController>
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            int requestid = RequestResponse.RequestSave(this.ControllerContext.ToString(), null, null);
            Response res = await Mediator.Send(new Right_Lst_cmd());

            RequestResponse.RepsponseSave(JsonConvert.SerializeObject(res), requestid.ToString());

            if (res.ResponseStatus.ToLower() != "success")
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create([FromBody] Right_Inst_cmd inst_Cmd)
        {
            Response res = await Mediator.Send(inst_Cmd);
            if (res.ResponseStatus.ToLower() != "success")
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Update([FromBody] Right_Upd_cmd upd_Cmd)
        {
            Response res = await Mediator.Send(upd_Cmd);
            if (res.ResponseStatus.ToLower() != "success")
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Delete(int Id)
        {
            Response res = await Mediator.Send(new Right_Del_cmd { Id = Id });
            if (res.ResponseStatus.ToLower() != "success")
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetData(int Id)
        {
            Response res = await Mediator.Send(new Right_Dtl_cmd { Id = Id });
            if (res.ResponseStatus.ToLower() != "success")
                return BadRequest(res);
            else
                return Ok(res);
        }
    }
}