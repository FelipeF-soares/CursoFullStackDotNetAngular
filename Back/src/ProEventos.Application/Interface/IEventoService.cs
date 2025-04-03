using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Interface
{
    public interface IEventoService
    {
        Task<Evento> AddEvento(Evento model);
        Task<Evento> UpDateEvento(int id,Evento model);
        Task<bool> DeleteEvento(int id);
        Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false);
        Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false);
        Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false);
    }
}