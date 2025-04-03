using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Interface;
using ProEventos.Domain;
using ProEventos.Persistence.Interface;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IProEventosEventoPersist eventoPersist;
        private readonly IProEventosGeralPersist geralPersist;

        public EventoService(IProEventosGeralPersist geralPersist, IProEventosEventoPersist eventoPersist)
        {
            this.geralPersist = geralPersist;
            this.eventoPersist = eventoPersist;
        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                geralPersist.Add<Evento>(model);
                if(await geralPersist.SaveChangesAsync())
                {
                    return await eventoPersist.GetEventoByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao adicionar o Evento");
            }
        }
         public async Task<Evento> UpDateEvento(int id, Evento model)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(id);
                if(evento == null) return null;
                model.Id = evento.Id;
                geralPersist.Update<Evento>(model);
                if(await geralPersist.SaveChangesAsync())
                {
                    return await eventoPersist.GetEventoByIdAsync(model.Id);
                }
                return null;
            }
            catch (Exception)
            {
                
                throw new Exception("Erro ao atualizar o Evento");
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(id);
                if(evento == null) throw new Exception("Evento para Delete não encontrado.");
                geralPersist.Delete<Evento>(evento);
                return await geralPersist.SaveChangesAsync();
            }
            catch (Exception)
            {
                
                throw new Exception("Erro ao Deletar o Evento");
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrante = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosAsync(includePalestrante);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao carregar os Eventos");
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrante = false)
        {
           try
            {
                var eventos = await eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrante);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao carregar os Eventos por Tema");
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante = false)
        {
            try
            {
                var eventos = await eventoPersist.GetEventoByIdAsync(eventoId, includePalestrante);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception)
            {
                throw new Exception("Erro ao carregar os Evento por Id");
            }
        }

    }
}