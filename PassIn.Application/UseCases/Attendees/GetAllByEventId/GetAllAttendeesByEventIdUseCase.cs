﻿using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId
{
    public class GetAllAttendeesByEventIdUseCase
    {

        private readonly PassInDbContext _dbContext;

        // Construtor para fazer a instância do PassInDbContext
        public GetAllAttendeesByEventIdUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseAllAttendeesJson Execute (Guid eventId)
        {
            // Vai no banco de dados e encontra um evento com o id do parâmetro
            var entity = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(attendee => attendee.CheckIn).FirstOrDefault(ev => ev.Id == eventId);

            if (entity is null)
            {
                throw new NotFoundException("An event with this ID don't exist");
            }

            return new ResponseAllAttendeesJson
            {
                Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
                {
                    Id = attendee.Id,   
                    Name = attendee.Name,
                    Email = attendee.Email, 
                    CreatedAt = attendee.Created_At,
                    CheckedInAt = attendee.CheckIn?.Created_at
                }).ToList(),
            };
        }
    }
}