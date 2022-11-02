using ProEventos.Application.Interfaces;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Persistencia;
using System.Linq.Expressions;

namespace ProEventos.Application.Application
{
    public class EventoService : IEventoService
    {
        private readonly BasePersistence _basePersistence;
        private readonly EventoPersistence _eventoPersistence;

        public EventoService(BasePersistence basePersistence, EventoPersistence eventoPersistence)
        {
            this._basePersistence = basePersistence;
            this._eventoPersistence = eventoPersistence;
        }

        public async Task<Evento> AddEvento(Evento evento)
        {
            try
            {
                _basePersistence.Add(evento);

                if (await _basePersistence.SaveChangesAsync())
                    return await _eventoPersistence.GetEventoByIdAsync(evento.Id);

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)

        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);

                if (evento is null)
                    throw new Exception("Evento para delete não encontrado.");

                _basePersistence.Remove(evento);

                return await _basePersistence.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);

            if (evento is not null)
            {
                model.Id = eventoId;
                _basePersistence.Update(model);
            }

            if (await _basePersistence.SaveChangesAsync())
                return await _eventoPersistence.GetEventoByIdAsync(eventoId);
            
            return null;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool incluirPalestrantes = false)
        {
            try
            {
                return await _eventoPersistence.GetAllEventosAsync(incluirPalestrantes);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool incluirPalestrantes = false)
        {
            return await _eventoPersistence.GetAllByTemaAsync(tema, incluirPalestrantes);
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool incluirPalestrantes = false)
        {
            return await _eventoPersistence.GetEventoByIdAsync(eventoId, incluirPalestrantes);
        }
    }
}
