using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CQRsAndMEdiatorsEXample.features.Command;
using CQRsAndMEdiatorsEXample.Models;

namespace CQRsAndMEdiatorsEXample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartcipentController : ControllerBase
    {
        private readonly IMediator _mediator;


        public PartcipentController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpPost]
        [Route("InsertParticipant")]
        public async Task<Participant> InsertParticipant([FromBody] InsertParticipantCommand command)
        {
            var insertedParticipant = await _mediator.Send(command);
            return insertedParticipant;
        }


        [HttpPost]
        [Route("UpdateOutput")]
        public async Task<IActionResult> UpdateOutput([FromBody] UpdateParticipantCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}
