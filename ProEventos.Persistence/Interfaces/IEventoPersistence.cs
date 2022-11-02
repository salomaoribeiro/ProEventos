using ProEventos.Domain.Models;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoPersistence
    {
        Task<Evento[]> GetAllByTemaAsync(string tema, bool incluirPalestrantes);
        Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes);
    }
}
