using Application_Command.Insert_Command;
using Application_Command.List_Command;
using Application_Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application_API.Controllers
{
    public class ValuesController : BaseController<ValuesController>
    {
        [HttpGet]
        public async Task<ActionResult> GetValue()
        {
            var res = await this.Mediator.Send(new Demo_Customer_lst_cmd());

            return this.Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult<Response>> PostValue([FromBody] Demo_Customer_Inst_cmd customer_Cmd)
        {
            var res = await this.Mediator.Send(customer_Cmd);
            return this.Ok(res);
        }
    }
}