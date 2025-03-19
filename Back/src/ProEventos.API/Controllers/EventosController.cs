using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        public IEnumerable<Evento> eventos = new Evento[]
            {
                new Evento()
                {
                    EventoId = 1, 
                    Tema = "Angular 11 e .NET 5", 
                    Local = "Belo Horizonte", 
                    Lote = "1 lote", 
                    QtnPessoas = 250, 
                    DataEvento = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy"),
                    ImagemURL = "foto.png"
                },
                new Evento()
                {
                    EventoId = 2, 
                    Tema = "Angular 12 e suas novidades", 
                    Local = "São Paulo", 
                    Lote = "2 lote", 
                    QtnPessoas = 350, 
                    DataEvento = DateTime.Now.AddDays(90).ToString("dd/MM/yyyy"),
                    ImagemURL = "foto1.png"
                },          
            };
        public EventosController()
        {
           
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return eventos;
            
        }
        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return eventos.FirstOrDefault(evento => evento.EventoId == id); 
        }
        [HttpPost]
        public string Post()
        {
            return "Metodo Post";
        }
    }
}
