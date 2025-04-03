using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interface;

namespace ProEventos.Persistence
{
    public class ProEventosEventoPersist : IProEventosEventoPersist
    {
        private readonly ProEventosContext context;

        public ProEventosEventoPersist(ProEventosContext context)
        {
            this.context = context;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = context.Eventos
                                              .Include(evento => evento.Lotes)
                                              .Include(evento => evento.RedesSociais);
            if (includePalestrante)
            {
                query = query
                        .Include(evento => evento.PalestrantesEventos)
                        .ThenInclude(palestranteEventos => palestranteEventos.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(evento => evento.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
            IQueryable<Evento> query = context.Eventos
                                              .Where(evento => evento.Tema.ToLower().Contains(tema.ToLower()))
                                              .Include(evento => evento.Lotes)
                                              .Include(evento => evento.RedesSociais);
            if (includePalestrante)
            {
                query = query
                        .Include(evento => evento.PalestrantesEventos)
                        .ThenInclude(palestranteEventos => palestranteEventos.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(evento => evento.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
            IQueryable<Evento> query = context.Eventos
                                                .Where(evento => evento.Id == eventoId)
                                               .Include(evento => evento.Lotes)
                                               .Include(evento => evento.RedesSociais);
            if (includePalestrante)
            {
                query = query
                        .Include(evento => evento.PalestrantesEventos)
                        .ThenInclude(palestranteEventos => palestranteEventos.Palestrante);
            }

            query = query.AsNoTracking().OrderBy(evento => evento.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}