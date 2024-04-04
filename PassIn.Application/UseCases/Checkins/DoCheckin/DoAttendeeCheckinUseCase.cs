using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Checkins.DoCheckin
{
    public class DoAttendeeCheckinUseCase
    {

        private readonly PassInDbContext _dbContext;

        // Construtor para fazer a instância do PassInDbContext
        public DoAttendeeCheckinUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisterJson Execute(Guid attendeeId)
        {
            
            Validate(attendeeId);

            var entity = new CheckIn
            {
                Attendee_Id = attendeeId,
                Created_at = DateTime.UtcNow,
            };

            _dbContext.CheckIns.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisterJson
            {
                Id = entity.Id,
            };
        }

        // Verificando na tabela de participante se ele existe
        private void Validate(Guid attendeeId)
        {
            var existAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);

            if(existAttendee == false)
            {
                throw new NotFoundException("The attendee with this Id wasn't found");
            }

            var existCheckin = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);

            if(existCheckin == true)
            {
                throw new ConflictException("Attendee can't do checking twice in the same event.");
            }
        }
    }
}
