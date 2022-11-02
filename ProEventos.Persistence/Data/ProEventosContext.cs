using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;

namespace ProEventos.Persistence.Data
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new { PE.EventoId, PE.PalestranteId });

            // Quando deletar um evento deletar as redes sociais desse evento
            modelBuilder.Entity<Evento>()
                .HasMany(e => RedesSociais)
                .WithOne(re => re.Evento)
                .OnDelete(DeleteBehavior.Cascade);

            // quando deletar um palestrante deletar as redes sociais desse palestrante
            modelBuilder.Entity<Palestrante>()
                .HasMany(p => p.RedesSociais)
                .WithOne(re => re.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
