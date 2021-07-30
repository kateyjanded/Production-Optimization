using Application.PVT.Command;
using Application.PVT.Command.UpdatPVT;
using Application.SystemAnalysisModels.Query.GetWellModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [EnableCors("MyPolicy")]
    public class PVTController: ApiControllerBase
    {
        [HttpPost("{SystemAnalysisId}")]
        public async Task<ActionResult<Guid>> Create(CreatePVTCommand request)
        {
            var value = await Mediator.Send(request);
            if (value == Guid.Empty)
            {
                return NotFound();
            }
            return value;
        }
        [HttpPut]
        public async Task<ActionResult<Guid>> Update(UpdatePVTCommand request)
        {
            var value = await Mediator.Send(request);
            if (value == Guid.Empty)
            {
                return NotFound();
            }
            return value;
        }
    }
}
