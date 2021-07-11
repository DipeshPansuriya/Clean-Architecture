using Application_Command.Insert_Command.UserConfig;
using Application_Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application_API.Controllers.UserConfig
{
    public class RightController : BaseController<RightController>
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            Response res = await this.Mediator.Send(new Right_Inst_cmd());

            return this.Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Create([FromBody] Right_Inst_cmd inst_Cmd)
        {
            Response res = await this.Mediator.Send(inst_Cmd);
            return this.Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Update([FromBody] Right_Upd_cmd upd_Cmd)
        {
            Response res = await this.Mediator.Send(upd_Cmd);
            return this.Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Delete(int Id)
        {
            Response res = await this.Mediator.Send(new Right_Del_cmd { Id = Id });
            return this.Ok(res);
        }

        //[HttpPut]
        //public async Task<ActionResult<Response>> GetData(int Id)
        //{
        //    Response res = await this.Mediator.Send(new Demo_Customer_lst_cmd(Id));
        //    return this.Ok(res);
        //}
    }
}