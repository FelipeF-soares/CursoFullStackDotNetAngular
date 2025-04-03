using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interface;

namespace ProEventos.Persistence
{
    public class ProEventosPalestrantePersist : IProEventosPalestrantePersist
    {
        private readonly ProEventosContext context;

        public ProEventosPalestrantePersist(ProEventosContext context)
        {
            this.context = context;

        }
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                                               .Include(palestrante => palestrante.RedesSociais);
            if (includeEventos)
            {
                query = query
                        .Include(palestrante => palestrante.PalestrantesEventos)
                        .ThenInclude(palestranteEventos => palestranteEventos.Evento);
            }

            query = query.AsNoTracking().OrderBy(palestrante => palestrante.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                                            .Where(palestrante => palestrante.Nome.ToLower().Contains(nome.ToLower()))
                                            .Include(palestrante => palestrante.RedesSociais);
            if (includeEventos)
            {
                query = query
                        .Include(palestrante => palestrante.PalestrantesEventos)
                        .ThenInclude(palestranteEventos => palestranteEventos.Evento);
            }

            query = query.AsNoTracking().OrderBy(palestrante => palestrante.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = context.Palestrantes
                                            .Where(palestrante => palestrante.Id == palestranteId)
                                            .Include(palestrante => palestrante.RedesSociais);
            if (includeEventos)
            {
                query = query
                        .Include(palestrante => palestrante.PalestrantesEventos)
                        .ThenInclude(palestranteEventos => palestranteEventos.Evento);
            }

            query = query.AsNoTracking().OrderBy(palestrante => palestrante.Id);
            return await query.FirstOrDefaultAsync();
        }
    }
}