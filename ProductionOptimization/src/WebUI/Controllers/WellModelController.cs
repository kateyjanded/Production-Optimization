using Application.SystemAnalysisModels.Commands.CreateModel;
using Application.SystemAnalysisModels.Commands.DeleteModel;
using Application.SystemAnalysisModels.Query.GetWellModel;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [EnableCors("MyPolicy")]
    public class WellModelController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<WellModelVM>> Create(CreateSystemAnalysisModel command)
        {
            var value = await Mediator.Send(command);
            if (value == null)
            {
                return BadRequest();
            }
            return value;
        }
        [HttpGet]
        public async Task<ActionResult<List<WellModelVM>>> GetAll()
        {
            return await Mediator.Send(new GetWellModelQuery());
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<WellModelVM>> Get(Guid Id)
        {
            var value = await Mediator.Send(new GetWellModelByIDQuery() { Id = Id});
            if (value == null)
            {
                return NotFound();
            }
            return value;
        }
        [HttpDelete("{Id}")]
        public async Task<Unit> Delete(Guid Id)
        {
            return await Mediator.Send(new DeleteModelCommand() { Id = Id });
        }
    }
}
