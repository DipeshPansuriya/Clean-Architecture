using Application_Command.Details_Command.UserConfig;
using Application_Command.Insert_Command.UserConfig;
using Application_Command.List_Command.UserConfig;
using Application_Genric;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Application_API.Controllers.UserConfig
{
    public class RoleController : BaseController<RoleController>
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            int requestid = RequestResponse.RequestSave(this.ControllerContext.ToString(), null, null);

            Response res = await Mediator.Send(new Role_Lst_cmd());

            RequestResponse.RepsponseSave(JsonConvert.SerializeObject(res), requestid.ToString());

            if (!res.ResponseStatus)
                return BadRequest(res);
            else
                return Ok(res);

        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create([FromBody] Role_Inst_cmd inst_Cmd)
        {
            int requestid = RequestResponse.RequestSave(this.ControllerContext.ToString(), null, JsonConvert.SerializeObject(inst_Cmd));

            Response res = await Mediator.Send(inst_Cmd);

            RequestResponse.RepsponseSave(JsonConvert.SerializeObject(res), requestid.ToString());

            if (!res.ResponseStatus)
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Update([FromBody] Role_Upd_cmd upd_Cmd)
        {
            int requestid = RequestResponse.RequestSave(this.ControllerContext.ToString(), null, JsonConvert.SerializeObject(upd_Cmd));

            Response res = await Mediator.Send(upd_Cmd);

            RequestResponse.RepsponseSave(JsonConvert.SerializeObject(res), requestid.ToString());

            if (!res.ResponseStatus)
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Delete(int Id)
        {
            int requestid = RequestResponse.RequestSave(this.ControllerContext.ToString(), null, JsonConvert.SerializeObject(Id));

            Response res = await Mediator.Send(new Role_Del_cmd { Id = Id });

            RequestResponse.RepsponseSave(JsonConvert.SerializeObject(res), requestid.ToString());

            if (!res.ResponseStatus)
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<Response>> GetData(int Id)
        {
            int requestid = RequestResponse.RequestSave(this.ControllerContext.ToString(), null, JsonConvert.SerializeObject(Id));

            Response res = await Mediator.Send(new Role_Dtl_cmd { Id = Id });

            RequestResponse.RepsponseSave(JsonConvert.SerializeObject(res), requestid.ToString());

            if (!res.ResponseStatus)
                return BadRequest(res);
            else
                return Ok(res);
        }
    }
}