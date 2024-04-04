using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Checkins.DoCheckin;
using PassIn.Communication.Responses;

/* Controlador de Check-In */

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        // Post porque vai ser criado registro na tabela de CheckIn
        [HttpPost]
        [Route ("{attendeeId}")]

        // Tipo que vai ser devolvido, caso ocorra um erro
        [ProducesResponseType(typeof(ResponseRegisterJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
        public IActionResult CheckIn([FromRoute] Guid attendeeId)
        {

            var useCase = new DoAttendeeCheckinUseCase();

            var response = useCase.Execute(attendeeId);

            return Created(string.Empty, response);
        }
    }
}
