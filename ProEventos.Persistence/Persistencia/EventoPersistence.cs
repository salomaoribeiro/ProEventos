using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persistencia
{
    public class EventoPersistence: IEventoPersistence
    {
        private readonly ProEventosContext _context;

        public EventoPersistence(ProEventosContext context)
        {
            this._context = context;
        }

        public Task<Evento[]> GetAllByTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.RedesSociais)
                                                       .Include(e => e.Lotes);

            if (incluirPalestrantes)
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                                                .Include(e => e.Lotes)
                                                .Include(e => e.RedesSociais);

            if (incluirPalestrantes)
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);

            query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.RedesSociais)
                                                       .Include(e => e.Lotes);

            if (incluirPalestrantes)
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);

            query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
