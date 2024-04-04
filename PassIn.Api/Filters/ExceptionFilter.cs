using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using System.Net;

namespace PassIn.Api.Filters
{

    /* Filtro de Exceção: Toda vez que acontecer uma exceção 
     * no projeto, ela vai cair nessa classe. 
     */

    // Classe com um contrato/interface que vai obrigar a classe a implementar o método
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Verifica se a exception é uma PassInException
            var result = context.Exception is PassInException;

            if (result)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnkownError(context);
            }
        }

        // Verifica para saber qual o exception específico
        private void HandleProjectException(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                // NotFound vai dar um significado ao número e precisa do cast(conversão)
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

                // Devolve uma resposta para quem chamou o endpoint
                context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
            else if (context.Exception is ErrorOnValidationException)
            {
                // BadRequest vai dar um significado ao número e precisa do cast(conversão)
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
            else if (context.Exception is ConflictException)
            {
                // BadRequest vai dar um significado ao número e precisa do cast(conversão)
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Result = new ConflictObjectResult(new ResponseErrorJson(context.Exception.Message));
            }
        }

        private void ThrowUnkownError(ExceptionContext context)
        {
            // InternalServerError vai dar um significado ao número e precisa do cast(conversão)
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson("Unknown Error"));
        }


    }
}
