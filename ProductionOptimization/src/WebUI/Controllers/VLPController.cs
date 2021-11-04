using Application.VLP.CalculateVLP;
using Application.VLP.UpdateVLP;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [EnableCors("MyPolicy")]
    public class VLPController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResultDTO>> Update(CalculateVLPCommand request)
        {
            var value = await Mediator.Send(request);
            if (value == null)
            {
                return UnprocessableEntity();
            }
            return value;
        }
        [HttpPut]
        public async Task<ActionResult<Guid>> Update(UpdateVLPCommand request)
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
