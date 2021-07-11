using Application_Command.Insert_Command.UserConfig;
using Application_Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application_API.Controllers.UserConfig
{
    public class UserController : BaseController<UserController>
    {
        [HttpGet]
        public async Task<ActionResult> GetAllUser()
        {
            Response res = await this.Mediator.Send(new User_Inst_cmd());

            return this.Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> CreateUser([FromBody] User_Inst_cmd inst_Cmd)
        {
            Response res = await this.Mediator.Send(inst_Cmd);
            return this.Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateUser([FromBody] User_Upd_cmd upd_Cmd)
        {
            Response res = await this.Mediator.Send(upd_Cmd);
            return this.Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> DeleteUser(int Id)
        {
            Response res = await this.Mediator.Send(new User_Del_cmd { Id = Id });
            return this.Ok(res);
        }

        //[HttpPut]
        //public async Task<ActionResult<Response>> GetUserData(int Id)
        //{
        //    Response res = await this.Mediator.Send(new Demo_Customer_lst_cmd(Id));
        //    return this.Ok(res);
        //}
    }
}