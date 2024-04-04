using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

// A função do Projeto de API é receber a requisição, passar para uma regra de negócio e devolver a resposta.

/* Controlador de todos os eventos */

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        // EndPoint para criar um evento, para criar um dado no banco de dados o endpoint tem que ser Post
        [HttpPost]

        // Tipo que vai ser devolvido, caso ocorra um erro
        [ProducesResponseType(typeof(ResponseRegisterJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]


        // Parâmetro esperando receber do body da requisição, o RequestEventJson, dando o nome de request
        public IActionResult Register([FromBody] RequestEventJson request)
        {
            // O useCase (Regra de negócio) é executado.

            var useCase = new RegisterEventUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }

        // Endpoint que recupere um evento por ID, que devolva as informações de um evento específico
        [HttpGet]

        // Para receber os Id's na rota, sempre recebe ID's pela rota
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {

            var useCase = new GetEventByIdUseCase();

            var response = useCase.Execute(id);

            return Created(string.Empty, response);
        }
    }
}
