using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure
{
    /* Função: Funcionar como se fosse um tradutor 
     de entidades/classes para query e vice-versa */

    // Herança para fazer a tradução
    public class PassInDbContext : DbContext
    {
        // Representação da tabela eventos, possibilitando alterar, remover, adicionar eventos
        public DbSet<Event> Events { get; set; }

        // Mapeamento da tabela participantes
        public DbSet<Attendee> Attendees { get; set; }

        // Mapeamento da tabela de checkin
        public DbSet<CheckIn> CheckIns { get; set; }

        // Esse override vai configurar esse contexto que vai linkar ele com a base de dados
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Estudos\\Rocketseat - nlw unit\\PassInDb.db");
        }

    }
}
