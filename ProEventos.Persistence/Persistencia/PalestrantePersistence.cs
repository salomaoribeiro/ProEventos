using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence.Persistencia
{
    public class PalestrantePersistence: IPalestrantePersistence
    {
        private readonly ProEventosContext _context;

        public PalestrantePersistence(ProEventosContext context)
        {
            this._context = context;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                                                        .Include(p => p.RedesSociais);

            if (incluirEventos)
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);

            query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

            if (incluirEventos)
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);

            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool incluirEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);

            if (incluirEventos)
                query = query.Include(p => p.PalestrantesEventos)
                             .ThenInclude(pe => pe.Evento);

            query = query.OrderBy(p => p.Id).Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
