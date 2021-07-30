using Application.IPRs.Command;
using Application.SystemAnalysisModels.Query.GetWellModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [EnableCors("MyPolicy")]
    public class IPRController : ApiControllerBase
    {
        [HttpPut]
        public async Task<ActionResult<Guid>> Update(UpdateIPRCommand request)
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
