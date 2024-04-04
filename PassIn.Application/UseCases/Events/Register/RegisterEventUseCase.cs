using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register
{
    // É responsável apenas por registrar, uma classe com apenas uma função
    public class RegisterEventUseCase
    {

        // Para executar essa função, é preciso receber o parâmetro request
        public ResponseRegisterJson Execute(RequestEventJson request)
        {
            Validate(request);

            /* Resolução Erro: Colocar Slug como único diretamente no modelo do banco 
             * de dados, usando a anotação [Unique] na propriedade Slug da classe Event.
             * Isso fará com que o próprio banco de dados SQLite garanta a unicidade do
             * valor do Slug e evite que o erro ocorra. 
             */

            using (var dbContext = new PassInDbContext())
            {
                var entity = new Infrastructure.Entities.Event
                {
                    Title = request.Title,
                    Details = request.Details,
                    Maximum_Attendees = request.MaximumAttendees,
                    Slug = request.Title.ToLower().Replace(" ", "-")
                };

                // Prepara uma query com a entity para o banco de dados
                dbContext.Events.Add(entity);
                // Executar a query
                dbContext.SaveChanges();

                return new ResponseRegisterJson
                {
                    Id = entity.Id,
                };
            }
        }

        // Tem a função de validar o request, em caso de fornecimento de informações erradas
        private void Validate(RequestEventJson request)
        {
            // Se a condição não for atendida, vai ser lançada uma exceção

            if(request.MaximumAttendees <= 0)
            {
                throw new ErrorOnValidationException ("The Maximum attendees is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ErrorOnValidationException("The title is invalid.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ErrorOnValidationException("The details is invalid.");
            }
        }

    }
}
 