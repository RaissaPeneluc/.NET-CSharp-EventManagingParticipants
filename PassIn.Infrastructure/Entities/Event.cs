using System.ComponentModel.DataAnnotations.Schema;

namespace PassIn.Infrastructure.Entities
{

    /* Função: Classe que contenha todas as colunas de cada evento */

    public class Event
    {

        // Sempre ao criar um evento, a ID já vai vir preenchida
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int Maximum_Attendees { get; set; }
        [ForeignKey("Event_Id")]
        public List<Attendee> Attendees { get; set; } = [];
    }
}
