using Microsoft.EntityFrameworkCore;

namespace DocumentacaoSwagger.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Aviao> Avioes { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
