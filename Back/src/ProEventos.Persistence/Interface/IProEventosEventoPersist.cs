using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interface
{
    public interface IProEventosEventoPersist
    {
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false);

    }
}