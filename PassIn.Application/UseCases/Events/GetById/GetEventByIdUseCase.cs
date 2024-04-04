using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById
{
    public class GetEventByIdUseCase
    {
        // Função para executar uma regra de negócio para recuperar um evento por ID
        public ResponseEventJson Execute(Guid id)
        {
            // Instanciando o DbContext
            var dbContext = new PassInDbContext();

            // Vai no banco de dados e encontra um evento com o id do parâmetro
            var entity = dbContext.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id == id);

            if(entity is null)
            {
                throw new NotFoundException ("An event with this ID don't exist");
            }

            return new ResponseEventJson
            {
                Id = entity.Id,
                Title = entity.Title,
                Details = entity.Details,
                MaximumAttendees = entity.Maximum_Attendees,
                AttendeesAmount = entity.Attendees.Count(),
            };
        }
    }
}
