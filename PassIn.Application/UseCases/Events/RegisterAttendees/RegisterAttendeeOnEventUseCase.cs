using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace PassIn.Application.UseCases.Events.RegisterAttendees
{
    public class RegisterAttendeeOnEventUseCase
    {
        private readonly PassInDbContext _dbContext;

        // Construtor para fazer a instância do PassInDbContext
        public RegisterAttendeeOnEventUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisterJson Execute(Guid eventId,  RequestRegisterEventJson request)
        {
            Validate(eventId, request);

            var entity = new Infrastructure.Entities.Attendee
            {
                Email = request.Email,
                Name = request.Name,
                Event_Id = eventId,
                Created_At = DateTime.UtcNow,
            };

            // Prepara uma query com a entity para o banco de dados
            _dbContext.Attendees.Add(entity);

            // Executar a query
            _dbContext.SaveChanges();

            return new ResponseRegisterJson
            {
                Id = entity.Id,
            };
        }

        // Validação se o ID do evento existe
        private void Validate(Guid eventId, RequestRegisterEventJson request)
        {
            // Olha na tabela de evento se existe qualquer evento onde o ID dele é igual o do parâmetro
            var eventEntity = _dbContext.Events.Find(eventId);

            if (eventEntity is null)
            {
                throw new NotFoundException("An event with this ID doesn't exist");

            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("The name is invalid.");
            }

            // Verifica se o email está correto
            var emailIsValid = EmailIsValid(request.Email);
            if(emailIsValid == false)
            {
                throw new ErrorOnValidationException("The e-mail is invalid.");
            }

            // Valida se existe mais de um email igual registrado
            var attendeeAlreadyRegistered = _dbContext
                .Attendees
                .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);

            if (attendeeAlreadyRegistered)
            {
                throw new ConflictException("You can't register twice on the same event.");
            }

            // Valida antes de adicionar um participante novo, se o máximo de participantes já bateu
            var attendeesForEvent = _dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);
            if(attendeesForEvent > eventEntity.Maximum_Attendees)
            {
                throw new ErrorOnValidationException("There is no room for this event.");
            }
        }

        // Validação se o email é válido
        private bool EmailIsValid(string email)
        {
            // Trtando exceção sem que ela caia no filtro
            try
            {
                // Instacia uma classe, com o parâmetro email, se for false vai lançar uma exceção
                new MailAddress(email);

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
