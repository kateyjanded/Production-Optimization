using Application.ModelBackGround.Commands;
using Application.ModelBackGround.Commands.UpdateCommand;
using Application.SystemAnalysisModels.Query.GetWellModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [EnableCors("MyPolicy")]
    public class ModelBackgroundController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateModelBackgroundCommand command)
        {
            var value = await Mediator.Send(command);
            if (value == Guid.Empty)
            {
                return NotFound();
            }
            return value;
        }
        [HttpPut]
        public async Task<ActionResult<Guid>> Update(UpdateModelBackgroundCommand command)
        {
            var value = await Mediator.Send(command);
            if (value == Guid.Empty)
            {
                return NotFound();
            }
            return value;
        }
    }
}
