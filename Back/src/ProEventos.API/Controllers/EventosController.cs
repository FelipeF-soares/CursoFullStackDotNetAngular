using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Application.Interface;
using ProEventos.Domain;
using ProEventos.Persistence;


namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService eventoService;

        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes
                           .Status500InternalServerError,
                           $"Erro ao tentar recuperar eventos. Erro:");
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound("Nenhum evento por ID encontrado.");

                return Ok(evento);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes
                           .Status500InternalServerError,
                           $"Erro ao tentar recuperar evento. Erro:");
            }
        }
        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var evento = await eventoService.GetAllEventosByTemaAsync(tema, true);
                if (evento == null) return NotFound("Nenhum eventos por tema encontrado.");

                return Ok(evento);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes
                           .Status500InternalServerError,
                           $"Erro ao tentar recuperar evento. Erro:");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await eventoService.AddEvento(model);
                if (evento == null) return BadRequest("Erro Nenhum eventos adicionado.");

                return Ok(evento);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes
                           .Status500InternalServerError,
                           $"Erro ao tentar inserir evento. Erro:");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var evento = await eventoService.UpDateEvento(id, model);
                if (evento == null) return BadRequest("Erro ao Atualizar o Evento.");

                return Ok(evento);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes
                           .Status500InternalServerError,
                           $"Erro ao tentar atualizar evento. Erro:");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await eventoService.DeleteEvento(id)
                                         ? Ok("Deletado")
                                         : BadRequest("Evento não deletado");
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes
                           .Status500InternalServerError,
                           $"Erro ao tentar deletar evento. Erro:");
            }
        }
    }
}
